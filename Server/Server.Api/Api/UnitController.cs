using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
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
        public List<UnitDto> GetAllUnit([FromBody] UnitRequest request)
        {
            return _UnitService.GetAllUnit(request);
        }

    }
}