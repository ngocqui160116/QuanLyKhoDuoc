using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Group;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/group")]
    public class GroupController : BaseApiController
    {
      

        private readonly IGroupService _GroupService;
        public GroupController(IGroupService GroupService)
        {
            _GroupService = GroupService;
        }

        [HttpPost]
        [Route("GetAllGroup")]
        public async Task<BaseResponse<GroupDto>> GetAllCustomer([FromBody] GroupRequest request)
        {
            return await _GroupService.GetAllGroup(request);
        }

    }
}