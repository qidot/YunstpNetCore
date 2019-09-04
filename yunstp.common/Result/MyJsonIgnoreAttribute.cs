using System;
namespace yunstp.common.Result
{
    /**
      * className:  MyJsonIgnoreAttribute
      * descrption: 自定义的JSON字段忽略属性(用于输出)
      * author:     wangzh
      * created:    2019-08-23 10:29
      * --------------------------------------------------------
      * 日期           时间       修改人          说明
      * 2019-08-23    10:29      wangzh         初始化文件    
      * --------------------------------------------------------
      **/
    public class MyJsonIgnoreAttribute: Attribute
    {
        /// <summary>
        /// 需要忽略的份额组信息,多个忽略组用英文逗号分隔
        /// 如: wx,ding
        /// </summary>
        public string Group { set; get; }
    }
}
