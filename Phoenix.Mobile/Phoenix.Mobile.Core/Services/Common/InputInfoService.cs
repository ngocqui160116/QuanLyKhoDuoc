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
        Task<List<InputInfoModel>> AddInputInfo(InputInfoRequest request);
        //Task<InputInfoModel> AddInputInfo(InputInfoRequest request);
        Task<List<InputInfoModel>> Complete(int Id, InputInfoRequest request);
        Task<InputInfoModel> AddInventory(InputInfoRequest request);
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
        public async Task<List<InputInfoModel>> AddInputInfo(InputInfoRequest request)
        {
            var inputinfo = await _InputInfoProxy.CreateInputInfo(request);
            return inputinfo.Data.MapTo<InputInfoModel>();
        }

        public async Task<List<InputInfoModel>> Complete(int Id, InputInfoRequest request)
        {
            var inputinfo = await _InputInfoProxy.Complete(Id, request);
            return inputinfo.Data.MapTo<InputInfoModel>();
        }
        public async Task<InputInfoModel> AddInventory(InputInfoRequest request)
        {
            var data = await _InputInfoProxy.AddInventory(request);
            return data.MapTo<InputInfoModel>();
        }
    }
}
