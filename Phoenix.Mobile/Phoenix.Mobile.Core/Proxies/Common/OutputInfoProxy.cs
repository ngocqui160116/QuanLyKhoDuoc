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
        Task<OutputInfoDto> AddOutputInfo(OutputInfoRequest request);
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
        public async Task<OutputInfoDto> AddOutputInfo(OutputInfoRequest request)
        {
            try
            {
                var api = RestService.For<IOutputInfoApi>(GetHttpClient());
                var result = await api.AddOutputInfo(request);
                if (result == null) return new OutputInfoDto();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new OutputInfoDto();
            }
        }

        public interface IOutputInfoApi
        {
            [Post("/outputInfo/GetAllOutputInfo")]
            Task<BaseResponse<OutputInfoDto>> GetAllOutputInfo([Body] OutputInfoRequest request);

            [Post("/outputInfo/CreateOutputInfo")]
            Task<OutputInfoDto> AddOutputInfo([Body] OutputInfoRequest request);
        }

    }
}
