using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace yunstp.common.Helper
{
    public class MyRestHelper
    {
        private RestClient _client;

        public RestClient Client { get { return _client; } }

        public MyRestHelper(string url)
        {
            this._client = new RestClient(url);
            //使用CookieContainer自动管理cookie
            this._client.CookieContainer = new System.Net.CookieContainer();
        }




        /// <summary>
        /// 发送一个HTTP请求
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="headers">请求头</param>
        /// <param name="method">请求方式</param>
        /// <param name="body">请求参数</param>
        /// <param name="setRequest">设置请求参数委托</param>
        /// <returns>返回T对象</returns>
        /// <remarks>当Method为Get的时候，body只能是简单的匿名对象，即匿名对象中不能在包含匿名对象</remarks>
        public T Execute<T>(Dictionary<string, string> headers, Method method = Method.POST, object body = null, Action<IRestRequest> setRequest = null)
            where T : new()
        {
            return ExecuteResponse<T>(headers, method, body, setRequest).Data;
        }

        /// <summary>
        /// 发送一个HTTP请求
        /// </summary>
        /// <param name="headers">请求头</param>
        /// <param name="method">请求方式</param>
        /// <param name="body">请求参数</param>
        /// <param name="setRequest">设置请求参数委托</param>
        /// <returns>返回一个包括所有服务器响应信息的原始对象</returns>
        public IRestResponse ExecuteResponse(Dictionary<string, string> headers, Method method = Method.POST, object body = null, Action<IRestRequest> setRequest = null)
        {
            var request = BuildRequest(headers, method, body, setRequest);
            return _client.Execute(request);
        }

        /// <summary>
        /// 发送一个异步HTTP请求
        /// </summary>
        /// <param name="headers">请求头</param>
        /// <param name="callback">异步回调函数</param>
        /// <param name="method">请求方式</param>
        /// <param name="body">请求参数</param>
        /// <param name="setRequest">设置请求参数委托</param>
        /// <returns></returns>
        public RestRequestAsyncHandle ExecuteResponseAsync(Dictionary<string, string> headers, Action<IRestResponse> callback, Method method = Method.POST, object body = null, Action<IRestRequest> setRequest = null)
        {
            var request = BuildRequest(headers, method, body, setRequest);
            return _client.ExecuteAsync(request, callback);
        }

        /// <summary>
        /// 发送一个HTTP请求
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="headers">请求头</param>
        /// <param name="method">请求方式</param>
        /// <param name="body">请求参数</param>
        /// <param name="setRequest">设置请求参数委托</param>
        /// <returns>返回一个包括所有服务器响应信息的原始对象及反序列化的T对象Data</returns>
        public IRestResponse<T> ExecuteResponse<T>(Dictionary<string, string> headers, Method method = Method.POST, object body = null, Action<IRestRequest> setRequest = null)
            where T : new()
        {
            var request = BuildRequest(headers, method, body, setRequest);
            return _client.Execute<T>(request);
        }

        /// <summary>
        /// 发送一个异步HTTP请求
        /// </summary>
        /// <param name="headers">请求头</param>
        /// <param name="callback">回调函数</param>
        /// <param name="method">请求方式</param>
        /// <param name="body">请求参数</param>
        /// <param name="setRequest">设置请求参数委托</param>
        /// <returns></returns>
        public RestRequestAsyncHandle ExecuteResponseAsync<T>(Dictionary<string,string> headers, Action<IRestResponse<T>> callback, Method method = Method.POST, object body = null, Action<IRestRequest> setRequest = null)
            where T : new()
        {
            var request = BuildRequest(headers, method, body, setRequest);
            return _client.ExecuteAsync<T>(request, callback);
        }

        /// <summary>
        /// 组装请求
        /// </summary>
        /// <param name="method">请求类型</param>
        /// <param name="body">请求体</param>
        /// <param name="setRequest">设置请求参数委托</param>
        /// <returns></returns>
        private RestRequest BuildRequest(Dictionary<string,string> headers, Method method = Method.GET, object body = null, Action<IRestRequest> setRequest = null)
        {

            //var client = new RestClient("http://form.r-vision-group.com/api/v1/Forms?page=0&pageSize=10&sort=CreatedTime&sortOrder=desc&search=");
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Postman-Token", "d4357baf-7716-4939-92db-e6025e6015e5");
            //request.AddHeader("Cache-Control", "no-cache");
            //request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImR5bGFuaHVAcnBsdXMuY29tIiwidXNlcklkIjoiMiIsInRlbmFudElkIjoiMSIsIm5iZiI6MTU2MDczNzkyOSwiZXhwIjoxNTkyMjczOTI5LCJpc3MiOiJ0ZXN0MTIzIiwiYXVkIjoidGVzdDEyMyJ9.qwiCjOf2hL7zpWRPx1qdv1z7jDXcUkegUe0Ln_TUL2E");
            //IRestResponse response = client.Execute(request);

            var request = new RestRequest(method)
            {
                RequestFormat = DataFormat.Json,
                Method = method
                //JsonSerializer = new RestJsonSerializer("application/json")
            };
            //默认传递数据格式及响应格式都为json
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            if (headers != null && headers.Count>0) {
                foreach (var d in headers) {
                    request.AddHeader(d.Key, d.Value);
                }
            }

            //添加请求体
            if (body != null)
            {
                if(new Method[] { Method.PUT, Method.POST }.Contains(method))
                {

                    //该方法只能用于POST或PUT请求
                    request.AddJsonBody(body);
                }
                else
                {
                    if (body is Dictionary<string,string>) {
                        var pList = (body as Dictionary<string, string>);
                        foreach(var p in pList)
                        {
                            request.AddOrUpdateParameter(p.Key,p.Value);
                        }
                    }
                    else
                    {

                        //Get或其他请求，body只能是一个简单匿名对象
                        request.AddObject(body);
                    }
                }
            }

            //执行设置请求参数委托
            if (setRequest != null)
            {
                setRequest.Invoke(request);
            }
            return request;
        }
    }
}
