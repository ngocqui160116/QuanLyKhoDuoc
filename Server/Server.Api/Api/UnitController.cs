using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Unit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/unit")]
    public class UnitController : BaseApiController
    {

        private readonly IUnitService _UnitService;
        public UnitController(IUnitService UnitService)
        {
            _UnitService = UnitService;
        }

        [HttpPost]
        [Route("GetAllUnit")]
        public async Task<BaseResponse<UnitDto>> GetAllUnit([FromBody] UnitRequest request)
        {
            return await _UnitService.GetAllUnit(request);
        }

        [HttpPost]
        [Route("CreateUnit")]
        public Task<CrudResult> CreateUnit([FromBody] UnitRequest request)
        {
            return _UnitService.CreateUnit(request);
        }

        [HttpPost]
        [Route("UpdateUnit")]
        public Task<CrudResult> UpdateUnit(int Id, [FromBody] UnitRequest request)
        {
            return _UnitService.UpdateUnit(Id, request);
        }

        [HttpDelete]
        [Route("DeleteUnit")]
        public Task<CrudResult> DeleteUnit(int Id)
        {
            return _UnitService.DeleteUnit(Id);
        }
    }
}