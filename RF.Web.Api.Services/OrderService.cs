namespace RF.Web.Api.Services
{
    using AutoMapper;
    using DataAccess;
    using Ext.Shared.DataAccess;
    using RF.Web.Api.Services.RequestModels;
    using RF.Web.Api.Services.ResponseModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IOrderService
    {
        Task<List<OrderResponseModel>> GetOrders();
        Task<Result<int>> CreateOrder(OrderRequestModel OrderRequestModel);
    }

    public class OrderService : RFBaseService, IOrderService
    {
        private readonly IOrderDA OrderDA;
        private readonly IMapper mapper;

        public OrderService(IOrderDA OrderDA, IMapper mapper)
        {
            this.OrderDA = OrderDA;
            this.mapper = mapper;
        }

        public async Task<List<OrderResponseModel>> GetOrders()
        {
            return mapper.Map<List<OrderResponseModel>>(await OrderDA.GetOrders());
        }

        public async Task<Result<int>> CreateOrder(OrderRequestModel OrderRequestModel)
        {
            List<int> productIds = !String.IsNullOrEmpty(OrderRequestModel.ProductIds) ? OrderRequestModel.ProductIds.Split(',').Select(int.Parse).ToList() : new List<int>();
            var result = await OrderDA.CreateOrder(OrderRequestModel.CustomerId, productIds);
            return result;
        }
    }
}
