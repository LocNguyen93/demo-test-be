namespace RF.Web.Api.Services
{
    using AutoMapper;
    using DataAccess;
    using Ext.Shared.DataAccess;
    using RF.Web.Api.Services.RequestModels;
    using RF.Web.Api.Services.ResponseModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICustomerService
    {
        Task<List<CustomerResponseModel>> GetCustomers();
        Task<CustomerResponseModel> GetCustomerById(int id);
        Task<Result<int>> CreateCustomer(CustomerRequestModel CustomerRequestModel);
        Task<Result> UpdateCustomer(int id, CustomerRequestModel CustomerRequestModel);
        Task<Result> DeleteCustomer(int id);
        
    }

    public class CustomerService : RFBaseService, ICustomerService
    {
        private readonly ICustomerDA CustomerDA;
        private readonly IMapper mapper;

        public CustomerService(ICustomerDA CustomerDA, IMapper mapper)
        {
            this.CustomerDA = CustomerDA;
            this.mapper = mapper;
        }

        public async Task<List<CustomerResponseModel>> GetCustomers()
        {
            return mapper.Map<List<CustomerResponseModel>>(await CustomerDA.GetCustomers());
        }

        public async Task<CustomerResponseModel> GetCustomerById(int id)
        {
            return mapper.Map<CustomerResponseModel>(await CustomerDA.GetCustomerById(id));
        }

        public async Task<Result<int>> CreateCustomer(CustomerRequestModel CustomerRequestModel)
        {
            var result = await CustomerDA.CreateCustomer(CustomerRequestModel.FullName, CustomerRequestModel.Email, CustomerRequestModel.Birthday);
            return result;
        }

        public async Task<Result> UpdateCustomer(int id, CustomerRequestModel CustomerRequestModel)
        {
            var result = await CustomerDA.UpdateCustomer(id, CustomerRequestModel.FullName, CustomerRequestModel.Email, CustomerRequestModel.Birthday);
            return result;
        }

        public async Task<Result> DeleteCustomer(int id)
        {
            var result = await CustomerDA.DeleteCustomer(id);
            return result;
        }
    }
}
