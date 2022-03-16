using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Medicine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/medicine")]
    public class MedicineController : BaseApiController
    {
        private readonly IMedicineService _medicineService;
        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpPost]
        [Route("GetAllMedicine")]

        public async Task<BaseResponse<MedicineDto>> GetAllMedicine([FromBody] MedicineRequest request)
        {
            return await _medicineService.GetAllMedicine(request);
        }

        [HttpPost]
        [Route("CreateMedicine")]
        public Task<CrudResult> CreateMedicine([FromBody] MedicineRequest request)
        {
            return _medicineService.CreateMedicine(request);
        }
    }
}