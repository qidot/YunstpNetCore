using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using rplus.common.exceptions;
using yunstp.common.Enums;
using yunstp.common.Helper;
using yunstp.common.Result;
//指定别名
using HHttpContext = Microsoft.AspNetCore.Http.HttpContext;

namespace yunstp.common.Exceptions
{
    public class MyErrorHandlingMiddleware
    {

        private readonly RequestDelegate next;

        public MyErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HHttpContext context)
        {
            string language = context.Request.Headers["Accept-Language"];
            if (string.IsNullOrEmpty(language)) {
                language = "zh-Hans";
            }
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.StackTrace);
                //this.GetLogger().Error(string.Format("全局异常中间件捕捉:{0}\r\n详细错误:\r\n{1}", ex.Message, ex));
                var dict = MyExceptionUtil.GetFriendlyMessage(ex, language);
                var statusCode = context.Response.StatusCode;
                if (ex is ArgumentException || ex is MyException)
                {
                    statusCode = 200;
                }
                await HandleExceptionAsync(context, statusCode, dict["message"]);
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                var msg = EnumHelper.GetDescription(statusCode,typeof(MyHttpStatusCode), language);
                //401 未授权 
                //if (statusCode == 401)
                //{
                //    msg =  "未授权";
                //}
                //else if (statusCode == 404)
                //{
                //    msg = "未找到服务";
                //}
                //else if (statusCode == 502)
                //{
                //    msg = "请求错误";
                //}
                //else if (statusCode != 200)
                //{
                //    msg = "未知错误";
                //}
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    await HandleExceptionAsync(context, statusCode, msg);
                }
            }
        }

        private static Task HandleExceptionAsync(HHttpContext context, int statusCode, string msg)
        {
            var data = new ApiResultDTO<string>{ Code = statusCode.ToString(), IsSuccess = false, Message = msg };
            var result = JsonHelper.ToJson(data); // JsonConvert.SerializeObject(data);
            context.Response.ContentType = "application/json;charset=utf-8";
           
            return context.Response.WriteAsync(result);
        }
    }
    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyErrorHandlingMiddleware>();
        }
    }
}
