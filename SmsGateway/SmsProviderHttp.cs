using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Flurl.Http;
using TelcobrightUtil;
using System.Threading.Tasks;

namespace SmsGateway
{
    public interface IHttpEndPoint
    {
        string BaseUrl { get; set; }
        string UrlSuffix { get; set; }
        string ApiKey { get; set; }
        string ClientId { get; set; }
        Task<object> Post(object payload);
        Task<object> Get(object payload);
    }
    public interface IHttpConfig
    {
        string BaseUrl { get; set; }
        string UrlSuffix { get; set; }
        string ApiKey { get; set; }
        string ClientId { get; set; }
    }
    public class HttpConfig : IHttpConfig
    {
        public string BaseUrl { get; set; }
        public string UrlSuffix { get; set; }
        public string ApiKey { get; set; }
        public string ClientId { get; set; }
    }
    public abstract class BaseEndPoint : IHttpEndPoint
    {
        public string BaseUrl { get; set; }
        public string UrlSuffix { get; set; }
        public string ApiKey { get; set; }
        public string ClientId { get; set; }
        public string FullUrl => $"{BaseUrl}{UrlSuffix}";
        Dictionary<string, object> RequestBody { get; }
        public BaseEndPoint(IHttpConfig config)
        {
            BaseUrl = config.BaseUrl;
            UrlSuffix = config.UrlSuffix;
            RequestBody = new Dictionary<string, object>
            {
                { "ApiKey", config.ApiKey },
                { "ClientId", config.ClientId }
            };
        }
        public Dictionary<string, object> CreateNewRequestBody()
        {
            return new Dictionary<string, object>(RequestBody);
        }
        public abstract Task<object> Post(object payload);
        public abstract Task<object> Get(object payload);
    }
    public class EndPoint : BaseEndPoint
    {
        public EndPoint(IHttpConfig config) : base(config) { }
        public override async Task<object> Post(object payload)
        {
            var response = await FullUrl.PostJsonAsync(payload);
            return response.GetJsonAsync<object>();
        }
        public override Task<object> Get(object payload)
        {
            throw new NotImplementedException();
        }
    }

    public class SmsProviderHttp : ISmsProvider
    {
        public EndPoint EndPoint { get; }
        public SmsProviderHttp(EndPoint endPoint)
        {
            this.EndPoint = endPoint;
        }
        public async Task<string> SendSms(SmsTask smsTask)
        {
            var requestBody = EndPoint.CreateNewRequestBody();
            var taskDict = smsTask.ToDictionary();

            taskDict.ForEach(item => requestBody.Add(item.Key, item.Value));
            var result = await EndPoint.Post(requestBody);

            return result.ToString();
        }
    }
}
