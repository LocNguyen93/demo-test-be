namespace RF.Web.Api.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using RF.Web.Api.Models;
    using RF.Web.Api.Services.RequestModels;
    using System.Threading.Tasks;
    using Web.Api.Services;

    [Route("customer")]
    public class CustomerController : RFBaseController
    {
        private readonly ICustomerService CustomerService;
        private readonly IMapper mapper;

        public CustomerController(IMapper mapper, ICustomerService CustomerService)
        {
            this.mapper = mapper;
            this.CustomerService = CustomerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            return Ok(await CustomerService.GetCustomers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int id)
        {
            return Ok(await CustomerService.GetCustomerById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerModel createCustomerModel)
        {
            var requestModel = mapper.Map<CustomerRequestModel>(createCustomerModel);
            var result = await CustomerService.CreateCustomer(requestModel);
            return Result(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] UpdateCustomerModel updateCustomerModel)
        {
            var requestModel = mapper.Map<CustomerRequestModel>(updateCustomerModel);
            var result = await CustomerService.UpdateCustomer(id, requestModel);
            return Result(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            var result = await CustomerService.DeleteCustomer(id);
            return Result(result);
        }
    }
}
