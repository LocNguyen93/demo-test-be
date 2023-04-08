namespace Ext.Shared
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class LimoHttpClient
    {
        private readonly HttpClient httpClient;
        private readonly bool useHttpV2;

        public LimoHttpClient(string baseUrl, bool useHttpV2 = true)
        {
            this.useHttpV2 = useHttpV2;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public void AddDefaultHeaders(string name, string value)
        {
            httpClient.DefaultRequestHeaders.Add(name, value);
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUrl, Dictionary<string, string> headers = null)
        {
            return await SendDataAsync(HttpMethod.Get, requestUrl);
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUrl, HttpContent content, Dictionary<string, string> headers = null)
        {
            return await SendDataAsync(HttpMethod.Post, requestUrl, content, headers);
        }

        public async Task<HttpResponseMessage> PostJsonBodyAsync(string requestUrl, object content, Dictionary<string, string> headers = null)
        {
            var strContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            return await PostAsync(requestUrl, strContent, headers);
        }

        public async Task<HttpResponseMessage> PutAsync(string requestUrl, HttpContent content, Dictionary<string, string> headers = null)
        {
            return await SendDataAsync(HttpMethod.Put, requestUrl, content, headers);
        }

        public async Task<HttpResponseMessage> PutJsonBodyAsync(string requestUrl, object content, Dictionary<string, string> headers = null)
        {
            var strContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            return await PutAsync(requestUrl, strContent, headers);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUrl, Dictionary<string, string> headers = null)
        {
            return await SendDataAsync(HttpMethod.Delete, requestUrl);
        }

        private async Task<HttpResponseMessage> SendDataAsync(HttpMethod method, string requestUrl, HttpContent content = null, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(method, requestUrl);

            if (content != null)
                request.Content = content;

            if (useHttpV2)
                request.Version = new Version("2.0");

            if (headers != null && headers.Any())
                foreach (var item in headers)
                    request.Headers.Add(item.Key, item.Value);

            return await httpClient.SendAsync(request);
        }
    }
}
