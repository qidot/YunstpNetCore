using System;


/**
  * className:  MyKeyValue
  * descrption: 自定义键值对
  * author:     wangzh
  * created:    2019-07-19 14:45
  * --------------------------------------------------------
  * 日期           时间       修改人          说明
  * 2019-07-19    14:45      wangzh         初始化文件
  * 2019-08-26    19:12      wangzh         增加:自定义枚举值
  * --------------------------------------------------------
  **/
namespace yunstp.common.Result
{
    /// <summary>
    /// 自定义键值对
    /// </summary>
    public class MyKeyValue
    {
        public string Key { set; get; }

        public string Value { set; get; }
    }

    /// <summary>
    /// 自定义枚举值
    /// </summary>
    public class MyEnumValue {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 值(数字型)
        /// </summary>
        public int Value { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// 语言
        /// </summary>
        public string Language { set; get; }
    }
}
