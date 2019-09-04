# YunstpNetCore
(Netcore2.2)版的简单开发框架,更多信息请阅读 [wiki](https://github.com/qidot/YunstpNetCore/wiki)

##### NetCore版本

V2.2.X(X=301) 如果没有安装,请下载安装  https://dotnet.microsoft.com/download/dotnet-core/2.2



##### 工程介绍

| 序号 | 工程              | 说明                                            |
| ---- | ----------------- | ----------------------------------------------- |
| 1    | yunstp.common      | 基础&&公用的代码写在这里                        |
| 2    | yunstp.dapper | ORM（dapper改写版） |
| 3    | yunstp         | API 应用                                        |
| 4    | yunstp.data      | service,mapper,model,dto写在这               |
|      |                   |                                                 |



##### Mac Git 使用介绍

https://docs.microsoft.com/zh-cn/visualstudio/mac/working-with-git?view=vsmac-2019




##### 用到的库

| 序号 | 库                  | 说明                                                         |
| ---- | ------------------- | ------------------------------------------------------------ |
| 1    | ~~CAP~~             | 分布式事务总线 https://github.com/dotnetcore/CAP             |
| 2    | Dapper              | orm包  https://github.com/StackExchange/Dapper               |
| 3    | Stackexchange.redis | redis包 https://github.com/StackExchange/StackExchange.Redis |
| 4    | swagger             | swagger文档 https://github.com/domaindrivendev/Swashbuckle   |
| 5    | jwt                 | 授权 （netcore自带的auth+自己扩展版）                        |
| 6    | dapper.contrib      | 修改版                                                       |
| 7    | dapper.sqlbuilder   | 修改版                                                       |
| 8    | Quartz.net          | 定时任务  https://github.com/quartznet/quartznet             |
| 9    | aliyun-oss          | 存储服务 https://github.com/qidot/aliyun-oss-csharp-sdk      |
| 10   | ~~kafka~~           | 消息队列 https://github.com/confluentinc/confluent-kafka-dotnet |
| 11   | RocketMq            | 阿里云MQ                                                     |
| 12   | Mongodb             | 日志存储                                                     |
| 16   | NPOI                | EXCEL导出导入组件  https://github.com/dotnetcore/NPOI        |
| 17   | MailKit             | 邮件组件  https://github.com/jstedfast/MailKit               |
|      |                     |                                                              |



##### 启动必备

| 序号 | 名称                        | 说明                                                         |
| ---- | --------------------------- | ------------------------------------------------------------ |
| 1    | Appsetting.Development.json | 用appsetting.kaifa.json 复制一份 Appsetting.Development.json避免修改冲突 |
| 2    | REDIS                       | 缓存存储器                                                   |
|      |                             |                                                              |



##### PropertyColumnMapper 映射

用于实现select * from table  中  字段带有下划线的映射

```c#
//3要素
1.实现IModel接口,2.配合[Table],3.配合[Column]
```



##### 获取JWT当前用户的方法

```c#
JwtAuthConstants.GetJwtUser()
或者
在控制器中用 CurrentJwtUser
```



##### Oss 用法

```c#
var localFile = @"/Users/wangzh/Downloads/youzan.jpg";  //等待上传的文件
var remoteFile = @"youzan.jpg";  //上传到oss端的文件名
using (var aliOss = new AliyunOssHelper(_aliossSetting)) {
  if ("upload".Equals(type))
  {
    //上传后返回一个可以访问的url地址
    return aliOss.upload(remoteFile, localFile,"public-read");
  }
  else if ("down".Equals(type))
  {
    //下载一个文件
    aliOss.download(remoteFile);
  }
  else if ("delete".Equals(type))
  {
    //删除
    aliOss.delete(remoteFile);
  }
  else if ("get".Equals(type)) {
    //获取一个url地址
    return aliOss.getUrl(remoteFile);
  }

  return "ok";
}
```



##### 模型绑定校验

> 需要在属性上增加指定校验的特性标签
> 下方给出了: 数字型和字符型的校验示例

```c#

[Range(0, 100, ErrorMessage = "Id数不能大于100")]
public int id { set; get; }

[StringLength(50, ErrorMessage = "创建人最长可以50个字")]
public string createUser { set; get; }

//在某个方法中
public ActionResult<string> Add([FromBody] TestWorker model)
{
  return "OK";
}
//当我提交的信息是超过校验范围的时候,会被拦截
//如
{
  "id": 10000   //明显超出了id在 0~100之间的限制
}
```

> 会返回统一格式的错误提示

```json
{
    "code": 400,
    "isSuccess": false,
    "message": "参数验证失败,Id数不能大于100,创建人最长可以50个字",
    "msg": "参数验证失败,Id数不能大于100,创建人最长可以50个字"
}
```