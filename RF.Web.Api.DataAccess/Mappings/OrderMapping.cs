using Ext.Shared.DataAccess.Dapper;
using RF.Web.Api.Entities;
using System;
using System.Data;
using Newtonsoft.Json.Linq;

namespace RF.Web.Api.DataAccess.Mappings
{
    public class OrderMapping
    {
        public static Func<Order, IDataRecord, Order> Order { get; set; } = (item, record) =>
        {
            item.FullName = record.GetString("FullName");
            item.Email = record.GetString("Email");
            item.ProductName = record.GetString("ProductName");
            item.Price = record.GetInt32("Price");
            item.ShopName = record.GetString("ShopName");
            item.Location = record.GetString("Location");
            return item;
        };
    }
}
