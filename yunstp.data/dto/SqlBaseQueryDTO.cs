using System;
namespace yunstp.data
{
    public class SqlBaseQueryDTO
    {
        //public string OrderBy { set; get; }

        public long Page { set; get; } = 1;

        public long PageSize { set; get; } = 20;
    }
}
