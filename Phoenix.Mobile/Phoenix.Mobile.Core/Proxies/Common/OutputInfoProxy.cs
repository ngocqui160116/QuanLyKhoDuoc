using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
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
        Task<List<OutputInfoDto>> GetAllOutputInfo(OutputInfoRequest request);
    }

    public class OutputInfoProxy : BaseProxy, IOutputInfoProxy
    {
        public async Task<List<OutputInfoDto>> GetAllOutputInfo(OutputInfoRequest request)
        {
            try
            {
                var api = RestService.For<IOutputInfoApi>(GetHttpClient());
                var result = await api.GetAllOutputInfo(request);
                if (result == null) return new List<OutputInfoDto>();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new List<OutputInfoDto>();
            }
        }
        public interface IOutputInfoApi
        {
            [Post("/outputInfo/GetAllOutputInfo")]
            Task<List<OutputInfoDto>> GetAllOutputInfo([Body] OutputInfoRequest request);

        }
    }
}
