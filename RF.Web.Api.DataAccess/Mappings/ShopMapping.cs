using Ext.Shared.DataAccess.Dapper;
using RF.Web.Api.Entities;
using System;
using System.Data;
using Newtonsoft.Json.Linq;

namespace RF.Web.Api.DataAccess.Mappings
{
    public class ShopMapping
    {
        public static Func<Shop, IDataRecord, Shop> Shop { get; set; } = (item, record) =>
        {
            item.ShopId = record.GetInt32("ShopId");
            item.Name = record.GetString("Name");
            item.Location = record.GetString("Location");
            return item;
        };
    }
}
