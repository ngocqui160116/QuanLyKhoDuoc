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
        List<InputModel> Search(string Id);
        Task<List<InputModel>> Create(InputRequest request);
        Task<InputModel> AddInput(InputRequest request);
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
            var input = await _InputProxy.GetAllInput(request);
            return input.Data.MapTo<InputModel>();
        }

        public  List<InputModel> Search(string Id)
        {
            var input =  _InputProxy.Search(Id);
            return input.MapTo<InputModel>();
        }

        public async Task<List<InputModel>> Create(InputRequest request)
        {
            var input = await _InputProxy.Create(request);
            return input.Data.MapTo<InputModel>();
        }

        public async Task<InputModel> AddInput(InputRequest request)
        {
            var data = await _InputProxy.AddInput(request);
            return data.MapTo<InputModel>();
        }
    }
}
