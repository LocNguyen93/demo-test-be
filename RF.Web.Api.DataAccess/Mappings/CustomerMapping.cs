using Ext.Shared.DataAccess.Dapper;
using RF.Web.Api.Entities;
using System;
using System.Data;
using Newtonsoft.Json.Linq;

namespace RF.Web.Api.DataAccess.Mappings
{
    public class CustomerMapping
    {
        public static Func<Customer, IDataRecord, Customer> Customer { get; set; } = (item, record) =>
        {
            item.CustomerId = record.GetInt32("CustomerId");
            item.FullName = record.GetString("FullName");
            item.Email = record.GetString("Email");
            item.Birthday = record.GetDateTime("Birthday");
            return item;
        };
    }
}
