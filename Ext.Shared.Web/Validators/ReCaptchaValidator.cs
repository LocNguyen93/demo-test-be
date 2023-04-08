namespace Ext.Shared.Web.Validators
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ReCaptchaValidator
    {
        public static ReCaptchaResponse Verify(string secret, string response)
        {
            var reCaptcha = new ReCaptchaResponse();
            using (System.Net.Http.HttpClient hc = new System.Net.Http.HttpClient())
            {
                var values = new Dictionary<string,
                    string> {
                        {
                            "secret",
                            secret
                        },
                        {
                            "response",
                            response
                        }
                    };
                var content = new System.Net.Http.FormUrlEncodedContent(values);
                var Response = hc.PostAsync("https://www.google.com/recaptcha/api/siteverify", content).Result;
                var responseString = Response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrWhiteSpace(responseString))
                    reCaptcha = JsonConvert.DeserializeObject<ReCaptchaResponse>(responseString);
                else
                    reCaptcha.Success = false;
            }
            return reCaptcha;
        }
    }

    public class ReCaptchaResponse
    {
        public bool Success { get; set; }

        public string Challenge_ts { get; set; }

        public string Hostname { get; set; }

        [JsonProperty(PropertyName = "error-codes")]
        public List<string> Error_codes { get; set; }
    }
}
