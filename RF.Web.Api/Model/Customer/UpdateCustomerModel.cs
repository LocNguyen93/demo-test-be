using Newtonsoft.Json;
using System;

namespace RF.Web.Api.Models
{
    public class UpdateCustomerModel
    {
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
