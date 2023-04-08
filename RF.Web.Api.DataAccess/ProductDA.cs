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

    public interface IProductDA
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Result<int>> CreateProduct(string name, int price);
        Task<Result> UpdateProduct(int id, string name, int price);
        Task<Result> DeleteProduct(int id);
        Task<IEnumerable<Product>> GetProductsByShops(List<int> shopIdsTvp);
    }

    public class ProductDA : ExtBaseDA, IProductDA
    {
        public ProductDA(IOptions<ConnectionStrings> connectionStrs)
           : base(connectionStrs.Value.RfDb) { }


        public Task<IEnumerable<Product>> GetProducts()
        {
            return QueryAsync<Product>("usp_GetProducts",
                ParameterBuilder()
            );
        }

        public Task<Product> GetProductById(int id)
        {
            return QueryFirstOrDefaultAsync("usp_GetProductById",
                ProductMapping.Product,
                ParameterBuilder()
                    .AddParam("ProductId", id)
                );
        }


        public async Task<Result<int>> CreateProduct(string name, int price)
        {
            var parameters = ParameterBuilder()
                .AddParam("O_Id", dbType: DbType.Int32, direction: ParameterDirection.Output)
                .AddParam("Name", name)
                .AddParam("Price", price)
                .AddResultParams();

            await ExecuteAsync("usp_CreateProduct", parameters);

            return parameters.ParseExecutionResult<int>("O_Id");
        }

        public async Task<Result> UpdateProduct(int id, string name, int price)
        {
            var parameters = ParameterBuilder()
                .AddParam("ProductId", id)
                .AddParam("Name", name)
                .AddParam("Price", price)
                .AddResultParams()
                ;

            await ExecuteAsync("usp_UpdateProduct", parameters);

            return parameters.ParseExecutionResult();
        }

        public async Task<Result> DeleteProduct(int id)
        {
            var parameters = ParameterBuilder()
                .AddParam("ProductId", id)
                .AddResultParams()
                ;

            await ExecuteAsync("usp_DeleteProduct", parameters);

            return parameters.ParseExecutionResult();
        }

        public async Task<IEnumerable<Product>> GetProductsByShops(List<int> shopIdsTvp)
        {
            return await QueryAsync<Product>("usp_GetProductsByShops", ParameterBuilder()
                  .AddParamTvpInt("ShopIdsTvp", shopIdsTvp)
             );
        }
    }
}
