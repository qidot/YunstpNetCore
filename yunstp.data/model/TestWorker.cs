using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using yunstp.common.Result;

/**
  * className:  TestWorker
  * descrption: 测试dapper用的基础类1
  * author:     wangzh
  * create by:  public 
  * --------------------------------------------------------
  * 日期           时间       修改人          说明
  * public     public       wangzh         初始化文件    
  * --------------------------------------------------------
  **/
namespace yunstp.data.model
{
    //[Table("test_worker")]
    public class TestWorker : IModel
    {
        [Key]
        //[Column("id")]
        public int id { set; get; }
        //[Column("name")]
        public string name { set; get; }
        //[Column("code")]
        public string code { set; get; }
        //[Column("age")]
        public int? age { set; get; }

        //[Column("create_time")]
        public DateTime? createTime { set; get; }

        //[Column("create_user")]
        [StringLength(50, ErrorMessage = "创建人最长可以50个字")]
        public string createUser { set; get; }


        [MyJsonIgnore(Group ="wx,ding")]
        //[Computed]
        public string abc { set; get; } = "this is json ignore";


        //[Computed]
        public int index { set; get; }

        public long GetPrimaryKey()
        {
            return this.id;
        }
    }
}
