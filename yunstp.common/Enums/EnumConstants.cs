using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using yunstp.common.Result;

namespace yunstp.common.Enums
{
    /**
      * className:  EnumConstants
      * descrption: 枚举值缓存类
      * author:     wangzh
      * created:    2019-08-26 19:13
      * --------------------------------------------------------
      * 日期           时间       修改人          说明
      * 2019-08-26    19:13      wangzh         初始化文件    
      * --------------------------------------------------------
      **/
    public class EnumConstants
    {
        /// <summary>
        /// 枚举值缓存
        /// </summary>
        public static ConcurrentDictionary<string, List<MyEnumValue>> EnumValues = new ConcurrentDictionary<string, List<MyEnumValue>>();

        /// <summary>
        /// map缓存
        /// </summary>
        public static ConcurrentDictionary<string, Hashtable> Map = new ConcurrentDictionary<string, Hashtable>();

        /// <summary>
        /// 字典缓存
        /// </summary>
        public static ConcurrentDictionary<string, Dictionary<int,string>> Dict = new ConcurrentDictionary<string, Dictionary<int, string>>();
    }
}
