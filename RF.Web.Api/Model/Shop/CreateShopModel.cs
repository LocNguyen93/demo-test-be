using Newtonsoft.Json;

namespace RF.Web.Api.Models
{
    public class CreateShopModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }
}
