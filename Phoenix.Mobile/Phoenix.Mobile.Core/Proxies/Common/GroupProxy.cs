using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Group;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IGroupProxy
    {
        Task<List<GroupDto>> GetAllGroup(GroupRequest request);
    }

    public class GroupProxy : BaseProxy, IGroupProxy
    {
        public async Task<List<GroupDto>> GetAllGroup(GroupRequest request)
        {
            try
            {
                var api = RestService.For<IGroupApi>(GetHttpClient());
                var result = await api.GetAllGroup(request);
                if (result == null) return new List<GroupDto>();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new List<GroupDto>();
            }
        }
        public interface IGroupApi
        {
            [Post("/group/GetAllGroup")]
            Task<List<GroupDto>> GetAllGroup([Body] GroupRequest request);

        }
    }
}
