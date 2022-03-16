using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Supplier;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/supplier")]
    public class SupplierController : BaseApiController
    {
      

        private readonly ISupplierService _SupplierService;
        public SupplierController(ISupplierService SupplierService)
        {
            _SupplierService = SupplierService;
        }

        [HttpPost]
        [Route("GetAllSupplier")]
        public async Task<BaseResponse<SupplierDto>> GetAllSupplier([FromBody] SupplierRequest request)
        {
            return await _SupplierService.GetAllSupplier(request);
        }

        //[HttpPost]
        //[Route("CreateSupplier")]
        //public async Task<BaseResponse<SupplierDto>> CreateSupplier([FromBody] SupplierRequest request)
        //{
        //    return await _SupplierService.GetAllSupplier(request);
        //}
        [HttpPost]
        [Route("CreateSupplier")]
        public Task<CrudResult> CreateSupplier([FromBody] SupplierRequest request)
        {
            return _SupplierService.CreateSupplier(request);
        }
    }
}