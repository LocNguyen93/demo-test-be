using System;
using Newtonsoft.Json;

namespace RF.Web.Api.Services.ResponseModels
{
    public class OrderResponseModel
    {
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("product_name")]
        public string ProductName { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("shop_name")]
        public string ShopName { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }
}
