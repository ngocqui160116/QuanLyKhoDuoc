using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Customer;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Server.Services.MainServices
{
    public interface ICustomerService
    {
        List<CustomerDto> GetAllCustomer(CustomerRequest request);
    }
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _dataContext;
        public CustomerService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public List<CustomerDto> GetAllCustomer(CustomerRequest request)
        {
            //setup query
            var query = _dataContext.Customers.AsQueryable().Where(r => !r.Deleted);

            //if (!string.IsNullOrEmpty(request.IdCustomer.ToString()))
            //{
            //    query = query.Where(d => d.IdCustomer.ToString().Contains(request.IdCustomer.ToString()));
            //}

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(d => d.Name.Contains(request.Name));
            }
           
            
            //filter

            var data = query.ToList();
            return data.MapTo<CustomerDto>();
        }
    }
}
