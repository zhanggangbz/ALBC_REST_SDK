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

        public string fileId12 { get; set; }
    }
}
