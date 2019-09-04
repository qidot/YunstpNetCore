using System;
using System.Collections.Generic;
using System.Text;

namespace yunstp.common.Enum
{
    /**
      * className:  EnumDescriptionAttribute
      * descrption: 枚举描述属性
      * author:     wangzh
      * created:    2019-08-26 19:03
      * --------------------------------------------------------
      * 日期           时间       修改人          说明
      * 2019-08-26    19:03      wangzh         初始化文件    
      * --------------------------------------------------------
      **/
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public class EnumDescriptionAttribute : Attribute
    {
        public EnumDescriptionAttribute(string desc,string lang="zh-Hans")
        {
            this.Description = desc;
            this.Language = lang;
        }

        /// <summary>
        /// 对应的描述
        /// </summary>
        public string Description
        {
            set;get;
        }
        /// <summary>
        /// 语言
        /// </summary>
        public string Language {
            set; get;
        }
    }
}
