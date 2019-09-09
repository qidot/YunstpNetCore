using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NLog;
using yunstp.common.Exceptions;
using yunstp.common.Result;

namespace rplus.common.exceptions
{
    /**
      * className:  MyGolbalExceptionFilter
      * descrption: 全局异常捕捉
      * author:     wangzh
      * created:    2019-08-16 09:54
      * --------------------------------------------------------
      * 日期           时间       修改人          说明
      * 2019-08-16    09:54      wangzh         初始化文件    
      * --------------------------------------------------------
      **/
    public class MyGolbalExceptionFilter : IExceptionFilter
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        readonly ILoggerFactory _loggerFactory;
        readonly IHostingEnvironment _env;

        public MyGolbalExceptionFilter(ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            _loggerFactory = loggerFactory;
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            //var isAjax = context.HttpContext.Request.IsAjaxRequest();
            //var method = context.HttpContext.Request.Method;
            //var requestPath = context.HttpContext.Request.Path.Value;
            string errorMessage = context.Exception.Message,fullException = context.Exception.StackTrace;
            //全局异常错误码
            var errorCode    = ((int)MyResultCode.GLOBAL_EXCEPTION).ToString();
            if (context.Exception.GetType() == typeof(MyException)) {
                errorCode = ((MyException)context.Exception).Code;
            }
            //友好化处理
            errorMessage = MyExceptionUtil.GetFriendlyMessage(errorMessage, fullException);
            //如果是
            if (_env.IsDevelopment())
            {
                errorMessage += "\n"+ fullException;
            }
            //返回值处理
            context.Result = CommonResult.ApiResult<string>(
                null,
                new MyErrorInfo {
                    Code = errorCode,
                    Message = errorMessage
                }
                );
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            context.ExceptionHandled = true;
        }
    }
    public class ApplicationErrorResult : ObjectResult
    {
        public ApplicationErrorResult(object value) : base(value)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }

    //public class ErrorResponse
    //{
    //    public ErrorResponse(string msg)
    //    {
    //        Message = msg;
    //    }
    //    public string Message { get; set; }
    //    public object DeveloperMessage { get; set; }
    //}
}
