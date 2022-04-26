using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Inventory;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/inventory")]
    public class InventoryController : BaseApiController
    {

        private readonly IInventoryService _InventoryService;
        public InventoryController(IInventoryService InventoryService)
        {
            _InventoryService = InventoryService;
        }

        [HttpPost]
        [Route("GetAllInventory")]
        public async Task<BaseResponse<InventoryDto>> GetAllInventory([FromBody] InventoryRequest request)
        {
            return await _InventoryService.GetAllInventory(request);
        }

    }
}