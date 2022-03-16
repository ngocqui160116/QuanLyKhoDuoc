using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.Reason;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IReasonProxy
    {
        Task<BaseResponse<ReasonDto>> GetAllReason(ReasonRequest request);
    }

    public class ReasonProxy : BaseProxy, IReasonProxy
    {
        public async Task<BaseResponse<ReasonDto>> GetAllReason(ReasonRequest request)
        {
            try
            {
                var api = RestService.For<IReasonApi>(GetHttpClient());

                return await api.GetAllReason(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public interface IReasonApi
        {
            [Post("/reason/GetAllReason")]
            Task<BaseResponse<ReasonDto>> GetAllReason([Body] ReasonRequest request);

        }

    }
}
