using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
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

        public async Task<BaseResponse<StaffDto>> GetAllStaff([FromBody] StaffRequest request)
        {
            return await _StaffService.GetAllStaff(request);
        }

        [HttpPost]
        [Route("CreateStaff")]
        public Task<CrudResult> CreateStaff([FromBody] StaffRequest request)
        {
            return _StaffService.CreateStaff(request);
        }

        [HttpPost]
        [Route("UpdateStaff")]
        public Task<CrudResult> UpdateStaff(int IdStaff, [FromBody] StaffRequest request)
        {
            return _StaffService.UpdateStaff(IdStaff, request);
        }

        [HttpDelete]
        [Route("DeleteStaff")]
        public Task<CrudResult> DeleteStaff(int IdStaff)
        {
            return _StaffService.DeleteStaff(IdStaff);
        }

        //[HttpPost]
        //[Route("GetStaffById")]

        //public async Task<BaseResponse<StaffDto>> GetStaffById(int User_Id)
        //{
        //    return await _StaffService.GetStaffById(User_Id);
        //}

        [HttpPost]
        [Route("GetStaffById")]
        public async Task<StaffDto> GetStaffById(int User_Id)
        {

            var user = _StaffService.GetStaffById(User_Id);
            return user;
        }

    }
}