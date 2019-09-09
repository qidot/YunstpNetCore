using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using rplus.common.exceptions;
using yunstp.common.Enums;
using yunstp.common.Exceptions;
/**
* className:  ApiResultDTO
* descrption: 公有的输出类
* author:     wangzh
* create by:  2019-07-19 14:44 
* --------------------------------------------------------
* 日期           时间       修改人          说明
* 2019-07-19    14:44      wangzh         初始化文件
* 2019-08-26    09:51      wangzh         增加子错误信息
* --------------------------------------------------------
**/
namespace yunstp.common.Result
{
    /// <summary>
    /// 统一的结果类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResultDTO<T>
    {
        /// <summary>
		///     是否成功
		/// </summary>
		public bool IsSuccess { set; get; }

        /// <summary>
        ///     错误码
        /// </summary>
        public string Code { set; get; }

        /// <summary>
        ///     错误的信息
        /// </summary>
        public string Message { set; get; }

        /// <summary>
        ///     子错误码
        /// </summary>
        public string SubCode { set; get; }

        /// <summary>
        ///     子错误消息
        /// </summary>
        public string SubMessage { set; get; }

        /// <summary>
        ///     成功的返回值
        /// </summary>
        public T Data { set; get; }

        public List<MyKeyValue> Params { set; get; } = null;
    }

    /// <summary>
    /// 统一放回方法,在非controller中可用
    /// </summary>
    public class CommonResult
    {
        /// <summary>
        /// JSON结果的基础方法
        /// </summary>
        /// <typeparam name="T">数据对象的泛型</typeparam>
        /// <param name="data">数据</param>
        /// <param name="error">错误对象</param>
        /// <param name="dtf">日期格式,默认:yyyy-MM-dd HH:mm:ss</param>
        /// <param name="Params">扩展信息</param>
        /// <param name="IgnoreGroup">输出忽略组,默认:空字符,非空字符的时候参与运算</param>
        /// <returns></returns>
        public static JsonResult ApiResult<T>(T data, MyErrorInfo error=null, string dtf = "yyyy-MM-dd HH:mm:ss", List<MyKeyValue> Params = null,string IgnoreGroup="") where T: class
        {

            var jSetting = new JsonSerializerSettings();
            var dtConverter = new IsoDateTimeConverter { DateTimeFormat = dtf };
            jSetting.MissingMemberHandling = MissingMemberHandling.Ignore;
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            jSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jSetting.Converters.Add(dtConverter);
            //自定义的解析器
            jSetting.ContractResolver = new MyResponseContractResolver(IgnoreGroup);

            string code = "0", subCode = "0", message = "", subMessage = "";
            if (error != null) {
                code = error.Code;
                subCode = error.SubCode;
                message = error.Message;
                subMessage = error.SubMessage;
            }

            return new JsonResult(new ApiResultDTO<T>
            {
                Code = code,
                SubCode = subCode,
                IsSuccess = "0".Equals(code, StringComparison.CurrentCultureIgnoreCase),
                Message = message,
                SubMessage = subMessage,
                Data = data,
                Params = Params
            }, jSetting);
        }

        /// <summary>
        /// 根据结果编码枚举返回
        /// </summary>
        /// <param name="code">返回码枚举</param>
        /// <returns></returns>
        public static JsonResult ApiResult(MyResultCode code,  MyResultSubCode subCode,string language="zh-Hans")
        {
            return ApiResult<string>(null, new MyErrorInfo {
                Code = ((int)code).ToString(),
                SubCode = ((int)subCode).ToString(),
                Message = EnumHelper.GetDescription(code, language),
                SubMessage = EnumHelper.GetDescription(subCode, language)
        });
        }

        /// <summary>
        /// 根据异常构建返回
        /// </summary>
        /// <param name="e">异常</param>
        /// <returns></returns>
        public static JsonResult ApiResult(Exception e,string language="zh-Hans")
        {
            if (e is MyException)
            {
                var me = (MyException)e;
                return ApiResult<string>(null, new MyErrorInfo {
                    Code = me.Code,
                    Message = me.Message,
                    SubMessage = me.SubMessage,
                    SubCode = me.SubCode
                });
            }

            //解析错误得到字典
            var errorDict = MyExceptionUtil.GetFriendlyMessage(e,language);

            return ApiResult<string>(null,
                new MyErrorInfo
                {
                    Code = errorDict["code"],
                    Message = errorDict["message"]
                }
               );
        }


    }


    public class MyErrorInfo {
        public string Code { set; get; } = "0";
        public string SubCode { set; get; } = "0";
        public string Message { set; get; } 
        public string SubMessage { set; get; }
    }
    
}
