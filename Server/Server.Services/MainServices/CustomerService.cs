using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Customer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface ICustomerService
    {
        Task<BaseResponse<CustomerDto>> GetAllCustomer(CustomerRequest request);
        Task<BaseResponse<CustomerDto>> CreateCustomer(CustomerRequest request);
    }
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _dataContext;
        public CustomerService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<CustomerDto>> GetAllCustomer(CustomerRequest request)
        {
            var result = new BaseResponse<CustomerDto>();
            try
            {
                // setup query
                var query = _dataContext.Customers.AsQueryable();

                // filter
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(d => d.Address.Contains(request.Name));
                }
                if (!string.IsNullOrEmpty(request.Address))
                {
                    query = query.Where(d => d.Address.Contains(request.Address));
                }

                query = query.OrderByDescending(d => d.IdCustomer);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<CustomerDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public async Task<BaseResponse<CustomerDto>> CreateCustomer(CustomerRequest request)
        {
            var result = new BaseResponse<CustomerDto>();
            try
            {
                Customer customer = new Customer
                {
                    Name = request.Name,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Address = request.Address,
                    Deleted = false
                };
                _dataContext.Customers.Add(customer);
                await _dataContext.SaveChangesAsync();

                result.success = true;
            }
            catch
            {

            }
            return result;
        }
    }
}
