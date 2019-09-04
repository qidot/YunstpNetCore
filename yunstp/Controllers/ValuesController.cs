using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NLog;
using yunstp.common.Helper;
using yunstp.data;

namespace yunstp.Controllers
{
    /**
      * className:  ValuesController
      * descrption: 访问示例
      * author:     wangzh
      * created:    2019-09-02 16:45
      * --------------------------------------------------------
      * 日期           时间       修改人          说明
      * 2019-09-02    16:45      wangzh         初始化文件    
      * --------------------------------------------------------
      **/
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //公用的本地化
        private readonly IStringLocalizer _localizer;
        //日志
        private readonly ILogger<ValuesController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="localizer"></param>
        public ValuesController(IStringLocalizer localizer, ILogger<ValuesController> logger
            ) {
            _localizer = localizer;
            _logger = logger;
        }

        /// <summary>
        /// 日志测试
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost("log")]
        public IActionResult log([FromBody]string message) {
            //var str = message ?? "你好啊,这是一个默认的日志消息";
            _logger.LogDebug($"DI的日志输出======>{message}");
            this.GetLogger().Debug($"NLog自定义输出====>{message}");

            return Ok("OK");
        }

        /// <summary>
        /// 多语言(Localization)处理示例
        /// </summary>
        /// <returns>不同语言下的返回信息</returns>
        [HttpGet("loc")]
        public ActionResult loc()
        {
            string hello = _localizer["hello"];
            return Ok(hello);
        }


        private  bool GMTStrParse(string gmtStr, out DateTime gmtTime)
        {
            //CultureInfo enUS = new CultureInfo("zh-cn");

            bool s = DateTime.TryParseExact(gmtStr, "r", CultureInfo.InvariantCulture, DateTimeStyles.None, out gmtTime);
            return s;
        }

        private DateTime getDateFromFormat(string _date, string _parsePattern)
        {
            DateTimeOffset dto = DateTimeOffset.ParseExact(_date, _parsePattern, CultureInfo.InvariantCulture);
            return Convert.ToDateTime(dto.ToLocalTime());
        }

        [HttpGet("setLanguage")]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        /// <summary>
        /// DataAnnotation数据校验多语言示例
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("valid")]
        public IActionResult ValidateDemo([FromBody] AppDemoDTO dto)
        {
            return Ok(dto.Name);
        }

        

    }
}
