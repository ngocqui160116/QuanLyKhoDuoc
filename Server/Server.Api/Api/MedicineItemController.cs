using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.MedicineItem;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/MedicineItem")]
    public class MedicineItemController : BaseApiController
    {
        private readonly IMedicineItemService _MedicineItemService;
        public MedicineItemController(IMedicineItemService MedicineItemService)
        {
            _MedicineItemService = MedicineItemService;
        }

        [HttpPost]
        [Route("GetAllMedicineItems")]
        public async Task<BaseResponse<MedicineItemDto>> GetAllMedicineItems(MedicineItemRequest request)
        {
            return await _MedicineItemService.GetAllMedicineItems(request);
        }

        [HttpPost]
        [Route("GetMedicineItemById")]
        public async Task<BaseResponse<MedicineItemDto>> GetMedicineItemById(int Id)
        {
            return await _MedicineItemService.GetMedicineItemById(Id);
        }

        [HttpPost]
        [Route("AddMedicineItem")]
        public Task<CrudResult> AddMedicineItem([FromBody] MedicineItemRequest request)

        {
            return _MedicineItemService.AddMedicineItem(request);
        }

        [HttpPost]
        [Route("AddItemInventory")]
        public Task<CrudResult> AddItemInventory([FromBody] MedicineItemRequest request)

        {
            return _MedicineItemService.AddItemInventory(request);
        }

        [HttpPost]
        [Route("UpdateMedicineItem")]
        public Task<CrudResult> UpdateMedicineItem(int Id, [FromBody] MedicineItemRequest request)
        {
            return _MedicineItemService.UpdateMedicineItem(Id, request);
        }

        [HttpPost]
        [Route("UpdateItemInventory")]
        public Task<CrudResult> UpdateItemInventory(int Id, [FromBody] MedicineItemRequest request)
        {
            return _MedicineItemService.UpdateItemInventory(Id, request);
        }

        [HttpDelete]
        [Route("RemoveMedicineItem")]
        public Task<CrudResult> RemoveMedicineItem(int Id)

        {
            return _MedicineItemService.RemoveMedicineItem(Id);
        }

        [HttpDelete]
        [Route("DeleteAll")]
        public Task<CrudResult> DeleteAll()

        {
            return _MedicineItemService.DeleteAll();
        }
    }
}