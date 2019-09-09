using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace yunstp.common.Cors
{
    /**
      * className:  MyOptionsMiddleware
      * descrption: 用于放开前后分离模式的options请求
      * author:     wangzh
      * created:    2019-08-12 13:08
      * --------------------------------------------------------
      * 日期           时间       修改人          说明
      * 2019-08-12 13:08               wangzh         初始化文件
      * 2019-08-19 10:23         wangzh         放开所有的来源,缩减请求方式集合
      * --------------------------------------------------------
      **/
    public class MyOptionsMiddleware
    {
        private readonly RequestDelegate _next;

        public MyOptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            return BeginInvoke(context);
        }

        private Task BeginInvoke(HttpContext context)
        {
            if (context.Request.Method == "OPTIONS")
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" }); //(string)context.Request.Headers["Origin"]
                context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin, X-Requested-With, Content-Type, Accept" });
                context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET, POST, OPTIONS" }); //PUT, DELETE,
                context.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
                context.Response.StatusCode = 200;
                return context.Response.WriteAsync("OK");
            }
            return _next.Invoke(context);
        }
    }
    public static class OptionsMiddlewareExtensions
    {
        public static IApplicationBuilder UseOptions(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyOptionsMiddleware>();
        }
    }
}
