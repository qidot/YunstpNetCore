using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Linq;
using yunstp.common.Result;

/**
  * className:  EnumHelper
  * descrption: 枚举帮助类
  * author:     wangzh
  * created:    2019-07-19 10:45
  * --------------------------------------------------------
  * 日期           时间       修改人          说明
  * 2019-07-19    10:45      wangzh         初始化文件    
  * --------------------------------------------------------
  **/

namespace yunstp.common.Enums
{
    public static class EnumHelper
    {
        /// <summary>
        /// 转成MAP
        /// </summary>
        /// <param name="t">类型</param>
        /// <param name="language">语言</param>
        /// <returns>Hashtable</returns>
        public static Hashtable Convert2Hashtable(Type t,string language="zh-Hans") {
            var cacheKey = $"{t.Name}.{language}";
            if (EnumConstants.Map.ContainsKey(cacheKey))
            {
                return EnumConstants.Map[cacheKey];
            }
            Hashtable ht = new Hashtable();
            var list = GetValuesWithCache(t,language);
            foreach (var v in list)
            {
                if (!ht.Contains(v.Value))
                {
                    ht.Add(v.Value, v.Description);
                }
            }
            EnumConstants.Map[cacheKey] = ht;
            return ht;
        }

        /// <summary>
        /// 解析枚举的值
        /// </summary>
        /// <param name="t">枚举类型</param>
        /// <param name="language">语言</param>
        /// <returns></returns>
        private static List<MyEnumValue> GetValues(Type t,string language="zh-CN")
        {
			var typeInfo = t.GetTypeInfo();
			var enumValues = typeInfo.GetEnumValues();
            var fields = t.GetFields(BindingFlags.Static | BindingFlags.Public);
            List<MyEnumValue> values = new List<MyEnumValue>();
            foreach (var fi in fields)
            {
                string key = fi.Name;
                // cast the one and only attribute to EnumDescriptionAttribute
                var list = (fi.GetCustomAttributes<EnumDescriptionAttribute>()).ToList();
                var attrib = list.Find(P => P.Language.Equals(language));
                if (attrib == null) { continue; }
                values.Add(new MyEnumValue
                {
                    Name = key,
                    Description = attrib.Description,
                    Language = language,
                    Value = (int)fi.GetValue(fi)
                });
            }

            EnumConstants.EnumValues[$"{t.Name}.{language}"] = values;

            return values;
        }

        /// <summary>
        /// 带缓存获取
        /// </summary>
        /// <param name="t">类型</param>
        /// <param name="language">语言</param>
        /// <returns></returns>
        public static List<MyEnumValue> GetValuesWithCache(Type t, string language = "zh-Hans") {
            var cacheKey = $"{t.Name}.{language}";
            List<MyEnumValue> caches;
            if (EnumConstants.EnumValues.ContainsKey(cacheKey))
            {
                caches = EnumConstants.EnumValues[cacheKey];
            }
            else
            {
                caches = GetValues(t, language);
            }

            return caches;
        }

        /// <summary>
        /// 返回枚举项的描述信息。
        /// </summary>
        /// <param name="value">要获取描述信息的枚举项。</param>
        /// <param name="language">语言</param>
        /// <returns>枚举想的描述信息。</returns>
        public static string GetDescription(System.Enum value,string language="zh-Hans")
        {
            Type enumType = value.GetType();
            var v = GetValuesWithCache(enumType,language).Find(P => P.Name.Equals(value.ToString()));
            if (v != null) { return v.Description; }
            return null;
        }

        public static string GetDescription(int v, Type type, string language = "zh-Hans")
        {
            var find = GetValuesWithCache(type, language).Find(P => P.Value.Equals(v));
            if (find != null) { return find.Description; }
            return null;
        }

    }
}
