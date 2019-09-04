using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

/**
    * className:  MyResponseContractResolver
    * descrption: 自定义JSON输出解析器(用于忽略相关输出)
    * author:     wangzh
    * created:    2019-08-23 10:24
    * --------------------------------------------------------
    * 日期           时间       修改人          说明
    * 2019-08-23    10:24      wangzh         初始化文件    
    * --------------------------------------------------------
    **/
namespace yunstp.common.Result
{
    public class MyResponseContractResolver : DefaultContractResolver
    {
        public readonly string _ignoreGroup;
        public MyResponseContractResolver(string ignoreGroup) {
            _ignoreGroup = ignoreGroup;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            //检测是否存在自定义忽略的属性信息
            MyJsonIgnoreAttribute mIgnore = null;
            if (!string.IsNullOrEmpty(_ignoreGroup)) {
                mIgnore = member.GetCustomAttribute<MyJsonIgnoreAttribute>(false);
            }
            //当前JSON字段信息
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            //property.HasMemberAttribute = true;
            if (mIgnore != null && !string.IsNullOrEmpty(mIgnore.Group) && (mIgnore.Group+",").Contains(_ignoreGroup+",")) {
                //检测到忽略属性了
                property.Ignored = true;
            }
            //property.ShouldSerialize = instance =>
            //{
            //    return true;
            //};

            return property;
        }
    }
}
