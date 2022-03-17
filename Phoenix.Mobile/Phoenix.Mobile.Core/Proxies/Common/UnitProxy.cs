using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.Unit;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IUnitProxy
    {
        Task<BaseResponse<UnitDto>> GetAllUnit(UnitRequest request);
        Task<UnitDto> AddUnit(UnitRequest request);
    }

    public class UnitProxy : BaseProxy, IUnitProxy
    {
        public async Task<BaseResponse<UnitDto>> GetAllUnit(UnitRequest request)
        {
            try
            {
                var api = RestService.For<IUnitApi>(GetHttpClient());

                return await api.GetAllUnit(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }
        public async Task<UnitDto> AddUnit(UnitRequest request)
        {
            try
            {
                var api = RestService.For<IUnitApi>(GetHttpClient());
                var result = await api.AddUnit(request);
                if (result == null) return new UnitDto();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new UnitDto();
            }
        }

        public interface IUnitApi
        {
            [Post("/unit/GetAllUnit")]
            Task<BaseResponse<UnitDto>> GetAllUnit([Body] UnitRequest request);

            [Post("/unit/CreateUnit")]
            Task<UnitDto> AddUnit([Body] UnitRequest request);
        }

    }
}
