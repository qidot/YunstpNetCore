using System;
namespace yunstp.common.Enums
{
    public enum MyHttpStatusCode
    {
        [EnumDescription("未授权")]
        NoAuth = 401,
        [EnumDescription("路径不存在")]
        NotFound = 404,
    }
}
