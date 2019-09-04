using System;
using NLog;

namespace yunstp.common.Helper
{
    public class LogHelper
    {

    }

    /// <summary>
    /// 日志扩展
    /// </summary>
    public static class MyLogExtensions {
        /// <summary>
        /// 获取当前的日志信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Logger GetLogger(this object value)
        {
            return LogManager.GetCurrentClassLogger(value.GetType());
        }
    }

}
