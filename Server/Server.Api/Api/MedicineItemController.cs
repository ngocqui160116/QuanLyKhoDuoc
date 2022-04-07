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
        //public async Task<BaseResponse<MedicineItemDto>> GetAllMedicineItems(MedicineItemRequest request)
        public async Task<BaseResponse<MedicineItemDto>> GetAllMedicineItems(MedicineItemRequest request)
        {
            return await _MedicineItemService.GetAllMedicineItems(request);
        }

        [HttpPost]
        [Route("AddMedicineItem")]
        public Task<CrudResult> AddMedicineItem([FromBody] MedicineItemRequest request)

        {
            return _MedicineItemService.AddMedicineItem(request);
        }

        [HttpDelete]
        [Route("RemoveMedicineItem")]
        public Task<CrudResult> RemoveMedicineItem(int Id)

        {
            return _MedicineItemService.RemoveMedicineItem(Id);
        }

        //[HttpDelete]
        //[Route("ClearCart")]
        //public Task<CrudResult> ClearCart(int User_Id)

        //{
        //    return _MedicineItemService.ClearCart(User_Id);
        //}

       
    }
}