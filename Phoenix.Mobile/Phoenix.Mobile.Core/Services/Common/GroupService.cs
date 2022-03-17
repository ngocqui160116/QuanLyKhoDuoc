using Phoenix.Mobile.Core.Models.Group;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Group;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IGroupService
    {
        Task<List<GroupModel>> GetAllGroup(GroupRequest request);
        Task<GroupModel> AddGroup(GroupRequest request);
    }

    public class GroupService : IGroupService
    {
        private readonly IGroupProxy _GroupProxy;
        public GroupService(IGroupProxy GroupProxy)
        {
            _GroupProxy = GroupProxy;
        }
        public async Task<List<GroupModel>> GetAllGroup(GroupRequest request)
        {
            var group = await _GroupProxy.GetAllGroup(request);
            return group.Data.MapTo<GroupModel>();
        }
        public async Task<GroupModel> AddGroup(GroupRequest request)
        {
            var data = await _GroupProxy.AddGroup(request);
            return data.MapTo<GroupModel>();
        }
    }
}
