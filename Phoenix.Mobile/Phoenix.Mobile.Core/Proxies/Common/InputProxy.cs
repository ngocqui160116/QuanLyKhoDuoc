using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
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
        Task<BaseResponse<InputDto>> CreateInput(InputRequest request);
       //Task<InputDto> AddInput(InputRequest request);
        Task<CrudResult> UpdateStatus(int Id, InputRequest request);
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

        public async Task<BaseResponse<InputDto>> CreateInput(InputRequest request)
        {
            try
            {
                var api = RestService.For<IInputApi>(GetHttpClient());

                return await api.CreateInput(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        //public async Task<InputDto> AddInput(InputRequest request)
        //{
        //    try
        //    {
        //        var api = RestService.For<IInputApi>(GetHttpClient());
        //        var result = await api.AddInput(request);
        //        if (result == null) return new InputDto();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHandler.Handle(new NetworkException(ex), true);
        //        return new InputDto();
        //    }
        //}

        public async Task<CrudResult> UpdateStatus(int Id, InputRequest request)
        {
            try
            {
                var api = RestService.For<IInputApi>(GetHttpClient());
                var result = await api.UpdateStatus(Id, request);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }

        public interface IInputApi
        {
            [Post("/input/GetAllInput")]
            Task<BaseResponse<InputDto>> GetAllInput([Body] InputRequest request);

            [Post("/input/Search")]
            List<InputDto> Search(string Id);

            [Post("/input/GetInput")]
            Task<List<InputDto>> GetInput(string Id);

            [Post("/input/CreateInput")]
            Task<BaseResponse<InputDto>> CreateInput(InputRequest request);

            [Post("/input/UpdateStatus")]
            Task<CrudResult> UpdateStatus(int Id, InputRequest request);
        }

    }
}
