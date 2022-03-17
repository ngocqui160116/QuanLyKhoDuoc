using Phoenix.Mobile.Core.Models.InputInfo;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.InputInfo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IInputInfoService
    {
        Task<List<InputInfoModel>> GetAllInputInfo(InputInfoRequest request);
        Task<InputInfoModel> AddInputInfo(InputInfoRequest request);
    }

    public class InputInfoService : IInputInfoService
    {
        private readonly IInputInfoProxy _InputInfoProxy;
        public InputInfoService(IInputInfoProxy InputInfoProxy)
        {
            _InputInfoProxy = InputInfoProxy;
        }
        public async Task<List<InputInfoModel>> GetAllInputInfo(InputInfoRequest request)
        {
            var inputinfo = await _InputInfoProxy.GetAllInputInfo(request);
            return inputinfo.Data.MapTo<InputInfoModel>();
        }
        public async Task<InputInfoModel> AddInputInfo(InputInfoRequest request)
        {
            var data = await _InputInfoProxy.AddInputInfo(request);
            return data.MapTo<InputInfoModel>();
        }
    }
}
