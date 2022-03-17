using Phoenix.Mobile.Core.Models.Unit;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Unit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IUnitService
    {
        Task<List<UnitModel>> GetAllUnit(UnitRequest request);
        Task<UnitModel> AddUnit(UnitRequest request);
    }

    public class UnitService : IUnitService
    {
        private readonly IUnitProxy _UnitProxy;
        public UnitService(IUnitProxy UnitProxy)
        {
            _UnitProxy = UnitProxy;
        }
        public async Task<List<UnitModel>> GetAllUnit(UnitRequest request)
        {
            var unit = await _UnitProxy.GetAllUnit(request);
            return unit.Data.MapTo<UnitModel>();
        }

        public async Task<UnitModel> AddUnit(UnitRequest request)
        {
            var data = await _UnitProxy.AddUnit(request);
            return data.MapTo<UnitModel>();
        }
    }
}
