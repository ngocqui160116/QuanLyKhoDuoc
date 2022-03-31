using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Common;
using Phoenix.Shared.InventoryTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/inventoryTags")]
    public class InventoryTagsController : BaseApiController
    {

        private readonly IInventoryTagsService _InventoryTagsService;
        public InventoryTagsController(IInventoryTagsService InventoryTagsService)
        {
            _InventoryTagsService = InventoryTagsService;
        }

        [HttpPost]
        [Route("GetAllInventoryTags")]
        public async Task<BaseResponse<InventoryTagsDto>> GetAllCustomer([FromBody] InventoryTagsRequest request)
        {
            return await _InventoryTagsService.GetAllInventoryTags(request);
        }

    }
}