namespace RF.Web.Api.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using RF.Web.Api.Models;
    using RF.Web.Api.Services.RequestModels;
    using RF.Web.Api.Services.ResponseModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Web.Api.Services;

    [Route("order")]
    public class OrderController : RFBaseController
    {
        private readonly IOrderService OrderService;
        private readonly IMapper mapper;

        public OrderController(IMapper mapper, IOrderService OrderService)
        {
            this.mapper = mapper;
            this.OrderService = OrderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await OrderService.GetOrders());
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel createOrderModel)
        {
            var requestModel = mapper.Map<OrderRequestModel>(createOrderModel);
            var result = await OrderService.CreateOrder(requestModel);
            return Result(result);
        }

    }
}
