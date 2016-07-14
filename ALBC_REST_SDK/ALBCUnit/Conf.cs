using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALBC_REST_SDK.ALBCUnit
{
    public class Conf
    {
        public const String CHARSET = "UTF-8";
	    public const String SDK_VERSION = "2.0.3";
	
        public const String UPLOAD_HOST_MEDIA = "http://upload.media.aliyun.com";		//文件上传的地址
        public const String MANAGE_HOST_MEDIA = "http://rs.media.aliyun.com";			//服务管理的地址
        public const String MANAGE_API_VERSION = "3.0";		//资源管理接口版本
        public const String SCAN_PORN_VERSION = "3.1";		//黄图扫描接口版本
        public const String MEDIA_ENCODE_VERSION = "3.0";		//媒体转码接口版本
    
        public const String UPLOAD_API_UPLOAD = "/api/proxy/upload";
        public const String UPLOAD_API_BLOCK_INIT = "/api/proxy/blockInit";
        public const String UPLOAD_API_BLOCK_UPLOAD = "/api/proxy/blockUpload";
        public const String UPLOAD_API_BLOCK_COMPLETE = "/api/proxy/blockComplete";
        public const String UPLOAD_API_BLOCK_CANCEL = "/api/proxy/blockCancel";
    
        public const String TYPE_TOP = "TOP";
        public const String TYPE_CLOUD = "CLOUD";
    
        public const int DETECT_MIME_TRUE = 1;			//检测MimeType
        public const int DETECT_MIME_NONE = 0;			//不检测MimeType
        public const int INSERT_ONLY_TRUE = 1;			//文件上传不可覆盖
        public const int INSERT_ONLY_NONE = 0;			//文件上传可覆盖
    
        public const int MIN_OBJ_SIZE = 102400;		//1024*100;
        public const int HTTP_TIMEOUT = 30;			//http的超时时间：30s
        public const int HTTP_RETRY = 1;				//http失败后重试：1
    
        public const int BLOCK_MIN_SIZE = 102400;		//文件分片最小值：1024*100; 100K
        public const int BLOCK_DEFF_SIZE = 2097152;	//文件分片默认值：1024*1024*2; 2M
        public const int BLOCK_MAX_SIZE = 10485760;	//文件分片最大值：1024*1024*10; 10M
    
        public const String CURL_ERR_LOG = "curl_error.log";	//curl请求时的错误日志信息

        public const int RUN_LEVEL_RELEASE = 1;		//release级别
        public const int RUN_LEVEL_DEBUG = 2;			//debug级别
    }
}
