using Phoenix.Mobile.Core.Models.Customer;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface ICustomerService
    {
        Task<List<CustomerModel>> GetAllCustomer(CustomerRequest request);
    }

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerProxy _CustomerProxy;
        public CustomerService(ICustomerProxy CustomerProxy)
        {
            _CustomerProxy = CustomerProxy;
        }
        public async Task<List<CustomerModel>> GetAllCustomer(CustomerRequest request)
        {
            var data = await _CustomerProxy.GetAllCustomer(request);
            return data.MapTo<CustomerModel>();
        }
    }
}
