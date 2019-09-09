using System;
using System.Collections.Generic;
using System.Dynamic;
using yunstp.common.Helper;

namespace yunstp.common.Extensions
{
    public static class ObjectExtends
    {
        /// <summary>
        /// 对象转为json字符串
        /// </summary>
        /// <returns>The json.</returns>
        /// <param name="obj">Object.</param>
        public static string ToJson(this object obj)
        {
            return JsonHelper.ToJson(obj);
        }
        public static int ToInt(this object obj)
        {
            return (int)obj;
        }

        public static bool IsPropertyExist(this object data, string propertyname)
        {
            if (data is ExpandoObject)
            {
                return ((IDictionary<string, object>)data).ContainsKey(propertyname);
            }
            return data.GetType().GetProperty(propertyname) != null;
        }
    }
}
