using Newtonsoft.Json;
namespace RF.Web.Api.Services.ResponseModels
{
    public class ProductResponseModel
    {
        [JsonProperty("product_id")]
        public int ProductId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }
    }
}
