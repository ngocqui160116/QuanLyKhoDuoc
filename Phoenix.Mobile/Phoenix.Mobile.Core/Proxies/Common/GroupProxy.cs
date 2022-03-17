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
        Task<GroupDto> AddGroup(GroupRequest request);
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

        public async Task<GroupDto> AddGroup(GroupRequest request)
        {
            try
            {
                var api = RestService.For<IGroupApi>(GetHttpClient());
                var result = await api.AddGroup(request);
                if (result == null) return new GroupDto();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new GroupDto();
            }
        }

        public interface IGroupApi
        {
            [Post("/group/GetAllGroup")]
            Task<BaseResponse<GroupDto>> GetAllGroup([Body] GroupRequest request);

            [Post("/group/CreateGroup")]
            Task<GroupDto> AddGroup([Body] GroupRequest request);


        }

    }
}
