using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ALBC_REST_SDK.ALBCUnit
{
    public class BlockFile
    {
        /// <summary>
        /// 分片上传文件接口，如果有错误会抛出异常，需要外部捕获
        /// </summary>
        /// <param name="_filePath">本地文件全路径</param>
        /// <param name="_uploadFileName">文件名</param>
        /// <param name="_uploadPath">上传路径</param>
        /// <param name="_token">上传TOKEN</param>
        public static void uploadFile(string _filePath, string _uploadFileName, string _uploadPath, string _token)
        {
            FileStream fs1 = File.OpenRead(_filePath);
            long filesize = fs1.Length;

            long blockSize = Conf.BLOCK_DEFF_SIZE;
            int blockNum = (int)Math.Ceiling(Convert.ToDouble(filesize) / Convert.ToDouble(blockSize));

            string _uploadId = "";//分块上传id
            string _id = "";//上传唯一ID

            List<ResultPart> _allPartNum_ETag = new List<ResultPart>();
            int i = 0;
            for (; i < blockNum; i++)
            {
                long currentSize = blockSize; // 当前文件块的大小
                if ((i + 1) == blockNum)
                {
                    currentSize = (filesize - (blockNum - 1) * blockSize); // 计算最后一个块的大小
                }
                
                byte[] blockData = new byte[currentSize];//file_get_contents(_filePath, 0, null, offset, currentSize); //当前文件块的数据

                long readSize = fs1.Read(blockData, 0, (int)currentSize);

                if(readSize == currentSize)
                {
                    if(i == 0)//分片上传初始化
                    {
                        var client = new RestClient();
                        client.BaseUrl = new Uri(Conf.UPLOAD_HOST_MEDIA);

                        var request = new RestRequest(Conf.UPLOAD_API_BLOCK_INIT, Method.POST);
                        request.AddHeader("Content-Type", "multipart/form-data");
                        request.AddHeader("Authorization", _token);

                        request.AddParameter("dir", _uploadPath);
                        request.AddParameter("name", _uploadFileName);
                        request.AddParameter("size", currentSize);
                        request.AddFile("content", blockData, _uploadFileName, "application/octet-stream");

                        var response = client.Execute<ResultType>(request);
                        if (response.ErrorException != null && response.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            fs1.Close();
                            var twilioException = new ApplicationException(response.StatusDescription, response.ErrorException);
                            throw twilioException;
                        }
                        else
                        {
                            ResultType _rs = response.Data;
                            if (_rs.code.ToUpper() == "OK")
                            {
                                _uploadId = _rs.uploadId;
                                _id = _rs.id;
                                _allPartNum_ETag.Add(new ResultPart(_rs.partNumber,_rs.eTag));
                            }
                        }
                    }
                    else
                    {
                        if(!string.IsNullOrEmpty(_uploadId) || !string.IsNullOrEmpty(_id))
                        {
                            var client = new RestClient();
                            client.BaseUrl = new Uri(Conf.UPLOAD_HOST_MEDIA);

                            var request = new RestRequest(Conf.UPLOAD_API_BLOCK_UPLOAD, Method.POST);
                            request.AddHeader("Content-Type", "multipart/form-data");
                            request.AddHeader("Authorization", _token);

                            request.AddParameter("id", _id);
                            request.AddParameter("uploadId", _uploadId);
                            request.AddParameter("partNumber",i+1);
                            request.AddParameter("size", currentSize);
                            request.AddFile("content", blockData, _uploadFileName, "application/octet-stream");

                            var response = client.Execute<ResultType>(request);
                            if (response.ErrorException != null && response.StatusCode != System.Net.HttpStatusCode.OK)
                            {
                                fs1.Close();
                                var twilioException = new ApplicationException(response.StatusDescription, response.ErrorException);
                                throw twilioException;
                            }
                            else
                            {
                                ResultType _rs = response.Data;
                                if (_rs.code.ToUpper() == "OK")
                                {
                                    _uploadId = _rs.uploadId;
                                    _id = _rs.id;
                                    _allPartNum_ETag.Add(new ResultPart(_rs.partNumber, _rs.eTag));
                                }
                            }
                        }
                    }
                }
            }

            //发送上传完成请求
            if (!string.IsNullOrEmpty(_uploadId) || !string.IsNullOrEmpty(_id))
            {
                var client = new RestClient();
                client.BaseUrl = new Uri(Conf.UPLOAD_HOST_MEDIA);

                var request = new RestRequest(Conf.UPLOAD_API_BLOCK_COMPLETE, Method.POST);
                request.AddHeader("Content-Type", "multipart/form-data");
                request.AddHeader("Authorization", _token);

                request.AddParameter("id", _id);
                request.AddParameter("uploadId", _uploadId);
                request.AddParameter("parts", GeFileParts(_allPartNum_ETag));

                var response = client.Execute<ResultType>(request);
                if (response.ErrorException != null && response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    fs1.Close();
                    var twilioException = new ApplicationException(response.StatusDescription, response.ErrorException);
                    throw twilioException;
                }
                else
                {
                    ResultType _rs = response.Data;
                    if (_rs.code.ToUpper() == "OK")
                    {
                        _uploadId = _rs.uploadId;
                        _id = _rs.id;
                        _allPartNum_ETag.Add(new ResultPart(_rs.partNumber, _rs.eTag));
                    }
                }
            }
            fs1.Close();
        }
        /// <summary>
        /// 通过分片上传返回的结果构造分片上传结束所需的参数
        /// </summary>
        /// <param name="fileParts">分片上传中记录的所有partNumber和eTag对</param>
        /// <returns>返回json的base64编码字符串</returns>
        private static string GeFileParts(List<ResultPart> fileParts)
        {
            string rs = JsonConvert.SerializeObject(fileParts, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            rs = Base64Encode(rs);
            return rs;
        }

        private static string Base64Encode(string AStr)
        {
            string _rs = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(AStr));
            _rs = _rs.Replace("+", "-");
            _rs = _rs.Replace("/", "_");
            _rs = _rs.Replace("=", "");
            return _rs;
        }
    }
}
