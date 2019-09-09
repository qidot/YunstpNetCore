using System;

namespace yunstp.common.Enums
{


    /// <summary>
    /// 是/否 枚举
    /// </summary>
    public enum YesNoEnum
    {

        [EnumDescription("否")]
        No=0,
        [EnumDescription("是")]
        Yes=1
    }

    /// <summary>
    /// 启用/禁用 枚举
    /// </summary>
    public enum EnableDisableEnum {
        /// <summary>
        /// 已禁用
        /// </summary>
        [EnumDescription("已禁用")]
        Disable = 0,
        /// <summary>
        /// 已启用
        /// </summary>
        [EnumDescription("已启用")]
        Enable = 1
    }
}
