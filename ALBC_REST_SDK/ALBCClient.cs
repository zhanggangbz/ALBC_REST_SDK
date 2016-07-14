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
        private String _nameSpace;
        public ALBCClient(String _nameSpaceV)
        {
            _nameSpace = _nameSpaceV;
        }
        //不计算了，我自己之前算好的一个永不失效的TOKEN，是website空间
        //public String TOKEN_STR = ALBC_REST_SDK.ALBCUnit.Unit.GetPicTokenDefault("23247861", "4236288dc5e344610b0af3de54e6028d","website");//"UPLOAD_AK_TOP MjMyODU3OTQ6ZXlKa1pYUmxZM1JOYVcxbElqb3hMQ0psZUhCcGNtRjBhVzl1SWpvdE1Td2lhVzV6WlhKMFQyNXNlU0k2TUN3aWJtRnRaWE53WVdObElqb2lkMlZpYzJsMFpTSXNJbk5wZW1WTWFXMXBkQ0k2TUgwOjljZmQ2OTA2OTJmYWFjYTgyMWEwYTdlNjQ2Y2EyYTk4NzkwNDE4YTg";
        public bool UpLoadFile(String _filePath)
        {
            UploadPolicy po = new UploadPolicy()
            {
                expiration = "-1",
                insertOnly = 0,
                name = "${uuid}",
                NameSpace = _nameSpace,
            };
            String TOKEN_STR = ALBC_REST_SDK.ALBCUnit.Unit.GetPicTokenByPolicy("23247861", "4236288dc5e344610b0af3de54e6028d", ALBC_REST_SDK.ALBCUnit.Unit.BulidUploadPolicy(po));

            string filename = System.IO.Path.GetFileName(_filePath);

            var client = new RestClient();
            client.BaseUrl = new Uri(Conf.UPLOAD_HOST_MEDIA);

            var request = new RestRequest(Conf.UPLOAD_API_UPLOAD, Method.POST);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddHeader("Authorization", TOKEN_STR);

            request.AddParameter("dir", "qxqzxcom");
            request.AddParameter("name", filename);

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
