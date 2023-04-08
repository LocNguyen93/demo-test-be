using Ext.Shared.DataAccess.Dapper;
using RF.Web.Api.Entities;
using System;
using System.Data;
using Newtonsoft.Json.Linq;

namespace RF.Web.Api.DataAccess.Mappings
{
    public class ProductMapping
    {
        public static Func<Product, IDataRecord, Product> Product { get; set; } = (item, record) =>
        {
            item.ProductId = record.GetInt32("ProductId");
            item.Name = record.GetString("Name");
            item.Price = record.GetInt32("Price");
            return item;
        };
    }
}
