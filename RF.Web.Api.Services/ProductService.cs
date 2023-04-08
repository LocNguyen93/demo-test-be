namespace RF.Web.Api.Services
{
    using AutoMapper;
    using DataAccess;
    using Ext.Shared.DataAccess;
    using RF.Web.Api.Services.RequestModels;
    using RF.Web.Api.Services.ResponseModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task<List<ProductResponseModel>> GetProducts();
        Task<ProductResponseModel> GetProductById(int id);
        Task<Result<int>> CreateProduct(ProductRequestModel productRequestModel);
        Task<Result> UpdateProduct(int id, ProductRequestModel productRequestModel);
        Task<Result> DeleteProduct(int id);
        Task<List<ProductResponseModel>> GetProductsByShops(List<int> shopIds);

    }

    public class ProductService : RFBaseService, IProductService
    {
        private readonly IProductDA productDA;
        private readonly IMapper mapper;

        public ProductService(IProductDA productDA, IMapper mapper)
        {
            this.productDA = productDA;
            this.mapper = mapper;
        }

        public async Task<List<ProductResponseModel>> GetProducts()
        {
            return mapper.Map<List<ProductResponseModel>>(await productDA.GetProducts());
        }

        public async Task<ProductResponseModel> GetProductById(int id)
        {
            return mapper.Map<ProductResponseModel>(await productDA.GetProductById(id));
        }

        public async Task<Result<int>> CreateProduct(ProductRequestModel productRequestModel)
        {
            var result = await productDA.CreateProduct(productRequestModel.Name, productRequestModel.Price);
            return result;
        }

        public async Task<Result> UpdateProduct(int id, ProductRequestModel productRequestModel)
        {
            var result = await productDA.UpdateProduct(id, productRequestModel.Name, productRequestModel.Price);
            return result;
        }

        public async Task<Result> DeleteProduct(int id)
        {
            var result = await productDA.DeleteProduct(id);
            return result;
        }

        public async Task<List<ProductResponseModel>> GetProductsByShops(List<int> shopIds)
        {
            return mapper.Map<List<ProductResponseModel>>(await productDA.GetProductsByShops(shopIds));
        }
    }
}
