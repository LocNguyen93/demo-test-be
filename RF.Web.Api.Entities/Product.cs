
using System;
using Newtonsoft.Json.Linq;

namespace RF.Web.Api.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
