using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using NLog.Web;
using yunstp.common;
using yunstp.common.Cors;

namespace yunstp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            #region Enable CORS1
            services.AddCors(
                options =>
                {
                    options.AddPolicy("any", builder =>
                    {
                        builder.AllowAnyOrigin() //允许任何来源的主机访问//builder.WithOrigins("http://localhost:8080") ////允许http://localhost:8080的主机访问
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();//指定处理cookie
                    });
                });
            #endregion

            //全球化-本地化
            //指定本地化的resx配置的存放目录
            //文件名格式:filename.xx-XX.resx
            //中间的[xx-XX]部分,要参考这个网址:https://www.ietf.org/rfc/rfc4646.txt  ,人为修改的是出不来效果的
            //参考网址2:     https://www.haomeili.net/QuYuDetail?wd=zh-hant
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddDataAnnotationsLocalization(options =>
                {
                    //全球化-本地化
                    //MVC模式的时候,在视图中渲染的时候生效(具体没有测试)
                    //API模式的时候,[这个在校验返回失败的时候,是无效的,2.2进行了前置处理,所以需要特殊处理]
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                })
                .AddMvcLocalization()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix) //配置视图的 全球化-本地化
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)      //设定编译版本:2.2
                .ConfigureApiBehaviorOptions(
                    //配置DataAnnotations的校验行为
                    options =>
                    {
                        options.InvalidModelStateResponseFactory = context =>
                        {
                            //全球化-本地化的驱动器(否则无法自动渲染本地化配置)
                            var _localizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<DataValidResource>>();
                            //设定参数校验行为的返回信息
                            var problemDetails = new ValidationProblemDetails(context.ModelState);
                            //从本地化中实例错误信息处理
                            string errstr = "" + problemDetails.Errors.SelectMany(err => err.Value).Aggregate("", (current, e) => current + "," + _localizer[e]);
                            //去除掉最后一位的逗号
                            errstr = errstr.Trim().Trim(',');
                            //返回通用的信息类
                            return new JsonResult(new { message = errstr });
                        };
                    }
                );

            //全球化-本地化:::注册本地化服务，用于全局调用（单一实例）
            services.AddSingleton<IStringLocalizer>((sp) =>
            {
                var sharedLocalizer = sp.GetRequiredService<IStringLocalizer<SharedResource>>();
                return sharedLocalizer;
            });

            //全球化-本地化:::自定义支持的语言集合
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("zh-Hans"),
                        new CultureInfo("fr")
                    };
                options.DefaultRequestCulture = new RequestCulture("zh-Hans");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region Nlog
            //使用NLog作为日志记录工具
            loggerFactory.AddNLog();
            //引入Nlog配置文件(根据当前运行环境加载)
            env.ConfigureNLog($"nlog.{env.EnvironmentName}.config");
            #endregion

            #region 全球化-本地化
            var supportedCultures = new[] {
                new CultureInfo("en-US"),
                new CultureInfo("zh-Hans"),
                new CultureInfo("fr")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("zh-Hans"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures

            });
            #endregion

            #region cors && options
            app.UseCors("any");  //跨域配置
            app.UseOptions();    //用于对VUE,REACT的前后开发模式,会OPTIONS方式先预请求
            #endregion

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
