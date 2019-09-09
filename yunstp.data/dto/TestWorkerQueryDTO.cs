using System;
namespace yunstp.data.dto
{
    public class TestWorkerQueryDTO : SqlBaseQueryDTO
    {
        public int? id { set; get; }
        public string name { set; get; }

        public DateTime createTime { set; get; }

        public int age { set; get; }

        public int index {set;get;}
    }
}
