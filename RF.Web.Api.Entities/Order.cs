
using System;
using Newtonsoft.Json.Linq;

namespace RF.Web.Api.Entities
{
    public class Order
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string ShopName { get; set; }
        public string Location { get; set; }
    }
}
