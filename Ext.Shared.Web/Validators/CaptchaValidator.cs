using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Ext.Shared.Web.Validators
{
    public interface ICaptchaValidator
    {
        Task<bool> IsCaptchaPassedAsync(string token, string secretKey);
        Task<JObject> GetCaptchaResultDataAsync(string token, string secretKey);
    }

    public class GoogleReCaptchaValidator : ICaptchaValidator
    {

        private readonly HttpClient _httpClient;
        private const string RemoteAddress = "https://www.google.com/recaptcha/api/siteverify";

        public GoogleReCaptchaValidator(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> IsCaptchaPassedAsync(string token, string secretKey)
        {
            var response = await GetCaptchaResultDataAsync(token, secretKey);
            return response.HasValues && "true".Equals(response["success"].Value<string>(), StringComparison.OrdinalIgnoreCase);
        }

        public async Task<JObject> GetCaptchaResultDataAsync(string token, string secretKey)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", secretKey),
                new KeyValuePair<string, string>("response", token)
            });
            var res = await _httpClient.PostAsync(RemoteAddress, content);
            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException(res.ReasonPhrase);
            }
            var jsonResult = await res.Content.ReadAsStringAsync();
            return JObject.Parse(jsonResult);
        }
    }
}
