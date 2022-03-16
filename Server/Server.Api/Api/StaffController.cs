﻿using Phoenix.Server.Data.Entity;
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
    }
}