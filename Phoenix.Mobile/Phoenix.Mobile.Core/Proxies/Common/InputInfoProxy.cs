using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.InputInfo;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IInputInfoProxy
    {
        Task<List<InputInfoDto>> GetAllInputInfo(InputInfoRequest request);
    }

    public class InputInfoProxy : BaseProxy, IInputInfoProxy
    {
        public async Task<List<InputInfoDto>> GetAllInputInfo(InputInfoRequest request)
        {
            try
            {
                var api = RestService.For<IInputInfoApi>(GetHttpClient());
                var result = await api.GetAllInputInfo(request);
                if (result == null) return new List<InputInfoDto>();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new List<InputInfoDto>();
            }
        }
        public interface IInputInfoApi
        {
            [Post("/inputInfo/GetAllInputInfo")]
            Task<List<InputInfoDto>> GetAllInputInfo([Body] InputInfoRequest request);

        }
    }
}
