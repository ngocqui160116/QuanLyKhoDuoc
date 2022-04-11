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
        List<InputDto> Search(string Id);
        Task<BaseResponse<InputDto>> Create(InputRequest request);
        Task<InputDto> AddInput(InputRequest request);
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

        public List<InputDto> Search(string Id)
        {
            try
            {
                var api = RestService.For<IInputApi>(GetHttpClient());

                return  api.Search(Id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<BaseResponse<InputDto>> Create(InputRequest request)
        {
            try
            {
                var api = RestService.For<IInputApi>(GetHttpClient());

                return await api.Create(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<InputDto> AddInput(InputRequest request)
        {
            try
            {
                var api = RestService.For<IInputApi>(GetHttpClient());
                var result = await api.AddInput(request);
                if (result == null) return new InputDto();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new InputDto();
            }
        }

        public interface IInputApi
        {
            [Post("/input/GetAllInput")]
            Task<BaseResponse<InputDto>> GetAllInput([Body] InputRequest request);

            [Post("/input/Search")]
            List<InputDto> Search(string Id);

            [Post("/input/Create")]
            Task<BaseResponse<InputDto>> Create([Body] InputRequest request);


            [Post("/input/GetInput")]
            Task<List<InputDto>> GetInput(string Id);

            [Post("/input/CreateInput")]
            Task<InputDto> AddInput([Body] InputRequest request);
        }

    }
}
