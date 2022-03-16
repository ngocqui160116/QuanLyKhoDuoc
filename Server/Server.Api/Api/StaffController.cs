using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Core;
using Phoenix.Shared.Staff;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/staff")]
    public class StaffController : BaseApiController
    {
        private readonly IStaffService _StaffService;
        public StaffController(IStaffService StaffService)
        {
            _StaffService = StaffService;
        }

        [HttpPost]
        [Route("GetAllStaff")]
        public List<StaffDto> GetAllStaff([FromBody] StaffRequest request)
        {
            return _StaffService.GetAllStaff(request);
        }

    }
}