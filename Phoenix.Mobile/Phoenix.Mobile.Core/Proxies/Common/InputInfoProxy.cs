using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
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
        Task<BaseResponse<InputInfoDto>> GetAllInputInfo(InputInfoRequest request);
        Task<BaseResponse<InputInfoDto>> CreateInputInfo(InputInfoRequest request);
        Task<InputInfoDto> AddInventory(InputInfoRequest request);
        Task<BaseResponse<InputInfoDto>> Complete(int Id, InputInfoRequest request);
    }

    public class InputInfoProxy : BaseProxy, IInputInfoProxy
    {
        public async Task<BaseResponse<InputInfoDto>> GetAllInputInfo(InputInfoRequest request)
        {
            try
            {
                var api = RestService.For<IInputInfoApi>(GetHttpClient());
               
                return await api.GetAllInputInfo(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<BaseResponse<InputInfoDto>> CreateInputInfo(InputInfoRequest request)
        {
            try
            {

                var api = RestService.For<IInputInfoApi>(GetHttpClient());

                return await api.CreateInputInfo(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<BaseResponse<InputInfoDto>> Complete(int Id, InputInfoRequest request)
        {
            try
            {
                var api = RestService.For<IInputInfoApi>(GetHttpClient());
                return await api.Complete(Id, request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<InputInfoDto> AddInventory(InputInfoRequest request)
        {
            try
            {
                var api = RestService.For<IInputInfoApi>(GetHttpClient());
                var result = await api.AddInventory(request);
                if (result == null) return new InputInfoDto();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new InputInfoDto();
            }
        }

        public interface IInputInfoApi
        {
            [Post("/inputInfo/GetAllInputInfo")]
            Task<BaseResponse<InputInfoDto>> GetAllInputInfo([Body] InputInfoRequest request);

            [Post("/inputInfo/CreateInputInfo")]
            Task<BaseResponse<InputInfoDto>> CreateInputInfo(InputInfoRequest request);

            [Post("/inputInfo/CreateInventory")]
            Task<InputInfoDto> AddInventory([Body] InputInfoRequest request);

            [Post("/inputInfo/Complete")]
            Task<BaseResponse<InputInfoDto>> Complete(int Id, InputInfoRequest request);
        }

    }
}
