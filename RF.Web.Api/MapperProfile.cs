namespace RF.Web.Api
{
    using AutoMapper;
    using RF.Web.Api.Entities;
    using RF.Web.Api.Models;
    using RF.Web.Api.Services.RequestModels;
    using RF.Web.Api.Services.ResponseModels;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductResponseModel>();
            CreateMap<CreateProductModel, ProductRequestModel>();
            CreateMap<UpdateProductModel, ProductRequestModel>();

            CreateMap<Shop, ShopResponseModel>();
            CreateMap<CreateShopModel, ShopRequestModel>();
            CreateMap<UpdateShopModel, ShopRequestModel>();

            CreateMap<Customer, CustomerResponseModel>();
            CreateMap<CreateCustomerModel, CustomerRequestModel>();
            CreateMap<UpdateCustomerModel, CustomerRequestModel>();

            CreateMap<Order, OrderResponseModel>();
            CreateMap<CreateOrderModel, OrderRequestModel>();
        }
    }
}
