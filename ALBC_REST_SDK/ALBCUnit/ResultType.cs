using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALBC_REST_SDK.ALBCUnit
{
    public class ResultType
    {
        public string code { get; set; }
        public string mimeType { get; set; }
        public string dir { get; set; }
        public string message { get; set; }
        public string uri { get; set; }
        public string url { get; set; }
        public Boolean isImage { get; set; }
        public string fileSize { get; set; }
        public string requestId { get; set; }
        public string name { get; set; }
        public string eTag { get; set; }
        public string fileModified { get; set; }
        public string fileId { get; set; }

        //以下返回值是分片上传中的
        /// <summary>
        /// 分片上传id
        /// </summary>
        public string uploadId { get; set; }
        /// <summary>
        /// 块编号
        /// </summary>
        public String partNumber { get; set; }
        /// <summary>
        /// 上传唯一ID
        /// </summary>
        public string id { get; set; }
    }

    public class ResultPart
    {
        public ResultPart(string strNumber,string _etag)
        {
            partNumber = strNumber;
            eTag = _etag;
        }
        public string partNumber { get; set; }
        public string eTag { get; set; }
    }
}
