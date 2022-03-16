using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Customer;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface ICustomerProxy
    {
        Task<List<CustomerDto>> GetAllCustomer(CustomerRequest request);
    }

    public class CustomerProxy : BaseProxy, ICustomerProxy
    {
        public async Task<List<CustomerDto>> GetAllCustomer(CustomerRequest request)
        {
            try
            {
                var api = RestService.For<ICustomerApi>(GetHttpClient());
                var result = await api.GetAllCustomer(request);
                if (result == null) return new List<CustomerDto>();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new List<CustomerDto>();
            }
        }
        public interface ICustomerApi
        {
            [Post("/customer/GetAllCustomer")]
            Task<List<CustomerDto>> GetAllCustomer([Body] CustomerRequest request);

        }
    }
}
