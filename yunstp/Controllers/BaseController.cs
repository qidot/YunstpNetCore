using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using yunstp.common.Result;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace yunstp.Controllers
{
    [EnableCors("any")]//跨域
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 当前登录的用户
        /// </summary>
        //protected JwtUser CurrentJwtUser
        //{
        //    get { return JwtAuthConstants.GetJwtUser(); }
        //}

        /// <summary>
        /// 返回json格式的result
        /// </summary>
        /// <typeparam name="T">泛型数据</typeparam>
        /// <param name="data">接口返回的数据内容</param>
        /// <param name="mei">错误信息</param>
        /// <param name="dtf">时间格式</param>
        /// <param name="Params">额外的参数</param>
        /// <param name="IgnoreGroup">忽略JSON输出的组</param>
        /// <returns></returns>

        [ApiExplorerSettings(IgnoreApi = true)]
        public JsonResult ApiResult<T>(T data, MyErrorInfo mei = null, string dtf = "yyyy-MM-dd HH:mm:ss", List<MyKeyValue> Params = null, string IgnoreGroup = "") where T : class
        {
            return CommonResult.ApiResult<T>(data, mei, dtf, Params, IgnoreGroup);
        }


        /// <summary>
        /// 返回json格式的result
        /// </summary>
        /// <param name="code">错误枚举</param>
        /// <param name="subCode">子错误信息(描述的更加具体明确的消息)</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public JsonResult ApiResult(MyResultCode code, MyResultSubCode subCode)
        {
            string language = Request.Headers["Accept-Language"];
            if (string.IsNullOrEmpty(language)) {
                language = "zh-Hans";
            }
            return CommonResult.ApiResult(code, subCode, language);
        }
        /// <summary>
        /// 根据异常返回信息
        /// </summary>
        /// <param name="e">异常</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public JsonResult ApiResult(Exception e)
        {
            string language = Request.Headers["Accept-Language"];
            if (string.IsNullOrEmpty(language))
            {
                language = "zh-Hans";
            }
            return CommonResult.ApiResult(e,language);
        }


        /// <summary>
        /// 设置时间与更新人员相关信息
        /// modified by: wangzh 2019-08-29 10:03 增加对公司编号的赋值
        /// </summary>
        /// <param name="model">继承自IModel的实体</param>
        /// <param name="updateUser">更新人</param>
        /// <param name="coNo">公司编号</param>
        [ApiExplorerSettings(IgnoreApi = true)]
        public void SetModelUserAndTimeInfo(dynamic model, string updateUser, int? coNo = null)
        {

            model.UpdateTime = DateTime.Now;
            model.UpdateUser = updateUser;

            if (model.GetPrimaryKey() <= 0)
            {
                model.CreateTime = DateTime.Now;
                model.CreateUser = updateUser;
            }
            //对公司信息一并赋值
            if (model.IsPropertyExist(model, "CoNo") && coNo != null)
            {
                model.CoNo = coNo.Value;
            }

        }

    }
}
