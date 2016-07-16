#阿里百川多媒体服务.NET封装（ALBC_REST_SDK）

* 自己之前有个百川账号，刚好开通了顽兔多媒体，瞅着每月100G流量和存储来的
* 官方有安卓、IOS
* java，PHP，js等等的sdk封装已经很方便了
* 但是由于我自己水平有限，只熟悉C#和C++的语言，所以一直想找一个.NET封装的，但是一直没找到
* 之前由于对REST API从未接触过，HTTP通信方面也从未涉猎，第一次有自己弄个SDK的念头的时候被官方文档给吓回去了
* 当时自己就下载了一个官方java的SDK包，然后用IKVM.NET技术弄了个C#的dll用
* 最近有时间了，刚好也看到RestSharp，就拿来用用
* 很简单的封装，没啥技术水准和难度，就是方便使用而已，能力有限，代码拙劣


##使用方法

```javascript
   ALBC_REST_SDK.ALBCClient _client = new ALBC_REST_SDK.ALBCClient(ak, sk, namespace);
   _client.UpLoadFileBlock(_fileFillName);//分片上传,2M一片
   _client.UpLoadFile(_fileFillName);//分片上传
```