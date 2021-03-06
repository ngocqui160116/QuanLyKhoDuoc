using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Core;
using Phoenix.Shared.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/customer")]
    public class CustomerController : BaseApiController
    {
      

        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [Route("GetAllCustomer")]
        public List<CustomerDto> GetAllCustomer([FromBody] CustomerRequest request)
        {
            return _customerService.GetAllCustomer(request);
        }

    }
}