namespace Ext.Shared.Web.Models
{
    using Newtonsoft.Json;

    public class ResultModel
    {
        [JsonProperty(PropertyName = "ok")]
        public bool Ok { get; set; } = true;

        [JsonProperty(PropertyName = "m", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; } = null;

        [JsonProperty(PropertyName = "c", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorCode { get; set; } = null;

        [JsonProperty(PropertyName = "d", NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; } = null;

        private ResultModel() { }
        private ResultModel(bool ok, string code, string message, object data)
        {
            Ok = ok;
            Data = data;
            ErrorCode = code;
            Message = message;
        }

        public static ResultModel Create()
        {
            return new ResultModel();
        }

        public static ResultModel Create(object data)
        {
            return new ResultModel(true, null, null, data);
        }

        public static ResultModel Create(bool ok, string errCode, string message, object data = null)
        {
            return new ResultModel(ok, errCode, message, data);
        }
    }
}
