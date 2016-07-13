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
        //不计算了，我自己之前算好的一个永不失效的TOKEN，是website空间
        public String TOKEN_STR = "";
        public bool UpLoadFile(String _filePath)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri("http://upload.media.aliyun.com");

            var request = new RestRequest("/api/proxy/upload", Method.POST);
            request.AddHeader("Accept-Encoding", "gzip,deflate");
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddHeader("Authorization", TOKEN_STR);

            request.AddParameter("dir", "qxqzxcom");
            request.AddParameter("name","qxqzx.jpg");

            FileStream fs1 = File.OpenRead(_filePath);
            long filesize = fs1.Length;
            fs1.Close();

            request.AddParameter("size", filesize);
            request.AddFile("content", _filePath, "application/octet-stream");


            IRestResponse response = client.Execute(request);
            return false;
        }
    }
}
