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

        [HttpPost]
        [Route("CreateGroup")]
        public Task<CrudResult> CreateGroup([FromBody] GroupRequest request)
        {
            return _GroupService.CreateGroup(request);
        }

        [HttpPost]
        [Route("UpdateGroup")]
        public Task<CrudResult> UpdateGroup(int IdGroup, [FromBody] GroupRequest request)
        {
            return _GroupService.UpdateGroup(IdGroup, request);
        }

        [HttpDelete]
        [Route("DeleteGroup")]
        public Task<CrudResult> DeleteGroup(int IdGroup)
        {
            return _GroupService.DeleteGroup(IdGroup);
        }
    }
}