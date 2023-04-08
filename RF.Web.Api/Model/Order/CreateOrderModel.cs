using Newtonsoft.Json;
using System;

namespace RF.Web.Api.Models
{
    public class CreateOrderModel
    {
        [JsonProperty("customer_id")]
        public int CustomerId { get; set; }

        [JsonProperty("product_ids")]
        public string ProductIds { get; set; }
    }
}
