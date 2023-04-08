using System;

namespace RF.Web.Api.Services.RequestModels
{
    public class CustomerRequestModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
    }
}
