using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
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
        Task<BaseResponse<GroupDto>> GetAllGroup(GroupRequest request);
    }

    public class GroupProxy : BaseProxy, IGroupProxy
    {
        public async Task<BaseResponse<GroupDto>> GetAllGroup(GroupRequest request)
        {
            try
            {
                var api = RestService.For<IGroupApi>(GetHttpClient());

                return await api.GetAllGroup(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public interface IGroupApi
        {
            [Post("/group/GetAllGroup")]
            Task<BaseResponse<GroupDto>> GetAllGroup([Body] GroupRequest request);

        }

    }
}
