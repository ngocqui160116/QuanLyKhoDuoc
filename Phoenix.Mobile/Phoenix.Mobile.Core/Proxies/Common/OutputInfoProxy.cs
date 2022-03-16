using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.OutputInfo;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IOutputInfoProxy
    {
        Task<BaseResponse<OutputInfoDto>> GetAllOutputInfo(OutputInfoRequest request);
    }

    public class OutputInfoProxy : BaseProxy, IOutputInfoProxy
    {
        public async Task<BaseResponse<OutputInfoDto>> GetAllOutputInfo(OutputInfoRequest request)
        {
            try
            {
                var api = RestService.For<IOutputInfoApi>(GetHttpClient());

                return await api.GetAllOutputInfo(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public interface IOutputInfoApi
        {
            [Post("/outputInfo/GetAllOutputInfo")]
            Task<BaseResponse<OutputInfoDto>> GetAllOutputInfo([Body] OutputInfoRequest request);

        }

    }
}
