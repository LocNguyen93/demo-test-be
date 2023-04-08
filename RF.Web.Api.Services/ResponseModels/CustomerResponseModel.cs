using System;
using Newtonsoft.Json;

namespace RF.Web.Api.Services.ResponseModels
{
    public class CustomerResponseModel
    {
        [JsonProperty("customer_id")]
        public int CustomerId { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }
    }
}
