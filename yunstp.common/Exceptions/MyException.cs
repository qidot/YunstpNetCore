using System;
using yunstp.common.Enums;
using yunstp.common.Result;

namespace yunstp.common.Exceptions
{
    /**
      * className:  MyException
      * descrption: 自定义异常信息
      * author:     wangzh
      * created:    2019-08-27 17:01
      * --------------------------------------------------------
      * 日期           时间       修改人          说明
      * 2019-08-07    10:01      wangzh         初始化文件
      * 2019-08-27    17:01      wangzh         优化构造函数
      * --------------------------------------------------------
      **/
    public class MyException : SystemException
    {

        public string Code { set; get; }

        public string SubCode { set; get; }

        public string SubMessage { set; get; }

        /// <summary>
        /// 根据自定义码与消息构造
        /// </summary>
        /// <param name="c">自定义返回码</param>
        /// <param name="m">自定义消息</param>
        public MyException(string c,string m):base(m)
        {
            this.Code = c;   
        }

        /// <summary>
        /// 根据结果枚举来抛异常
        /// </summary>
        /// <param name="resultEnum">返回码枚举</param>
        /// <param name="language">语言</param>
        public MyException(MyResultCode resultEnum,string subMessage,string language="zh-Hans"):base(EnumHelper.GetDescription(resultEnum, language)+"("+ subMessage+")") {
            this.Code = ((int)resultEnum).ToString();
        }


        public MyException(MyResultCode c1, MyResultSubCode c2, string language = "zh-Hans") : base(EnumHelper.GetDescription(c1, language) + "(" + EnumHelper.GetDescription(c2, language) + ")")
        {
            this.Code = ((int)c1).ToString();
            this.SubCode = ((int)c2).ToString();
            this.SubMessage = EnumHelper.GetDescription(c2, language);
        }
    }
}
