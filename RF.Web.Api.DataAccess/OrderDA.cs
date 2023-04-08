namespace RF.Web.Api.DataAccess
{
    using AppSettings;
    using Ext.Shared.DataAccess;
    using Ext.Shared.DataAccess.Dapper;
    using Microsoft.Extensions.Options;
    using RF.Web.Api.DataAccess.Mappings;
    using RF.Web.Api.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public interface IOrderDA
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<Result<int>> CreateOrder(int customerId, List<int> productIdsTvp);
    }

    public class OrderDA : ExtBaseDA, IOrderDA
    {
        public OrderDA(IOptions<ConnectionStrings> connectionStrs)
           : base(connectionStrs.Value.RfDb) { }


        public Task<IEnumerable<Order>> GetOrders()
        {
            return QueryAsync<Order>("usp_GetOrders",
                ParameterBuilder()
            );
        }

        public async Task<Result<int>> CreateOrder(int customerId, List<int> productIdsTvp)
        {
            var parameters = ParameterBuilder()
                .AddParam("O_Id", dbType: DbType.Int32, direction: ParameterDirection.Output)
                .AddParam("CustomerId", customerId)
                .AddParamTvpInt("ProductIdsTvp", productIdsTvp)
                .AddResultParams();

            await ExecuteAsync("usp_CreateOrder", parameters);

            return parameters.ParseExecutionResult<int>("O_Id");
        }
    }
}
