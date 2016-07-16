using ALBC_REST_SDK.ALBCUnit;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALBC_REST_SDK
{
    public class ALBCClient
    {
        string TOKEN_STR = "";

        /// <summary>
        /// 通过ak,sk,和存储空间来构造TOKEN
        /// </summary>
        /// <param name="_ak"></param>
        /// <param name="_sk"></param>
        /// <param name="_nameSpaceV"></param>
        public ALBCClient(string _ak, string _sk, string _nameSpaceV)
        {
            UploadPolicy po = new UploadPolicy()
            {
                expiration = "-1",
                insertOnly = 0,
                //name = "${uuid}",文件使用随即名
                NameSpace = _nameSpaceV,
            };
            TOKEN_STR = ALBC_REST_SDK.ALBCUnit.Unit.GetPicTokenByPolicy(_ak, _sk, ALBC_REST_SDK.ALBCUnit.Unit.BulidUploadPolicy(po));
        }

        /// <summary>
        /// 通过ak,sk,和上传策略来构造TOKEN
        /// </summary>
        /// <param name="_ak"></param>
        /// <param name="_sk"></param>
        /// <param name="_po"></param>
        public ALBCClient(string _ak, string _sk, UploadPolicy _po)
        {
            TOKEN_STR = ALBC_REST_SDK.ALBCUnit.Unit.GetPicTokenByPolicy(_ak, _sk, ALBC_REST_SDK.ALBCUnit.Unit.BulidUploadPolicy(_po));
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="_filePath">本地要上传的文件的路径</param>
        /// <returns>是否上传成功</returns>
        public bool UpLoadFile(string _filePath)
        {
            string filename = System.IO.Path.GetFileName(_filePath);

            return UpLoadFile(_filePath, filename, "");
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="_filePath">本地要上传的文件的路径</param>
        /// <param name="_uploadFileName">上传到服务器后文件的名字（包括后缀名）</param>
        /// <param name="_uploadPath">上传到服务器上的路径</param>
        /// <returns></returns>
        public bool UpLoadFile(string _filePath, string _uploadFileName, string _uploadPath)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(Conf.UPLOAD_HOST_MEDIA);

            var request = new RestRequest(Conf.UPLOAD_API_UPLOAD, Method.POST);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddHeader("Authorization", TOKEN_STR);

            request.AddParameter("dir", _uploadPath);
            request.AddParameter("name", _uploadFileName);

            FileStream fs1 = File.OpenRead(_filePath);
            long filesize = fs1.Length;
            fs1.Close();

            request.AddParameter("size", filesize);
            request.AddFile("content", _filePath, "application/octet-stream");

            var response = client.Execute<ResultType>(request);

            if (response.ErrorException != null && response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var twilioException = new ApplicationException(response.StatusDescription, response.ErrorException);
                throw twilioException;
            }
            ResultType _rs = response.Data;
            if (_rs.code.ToUpper() == "OK")
                return true;
            return false;
        }

        /// <summary>
        /// 分片上传大文件，每片是2M
        /// </summary>
        /// <param name="_filePath">文件路径</param>
        /// <returns></returns>
        public bool UpLoadFileBlock(string _filePath)
        {
            string filename = System.IO.Path.GetFileName(_filePath);

            return UpLoadFileBlock(_filePath, filename, "");
        }

        /// <summary>
        /// 分片上传大文件，每次上传2M数据，由服务端自己合并
        /// </summary>
        /// <param name="_filePath">文件路径</param>
        /// <param name="_uploadFileName">文件上传名称</param>
        /// <param name="_uploadPath">文件保存路径</param>
        /// <returns></returns>
        public bool UpLoadFileBlock(string _filePath, string _uploadFileName, string _uploadPath)
        {
            BlockFile.uploadFile(_filePath, _uploadFileName, _uploadPath, TOKEN_STR);

            return false;
        }
    }
}
