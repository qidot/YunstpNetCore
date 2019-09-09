using System;
using System.Collections.Generic;
using NLog;

namespace rplus.common.exceptions
{
    /**
      * className:  MyExceptionUtil
      * descrption: 异常信息友好化处理
      * author:     wangzh
      * created:    2019-08-16 09:30
      * --------------------------------------------------------
      * 日期           时间       修改人          说明
      * 2019-08-16    09:30      wangzh         初始化文件
      * 2019-08-28    14:10      wangzh         增加捕捉异常到mongo中
      * --------------------------------------------------------
      **/
    public class MyExceptionUtil
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 得到一个友好化的提示信息
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="fullException">全量的异常内容</param>
        /// <returns>友好信息</returns>
        public static string GetFriendlyMessage(string message,string fullException=null,string language="zh-Hans") {
            var dict = GetFriendlyMessageDict(message, fullException, language);
            return dict["message"];
        }


        /// <summary>
        /// 增加捕捉异常到mongo中
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fullException"></param>
        /// <returns></returns>
        public static Dictionary<string,string> GetFriendlyMessageDict(string message, string fullException = null, string language = "zh-Hans")
        {
            logger.Error($"捕捉到异常===========>{language},{message},{fullException}");
            var dict = new Dictionary<string, string> {
                {"code","-1" },
                { "message",message}
            };
            if (message.StartsWith("Duplicate entry", StringComparison.Ordinal))
            {
                //主键重复/唯一索引重复
                dict["code"] = "115";
                dict["message"] = "插入了重复的数据";
            }
            else if ("Attempted to divide by zero.".Equals(message)) {
                dict["code"] = "113";
                dict["message"] = "不能被0除";
            }

            //记录到mongo里面
            //if (!AutoConfiguration.IsDevelopment)
            //{
            //    using (var mongoRepository = new MongoRepository<ExceptionLog>(AutoConfiguration.mongoSetting))
            //    {
            //        mongoRepository.Add(new ExceptionLog
            //        {
            //            Id = Guid.NewGuid(),
            //            FriendlyCode = dict["code"],
            //            FriendlyMessage = dict["message"],
            //            Message = message,
            //            FullException = fullException,
            //            Time = DateTime.Now
            //        });
            //    }
            //}
            


            return dict;
        }



        /// <summary>
        /// 得到一个友好化的提示信息
        /// </summary>
        /// <param name="e">异常</param>
        /// <returns>友好信息</returns>
        public static Dictionary<string, string> GetFriendlyMessage(Exception e,string language="zh-Hans") {
            return GetFriendlyMessageDict(e.Message, e.StackTrace, language);
        }
    }
}
