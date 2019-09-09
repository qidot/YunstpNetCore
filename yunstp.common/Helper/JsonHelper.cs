using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NLog;
/**
* className:  JsonHelper
* descrption: JSON转换的类
* author:     wangzh
* created:    2019-07-19 10:42
* --------------------------------------------------------
* 日期           时间       修改人          说明
* 2019-07-19    10:42      wangzh         初始化文件    
* --------------------------------------------------------
**/
namespace yunstp.common.Helper
{
    public static class JsonHelper
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly JsonSerializerSettings _jsonSettings;

        static JsonHelper()
        {
            var datetimeConverter = new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            };

            _jsonSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            _jsonSettings.Converters.Add(datetimeConverter);
        }

        /// <summary>
        /// 将指定的对象序列化成 JSON 数据。
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            try
            {
                return null == obj ? null : JsonConvert.SerializeObject(obj, Formatting.None, _jsonSettings);
            }
            catch (Exception ex)
            {
                logger.Error($"对象转Json字符串:{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 将指定的 JSON 数据反序列化成指定对象。
        /// </summary>
        /// <typeparam name="T">泛型对象类型</typeparam>
        /// <param name="json">JSON 数据字符串</param>
        /// <returns></returns>
        public static T FromJson<T>(string json)
        {
            try
            {
               
                return string.IsNullOrEmpty(json) ? default(T) : JsonConvert.DeserializeObject<T>(json, _jsonSettings);
            }
            catch (Exception ex)
            {
                logger.Error($"Json序列化:{ex.Message},字符串:{json}");
                return default(T);
            }
        }

        public static bool IsJson(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return false;
            }
            try
            {
                JsonConvert.DeserializeObject(json, _jsonSettings);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //public static T DeserializeJsonToObject<T>(RedisValue value) where T : class
        //{
        //    return FromJson<T>(ToJson(value));
        //}
    }
}
