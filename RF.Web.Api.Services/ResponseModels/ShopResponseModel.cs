using Newtonsoft.Json;

namespace RF.Web.Api.Services.ResponseModels
{
    public class ShopResponseModel
    {
        [JsonProperty("shop_id")]
        public int ShopId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }
}
