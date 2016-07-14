using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALBC_REST_SDK.ALBCUnit
{
    /// <summary>
    /// 上传参数
    /// </summary>
    public class UploadOption
    {
        /*optionType用于标识UploadOption的类型，即：普通上传、初始化分片上传、分片上传、分片上传完成*/
        public UpOptionType _optionType;

        /*以下属性是上传时的可选参数。即Rest API中Http请求Body中所需的可选参数*/
        public string _dir;                                    // 顽兔空间的图片路径(如果UploadPolicy中不指定dir属性，则生效)
        public string _name;                                   // 上传到服务端的文件名(如果UploadPolicy中不指定name属性，则生效)
        public string _metaArray;                              // 用户自定义的文件meta信息("meta-"为参数前缀, "*"为用户用于渲染的自定义Meta信息名)
        public string _varArray;                               // 用户自定义的魔法变量("var-"为参数前缀, "*"为用户用于渲染的自定义魔法变量名)
        private string _md5;                                   // 文件md5值(推荐提供此参数进行一致性检查)
        private string _size;                                  // 文件大小
        private string _content;                               // 文件内容(在http请求体Body中必须位于参数的最后一位)

        /*以下属性是用户根据自己应用需求，可选的配置*/
        public int _blockSize;                              // 文件分片的大小。针对分片上传。
        public int _timeout;                                // 进行http连接的超时时间
        public int _httpReTry;                              // http失败自动重试。0 or 1

        /*以下属性是用于分片上传时的参数，仅用于分片上传。用户在调用分片上传时可以选择配置。*/
        private string _uploadId;                              // OSS分片上传ID（OSS用于区分上传的id）
        private string _uniqueId;                              // 服务上传唯一ID（多媒体服务用于区分上传的id）
        private string _partNumber;                            // 分片文件块上传成功后返回的文件块编号
        private string _eTag;                                  // 分片文件块上传成功后返回的Tag标签(由md5和其他标记组成)
        private Dictionary<string, string> _array_PartNum_ETag;                    // 分片上传服务端返回的所有 块编号partNumber 和 标记ETag

        public UploadOption()
        {
            _optionType = UpOptionType.COMMON_UPLOAD_TYPE;//默认普通上传类型
            _metaArray = "";
            _varArray = "";
            _blockSize = Conf.BLOCK_DEFF_SIZE;   //默认2M
            _timeout = Conf.HTTP_TIMEOUT;        //默认超时30s
            _httpReTry = Conf.HTTP_RETRY;       //默认重试
            _array_PartNum_ETag = new Dictionary<string, string>();
        }
        /**得到上传时http请求体所需的参数*/
        public void getParaArray()
        {

        }
        /** 构造 普通上传 或者 初始化分片上传 所需的参数 */
        private void getParas_Common_BlockInit()
        {

        }
        /** 构造 分片上传过程中 所需的参数 */
        private void getParas_BlockRun()
        {

        }
        /** 构造 分片上传完成时 所需的参数 */
        private void getParas_BlockComplete()
        {

        }
        /** 构造 分片上传取消 所需的参数 */
        private void getParas_BlockCancel()
        {
        }

        /**设置待上传的数据。该方法开发者不需要调用，该方法根据用户数据自动调用
         * @param string $data 字符串 */
        private void setContent()
        {
        }
        /**设置MD5值。该方法主要用于在分片上传完成时调用
         * @param string $value md5值 */
        public void setMd5(string value)
        {
            _md5 = value;
        }
        /**得到MD5值。该方法主要用于在分片上传完成时调用*/
        public string getMd5()
        {
            return _md5;
        }

        /*下面四个函数均是用于分片上传时的设置，开发者不需要调用*/
        /**分片上传时用于设置uploadId */
        public void setUploadId(string value)
        {
            _uploadId = value;
        }
        /**分片上传时用于设置id */
        public void setUniqueIdId(string value)
        {
            _uniqueId = value;
        }
        /**分片上传时，用于获取分片上传时的块编号partNumber */
        public string getPartNumber()
        {
            return _partNumber;
        }
        /**分片上传时，用于设置分片上传时的块编号partNumber */
        public void setPartNumber(string value)
        {
            _partNumber = value;
        }
        /**分片上传过程中，用于保存所有的 块编号partNumber 和 标记ETag*/
        public void addPartNumberAndETag(string partNumber, string eTag)
        {
            _eTag = eTag;
            _array_PartNum_ETag.Add(partNumber, eTag);
        }
        /**检测分片上传的参数。即uploadId、uniqueId是否有值*/
        public Boolean checkMutipartParas()
        {
            return !string.IsNullOrEmpty(_uploadId) && !string.IsNullOrEmpty(_uniqueId);
        }
    }

    public enum UpOptionType
    {
        //下面的常量用于标识UploadOption对象适用的类型
        COMMON_UPLOAD_TYPE = 0,		//普通上传时的UploadOption类型
        BLOCK_INIT_UPLOAD,		//分片初始化时的UploadOption类型
        BLOCK_RUN_UPLOAD,			//分片上传过程中的UploadOption类型
        BLOCK_COMPLETE_UPLOAD,	//分片上传完成时的UploadOption类型
        BLOCK_CANCEL_UPLOAD		//分片上传取消时的UploadOption类型
    }
}
