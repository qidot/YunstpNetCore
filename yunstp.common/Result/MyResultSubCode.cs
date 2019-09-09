using System;
using yunstp.common.Enums;

namespace yunstp.common.Result
{
    /***
     * 1:系统报错
     * 2.stm基础报错
     * 3.bsc报错
     * 4.tm报错
     * **/
    public enum MyResultSubCode
    {
        [EnumDescription("OK",lang:"en-US")]
        [EnumDescription("成功")]
        SUCCESS = 0,
        /// <summary>
        /// 空指针
        /// </summary>
        /*[EnumDescription("空指针", "en-us")]*/
        [EnumDescription("空指针")]
        NULL_POINT = 10110,
        /// <summary>
        /// 配置文件读取失败
        /// </summary>
        [EnumDescription("配置文件读取失败")]
        CONFIG_READ_ERROR = 10111,
        /// <summary>
        /// 数据不存在
        /// </summary>
        [EnumDescription("数据不存在")]
        DATA_NOT_EXISTS = 10112,

        /// <summary>
        /// 数据公司信息不对称
        /// </summary>
        [EnumDescription("数据公司信息不对称")]
        DATA_CONO_NOT_EQ = 10113,


        /// <summary>
        /// 参数列表为空
        /// </summary>
        [EnumDescription("参数列表为空")]
        PARAM_LIST_EMPTY = 10114,
        

        /// <summary>
        /// 数据重复
        /// </summary>
        [EnumDescription("数据重复")]
        DATA_REPEAT = 10115,

        /// <summary>
        /// 数据已被禁用
        /// </summary>
        [EnumDescription("数据已被禁用")]
        DATA_FORBIDDEN = 10116,

        /// <summary>
        /// 系统数据无法编辑
        /// </summary>
        [EnumDescription("系统数据无法编辑")]
        DATA_IS_SYSTEM = 10117,

        /// <summary>
        /// 数据为空
        /// </summary>
        [EnumDescription("数据为空")]
        DATA_IS_NULL = 10118,

        /// <summary>
        /// 数据更新失败
        /// </summary>
        [EnumDescription("数据更新失败")]
        UPDATE_FAIL = 10119,

        /// <summary>
        /// 参数为空
        /// </summary>
        [EnumDescription("参数为空")]
        PARAM_EMPTY = 10120,
        /// <summary>
        /// 数据超出范围
        /// </summary>
        [EnumDescription("数据超出范围")]
        DATA_OUT_SCOPE = 10121,

        /// <summary>
        /// 导入数据错误
        /// </summary>
        [EnumDescription("导入数据错误")]
        IMPORT_DATA_ERROR = 10122,

        /// <summary>
        /// 导入头格式错误
        /// </summary>
        [EnumDescription("导入头格式错误")]
        IMPORT_HEADER_ERROR = 10123,

        /// <summary>
        /// 导入文件扩展名错误
        /// </summary>
        [EnumDescription("导入文件扩展名错误")]
        IMPORT_EXT_ERROR = 10124,


        /// <summary>
        /// 上传文件错误
        /// </summary>
        [EnumDescription("上传文件错误")]
        UPLOAD_FILE_ERROR = 10125,

        /// <summary>
        /// 数据插入失败
        /// </summary>
        [EnumDescription("数据插入失败")]
        INSERT_FAIL = 10126,

        /// <summary>
        /// 用户不存在或已被禁用
        /// </summary>
        [EnumDescription("用户不存在或已被禁用")]
        USER_NOT_EXIST =10127,

        /// <summary>
        /// 公司不存在或已被禁用
        /// </summary>
        [EnumDescription("公司不存在或已被禁用")]
        COMPANY_NOT_EXIST = 10128,

        /// <summary>
        /// 未获取到临时授权吗
        /// </summary>
        [EnumDescription("未获取到临时授权吗")]
        NO_AUTH_CODE = 10129,

        /// <summary>
        /// 当前公司此手机号已存在
        /// </summary>
        [EnumDescription("当前公司此手机号已存在")]
        USER_MOBILE_UNIQUE = 10130,


        /// <summary>
        /// 获取Reids失败
        /// </summary>
        [EnumDescription("获取Reids失败")]
        GET_REDIS_ERROR = 10201,

        /// <summary>
        /// 【微信】获取suiteToken失败
        /// </summary>
        [EnumDescription("【微信】获取suiteToken失败")]
        GET_SUITE_TOKEN_ERROR = 10202,

        /// <summary>
        /// 【微信】获取企业永久授权码失败
        /// </summary>
        [EnumDescription("【微信】获取企业永久授权码失败")]
        GET_PERMANENT_CODE_ERROR =10203,


        /// <summary>
        /// 【微信】获取服务商Token失败
        /// </summary>
        [EnumDescription("【微信】获取服务商Token失败")]
        GET_PROVIDER_TOKEN_ERROR = 10204,

        /// <summary>
        /// 【微信】获取用户登录信息失败
        /// </summary>
        [EnumDescription("【微信】获取用户登录信息失败")]
        GET_LOGIN_ERROR = 10205,

        /// <summary>
        /// 【微信】获取部门信息失败
        /// </summary>
        [EnumDescription("【微信】获取部门信息失败")]
        GET_WX_DEPARTMENT_ERROR = 10206,

        /// <summary>
        /// 【微信】导入部门列表失败
        /// </summary>
        [EnumDescription("【微信】导入部门列表失败")]
        WX_DEPARTMENT_INSERT =10207,

        /// <summary>
        /// 【微信】部门列表为空
        /// </summary>
        [EnumDescription("【微信】部门列表为空")]
        WX_DEPARTMENT_NULL = 10208,

        /// <summary>
        /// 【微信】部门列表为空
        /// </summary>
        [EnumDescription("【微信】获取部门用户失败")]
        WX_DEPARTMENT_USER_ERROR=10209,

        /// <summary>
        /// 【微信】录入部门用户失败
        /// </summary>
        [EnumDescription("【微信】录入部门用户失败")]
        WX_DEPARTMENT_USER_INSERT = 10210,

        /// <summary>
        /// 【微信】获取企业管理员列表失败
        /// </summary>
        [EnumDescription("【微信】获取企业管理员列表失败")]
        WX_ADMIN_USER_INSERT = 10211,

        /// <summary>
        /// 【微信】获取标签失败
        /// </summary>
        [EnumDescription("【微信】获取标签失败")]
        WX_TAG_ERROR = 10212,

        /// <summary>
        /// 【微信】录入标签失败
        /// </summary>
        [EnumDescription("【微信】录入标签失败")]
        WX_TAG_INSER = 10213,

        /// <summary>
        /// 【微信】更新部门列表失败
        /// </summary>
        [EnumDescription("【微信】更新部门列表失败")]
        WX_DEPARTMENT_UPDATE = 10214,


        /// <summary>
        /// 【微信】更新部门用户失败
        /// </summary>
        [EnumDescription("【微信】更新部门用户失败")]
        WX_DEPARTMENT_USER_UPDATE = 10215,

        /// <summary>
        /// 【微信】更新标签失败
        /// </summary>
        [EnumDescription("【微信】 更新标签失败")]
        WX_TAG_UPDATE = 10216,

        /// <summary>
        /// 【微信】获取suiteAccessToken失败
        /// </summary>
        [EnumDescription("【微信】 获取suiteAccessToken失败")]
        WX_SUITE_ACCESS_TOKEN = 10217,

        /// <summary>
        /// 【微信】发送消息失败
        /// </summary>
        [EnumDescription("【微信】发送消息失败 ")]
        WX_SEND_MSG = 10218,

        /// <summary>
        /// QUARTZ表达式异常
        /// </summary>
        [EnumDescription("QUARTZ表达式异常")]
        QUARTZ_CRON_ERROR = 10901,

        /// <summary>
        /// 数据为空
        /// </summary>
        [EnumDescription("数据已被使用")]
        DATA_IS_USED = 10919,

        /// <summary>
        /// 未登录
        /// </summary>
        [EnumDescription("未登录")]
        UN_LOGIN = 10401,
        /// <summary>
        /// 未授权
        /// </summary>
        [EnumDescription("未授权")]
        UN_AUTH = 10422,

        /// <summary>
        /// 当前公司正在初始化数据,不可重复处理
        /// </summary>
        [EnumDescription("当前公司正在初始化数据,不可重复处理")]
        ERROR_20001 = 20001,

        /// <summary>
        /// 公司已创建成功
        /// </summary>
        [EnumDescription("公司已存在")]
        ERROR_20002 = 20002,

        /// <summary>
        /// 无任务需要催办
        /// </summary>
        [EnumDescription("无任务需要催办")]
        ERROR_40001 = 40001,
        /// <summary>
        /// 日志插入失败
        /// </summary>
        [EnumDescription("日志插入失败")]
        ERROR_40002 = 40002,
        /// <summary>
        /// 取消任务失败
        /// </summary>
        [EnumDescription("取消任务失败")]
        ERROR_40003 = 40003,
        /// <summary>
        /// 当前任务已取消
        /// </summary>
        [EnumDescription("当前任务已取消")]
        ERROR_40004 = 40004,
        /// <summary>
        /// 当前模板正在使用
        /// </summary>
        [EnumDescription("当前模板正在使用")]
        ERROR_40005 = 40005,

        /// <summary>
        /// 操作失败
        /// </summary>
        [EnumDescription("操作失败")]
        ERROR_40006 = 40006,

        /// <summary>
        /// 添加任务失败
        /// </summary>
        [EnumDescription("添加任务失败")]
        ERROR_40007 = 40007,

        /// <summary>
        /// 完成任务失败
        /// </summary>
        [EnumDescription("完成任务失败")]
        ERROR_40008 = 40008,

        /// <summary>
        /// 任务已完成
        /// </summary>
        [EnumDescription("任务已完成")]
        ERROR_40009 = 40009,

        /// <summary>
        /// 任务未开始
        /// </summary>
        [EnumDescription("任务未开始")]
        ERROR_40010 = 40010,

        /// <summary>
        /// 启用/禁用失败
        /// </summary>
        [EnumDescription("启用/禁用失败")]
        ERROR_ENABLE = 50001,

        /// <summary>
        /// 未选择数据
        /// </summary>
        [EnumDescription("未选择数据")]
        ERROR_NO_SELECT = 50002,

        /// <summary>
        /// 启用/禁用状态不能为空
        /// </summary>
        [EnumDescription("启用/禁用状态不能为空")]
        ERROR_NO_ENABLED = 50003,

        /// <summary>
        /// 新增/编辑用户失败
        /// </summary>
        [EnumDescription("新增/编辑用户失败")]
        ERROR_50004 = 50004,

        /// <summary>
        /// 未找到链接
        /// </summary>
        [EnumDescription("未找到链接")]
        ERROR_NO_RDS = 50005,

        /// <summary>
        /// 删除失败
        /// </summary>
        [EnumDescription("删除失败")]
        ERROR_DELETE = 50006,

        /// <summary>
        /// 超时
        /// </summary>
        [EnumDescription("超时")]
        ERROR_OVERTIME = 50007,

        /// <summary>
        /// 开始时间必须小于结束时间
        /// </summary>
        [EnumDescription("开始时间必须小于结束时间")]
        ERROR_50008 = 50008,
        

    }
}
