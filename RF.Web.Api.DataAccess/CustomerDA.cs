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

    public interface ICustomerDA
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomerById(int id);
        Task<Result<int>> CreateCustomer(string name, string email, DateTime birthday);
        Task<Result> UpdateCustomer(int id, string name, string email, DateTime birthday);
        Task<Result> DeleteCustomer(int id);
    }

    public class CustomerDA : ExtBaseDA, ICustomerDA
    {
        public CustomerDA(IOptions<ConnectionStrings> connectionStrs)
           : base(connectionStrs.Value.RfDb) { }


        public Task<IEnumerable<Customer>> GetCustomers()
        {
            return QueryAsync<Customer>("usp_GetCustomers",
                ParameterBuilder()
            );
        }

        public Task<Customer> GetCustomerById(int id)
        {
            return QueryFirstOrDefaultAsync("usp_GetCustomerById",
                CustomerMapping.Customer,
                ParameterBuilder()
                    .AddParam("CustomerId", id)
                );
        }


        public async Task<Result<int>> CreateCustomer(string name, string email, DateTime birthday)
        {
            var parameters = ParameterBuilder()
                .AddParam("O_Id", dbType: DbType.Int32, direction: ParameterDirection.Output)
                .AddParam("FullName", name)
                .AddParam("Email", email)
                .AddParam("Birthday", birthday)
                .AddResultParams();

            await ExecuteAsync("usp_CreateCustomer", parameters);

            return parameters.ParseExecutionResult<int>("O_Id");
        }

        public async Task<Result> UpdateCustomer(int id, string name, string email, DateTime birthday)
        {
            var parameters = ParameterBuilder()
                .AddParam("CustomerId", id)
                .AddParam("FullName", name)
                .AddParam("Email", email)
                .AddParam("Birthday", birthday)
                .AddResultParams()
                ;

            await ExecuteAsync("usp_UpdateCustomer", parameters);

            return parameters.ParseExecutionResult();
        }

        public async Task<Result> DeleteCustomer(int id)
        {
            var parameters = ParameterBuilder()
                .AddParam("CustomerId", id)
                .AddResultParams()
                ;

            await ExecuteAsync("usp_DeleteCustomer", parameters);

            return parameters.ParseExecutionResult();
        }
    }
}
