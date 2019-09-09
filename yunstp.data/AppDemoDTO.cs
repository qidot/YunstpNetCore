using System;
using System.ComponentModel.DataAnnotations;
using yunstp.common;

namespace yunstp.data
{
    /**
      * className:  BscAddressLibDTO
      * description: 基础-省市区资料表
      * author:    wangzh
      * create by:  2019-08-22 13:30:22 
      * --------------------------------------------------------
      * [Required ] - 如果允许全空格可以 [Required(AllowEmptyStrings = true)]
      * [StringLength] - 字符串长度不能超过给定的最大长度[StringLength(10, MinimumLength=2)]
      * [Range] - 数字的可输入范围[Range(10, 20)] 
      * [Compare] -  [Compare("MyOtherProperty")]两个属性必须相同值
      * [RegularExpression] - 正则表达式匹配，字符串值必须匹配正则表达式，默认大小写敏感，可以使用(?i）修饰符关闭大小写敏感，比如[RegularExpression("(?i)mypattern")]
      * [Remote] - 服务端验证
      * --------------------------------------------------------
      * 日期    时间    修改人    说明
      * 2019-08-22 13:30:22    tanmengjia    初始化文件    
      * --------------------------------------------------------
      **/
    public class AppDemoDTO
    {
        //主键
        public int Id { set; get; } //int(10) UNSIGNED  自增编号 


        [Range(-1, 120, ErrorMessage = "age not more.")]
        public int Age { set; get; } = -1; //int(10) UNSIGNED  行政区划编号 


        [Required(ErrorMessage ="不能为空")]
        [StringLength(5, MinimumLength = 0, ErrorMessage = "hello")]
        public string Name { set; get; } //varchar(50)  名称 
    }
}
