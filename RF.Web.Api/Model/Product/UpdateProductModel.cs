using Newtonsoft.Json;

namespace RF.Web.Api.Models
{
    public class UpdateProductModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }
    }
}
