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
using RestSharp;
using yunstp.common.Extensions;
using yunstp.common.Graph;
using yunstp.common.Helper;
using yunstp.common.Result;
using yunstp.data;
using yunstp.data.dto;
using yunstp.data.model;

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
    public class ValuesController : BaseController
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
        public IActionResult loc()
        {
            string l = string.IsNullOrEmpty(Request.Headers["Accept-Language"]) ? "zh-Hans" : Request.Headers["Accept-Language"].ToString();
            try
            {
                string hello = _localizer["hello"];
                return Ok(hello);
            }
            catch (Exception ex) {
                return CommonResult.ApiResult(ex,l);
            }
            
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

        [HttpPost("findCycle")]
        public IActionResult FindCycle([FromBody]List<string> nodes) {
            //false=时候表示有闭环
            var result = GraphHelper.CheckDigraphLoop(nodes);
            return Ok(result);
        }

        [HttpPost("rest")]
        public IActionResult RestClient([FromBody] int type) {


            //var client = new RestClient("http://form.r-vision-group.com/api/v1/Forms?page=0&pageSize=10&sort=CreatedTime&sortOrder=desc&search=");
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Postman-Token", "d4357baf-7716-4939-92db-e6025e6015e5");
            //request.AddHeader("Cache-Control", "no-cache");
            //request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImR5bGFuaHVAcnBsdXMuY29tIiwidXNlcklkIjoiMiIsInRlbmFudElkIjoiMSIsIm5iZiI6MTU2MDczNzkyOSwiZXhwIjoxNTkyMjczOTI5LCJpc3MiOiJ0ZXN0MTIzIiwiYXVkIjoidGVzdDEyMyJ9.qwiCjOf2hL7zpWRPx1qdv1z7jDXcUkegUe0Ln_TUL2E");
            //IRestResponse response = client.Execute(request);


            if (type == 1)
            {
                MyRestHelper client = new MyRestHelper(
                     "http://form.r-vision-group.com/api/v1/Forms"
                     );

                var headers = new Dictionary<string, string>
                {
                    { "Authorization","Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImR5bGFuaHVAcnBsdXMuY29tIiwidXNlcklkIjoiMiIsInRlbmFudElkIjoiMSIsIm5iZiI6MTU2MDczNzkyOSwiZXhwIjoxNTkyMjczOTI5LCJpc3MiOiJ0ZXN0MTIzIiwiYXVkIjoidGVzdDEyMyJ9.qwiCjOf2hL7zpWRPx1qdv1z7jDXcUkegUe0Ln_TUL2E"}
                };

                //?page=0&pageSize=10&sort=CreatedTime&sortOrder=desc&search=
                var p = new Dictionary<string, string>
                {
                    { "page","0"},
                    { "pageSize","20"},
                    { "sort","CreatedTime"},
                    { "sortOrder","desc"},
                    { "search","" }
                };

                //发送一个GET请求
                var getResponse = client.ExecuteResponse<dynamic>(headers, Method.GET,p);

                if (getResponse != null && getResponse.IsSuccessful)
                {
                    return Ok(JsonHelper.ToJson(getResponse.Data));
                }
            }
            else if (type == 2) {
                MyRestHelper client = new MyRestHelper(
                     "http://form.r-vision-group.com/api/v1/UserForms"
                     );

                var headers = new Dictionary<string, string>
                {
                    { "Authorization","Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImR5bGFuaHVAcnBsdXMuY29tIiwidXNlcklkIjoiMiIsInRlbmFudElkIjoiMSIsIm5iZiI6MTU2MDczNzkyOSwiZXhwIjoxNTkyMjczOTI5LCJpc3MiOiJ0ZXN0MTIzIiwiYXVkIjoidGVzdDEyMyJ9.qwiCjOf2hL7zpWRPx1qdv1z7jDXcUkegUe0Ln_TUL2E"}
                };


                //发送一个POST请求
                var postResponse = client.ExecuteResponse<dynamic>(headers, Method.POST, new {
                    FormId= "12",
                    SourceType= "Task",
                    SourceId = "1"
                });

                if (postResponse != null)
                {
                    if (postResponse.IsSuccessful)
                    {
                        return Ok(JsonHelper.ToJson(postResponse.Data));
                    }
                    else {
                        return Ok(JsonHelper.ToJson(postResponse.Data));
                    }
                }
            }
            


            return Ok("ddd");
        }

        [HttpPost("copyExample")]
        public IActionResult CopyExample([FromBody]TestWorkerQueryDTO testWorkerDTO)
        {
            Console.WriteLine($"dto=====>{testWorkerDTO.ToJson()}");
            Console.WriteLine($"复制实例");
            var model = WAutoMapper<TestWorkerQueryDTO, TestWorker>.Map(testWorkerDTO);
            Console.WriteLine($"model====>{model.ToJson()}");
            Console.WriteLine($"正常实例");
            var model2 = new TestWorker
            {
                id = (int)testWorkerDTO.id,
                createTime = testWorkerDTO.createTime,
                name = testWorkerDTO.name,
                age = testWorkerDTO.age
            };

            Console.WriteLine($"model====>{model2.ToJson()}");
            return ApiResult(model);
        }

    }
}
