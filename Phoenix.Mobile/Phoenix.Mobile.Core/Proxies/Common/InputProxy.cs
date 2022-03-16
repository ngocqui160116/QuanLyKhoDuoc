using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
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
        Task<List<InputDto>> GetAllInput(InputRequest request);
    }

    public class InputProxy : BaseProxy, IInputProxy
    {
        public async Task<List<InputDto>> GetAllInput(InputRequest request)
        {
            try
            {
                var api = RestService.For<IInputApi>(GetHttpClient());
                var result = await api.GetAllInput(request);
                if (result == null) return new List<InputDto>();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new List<InputDto>();
            }
        }
        public interface IInputApi
        {
            [Post("/input/GetAllInput")]
            Task<List<InputDto>> GetAllInput([Body] InputRequest request);

        }
    }
}
