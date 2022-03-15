using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.Input;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IInputProxy
    {
        Task<BaseResponse<InputDto>> GetAllInput(InputRequest request);
    }

    public class InputProxy : BaseProxy, IInputProxy
    {
        public async Task<BaseResponse<InputDto>> GetAllInput(InputRequest request)
        {
            try
            {
                var api = RestService.For<IInputApi>(GetHttpClient());

                return await api.GetAllInput(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public interface IInputApi
        {
            [Post("/input/GetAllInput")]
            Task<BaseResponse<InputDto>> GetAllInput([Body] InputRequest request);

        }

    }
}
