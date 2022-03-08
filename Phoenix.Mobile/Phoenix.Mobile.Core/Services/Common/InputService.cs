using Phoenix.Mobile.Core.Models.Input;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Input;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IInputService
    {
        Task<List<InputModel>> GetAllInput(InputRequest request);
    }

    public class InputService : IInputService
    {
        private readonly IInputProxy _InputProxy;
        public InputService(IInputProxy InputProxy)
        {
            _InputProxy = InputProxy;
        }
        public async Task<List<InputModel>> GetAllInput(InputRequest request)
        {
            var data = await _InputProxy.GetAllInput(request);
            return data.MapTo<InputModel>();
        }
    }
}
