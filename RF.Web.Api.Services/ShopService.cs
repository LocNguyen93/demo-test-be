namespace RF.Web.Api.Services
{
    using AutoMapper;
    using DataAccess;
    using Ext.Shared.DataAccess;
    using RF.Web.Api.Entities;
    using RF.Web.Api.Services.RequestModels;
    using RF.Web.Api.Services.ResponseModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IShopService
    {
        Task<List<ShopResponseModel>> GetShops();
        Task<ShopResponseModel> GetShopById(int id);
        Task<Result<int>> CreateShop(ShopRequestModel ShopRequestModel);
        Task<Result> UpdateShop(int id, ShopRequestModel ShopRequestModel);
        Task<Result> DeleteShop(int id);
        Task<Result> UpdateShopProduct(int shopId, List<int> productIds);
    }

    public class ShopService : RFBaseService, IShopService
    {
        private readonly IShopDA shopDA;
        private readonly IMapper mapper;

        public ShopService(IShopDA shopDA, IMapper mapper)
        {
            this.shopDA = shopDA;
            this.mapper = mapper;
        }

        public async Task<List<ShopResponseModel>> GetShops()
        {
            return mapper.Map<List<ShopResponseModel>>(await shopDA.GetShops());
        }

        public async Task<ShopResponseModel> GetShopById(int id)
        {
            return mapper.Map<ShopResponseModel>(await shopDA.GetShopById(id));
        }

        public async Task<Result<int>> CreateShop(ShopRequestModel ShopRequestModel)
        {
            var result = await shopDA.CreateShop(ShopRequestModel.Name, ShopRequestModel.Location);
            return result;
        }

        public async Task<Result> UpdateShop(int id, ShopRequestModel ShopRequestModel)
        {
            var result = await shopDA.UpdateShop(id, ShopRequestModel.Name, ShopRequestModel.Location);
            return result;
        }

        public async Task<Result> DeleteShop(int id)
        {
            var result = await shopDA.DeleteShop(id);
            return result;
        }

        public async Task<Result> UpdateShopProduct(int shopId, List<int> productIds)
        {
            var result = await shopDA.UpdateShopProduct(shopId, productIds);
            return result;
        }
    }
}
