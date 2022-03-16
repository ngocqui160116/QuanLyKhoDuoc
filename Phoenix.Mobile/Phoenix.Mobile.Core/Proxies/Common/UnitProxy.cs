using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
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
        Task<List<UnitDto>> GetAllUnit(UnitRequest request);
    }

    public class UnitProxy : BaseProxy, IUnitProxy
    {
        public async Task<List<UnitDto>> GetAllUnit(UnitRequest request)
        {
            try
            {
                var api = RestService.For<IUnitApi>(GetHttpClient());
                var result = await api.GetAllUnit(request);
                if (result == null) return new List<UnitDto>();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new List<UnitDto>();
            }
        }
        public interface IUnitApi
        {
            [Post("/unit/GetAllUnit")]
            Task<List<UnitDto>> GetAllUnit([Body] UnitRequest request);

        }
    }
}
