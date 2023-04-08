namespace RF.Web.Api.DataAccess
{
    using AppSettings;
    using Ext.Shared.DataAccess;
    using Ext.Shared.DataAccess.Dapper;
    using Microsoft.Extensions.Options;
    using RF.Web.Api.DataAccess.Mappings;
    using RF.Web.Api.Entities;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public interface IShopDA
    {
        Task<IEnumerable<Shop>> GetShops();
        Task<Shop> GetShopById(int id);
        Task<Result<int>> CreateShop(string name, string location);
        Task<Result> UpdateShop(int id, string name, string location);
        Task<Result> DeleteShop(int id);

        Task<Result> UpdateShopProduct(int shopId, List<int> productIdsTvp);
    }

    public class ShopDA : ExtBaseDA, IShopDA
    {
        public ShopDA(IOptions<ConnectionStrings> connectionStrs)
           : base(connectionStrs.Value.RfDb) { }


        public Task<IEnumerable<Shop>> GetShops()
        {
            return QueryAsync<Shop>("usp_GetShops",
                ParameterBuilder()
            );
        }

        public Task<Shop> GetShopById(int id)
        {
            return QueryFirstOrDefaultAsync("usp_GetShopById",
                ShopMapping.Shop,
                ParameterBuilder()
                    .AddParam("ShopId", id)
                );
        }


        public async Task<Result<int>> CreateShop(string name, string location)
        {
            var parameters = ParameterBuilder()
                .AddParam("O_Id", dbType: DbType.Int32, direction: ParameterDirection.Output)
                .AddParam("Name", name)
                .AddParam("Location", location)
                .AddResultParams();

            await ExecuteAsync("usp_CreateShop", parameters);

            return parameters.ParseExecutionResult<int>("O_Id");
        }

        public async Task<Result> UpdateShop(int id, string name, string location)
        {
            var parameters = ParameterBuilder()
                .AddParam("ShopId", id)
                .AddParam("Name", name)
                .AddParam("Location", location)
                .AddResultParams()
                ;

            await ExecuteAsync("usp_UpdateShop", parameters);

            return parameters.ParseExecutionResult();
        }

        public async Task<Result> DeleteShop(int id)
        {
            var parameters = ParameterBuilder()
                .AddParam("ShopId", id)
                .AddResultParams()
                ;

            await ExecuteAsync("usp_DeleteShop", parameters);

            return parameters.ParseExecutionResult();
        }
        public async Task<Result> UpdateShopProduct(int shopId, List<int> productIdsTvp)
        {
            var parameters = ParameterBuilder()
                .AddParam("ShopId", shopId)
                .AddParamTvpInt("ProductIdsTvp", productIdsTvp)
                .AddResultParams()
                ;

            await ExecuteAsync("usp_UpdateShopProduct", parameters);

            return parameters.ParseExecutionResult();
        }
    }
}
