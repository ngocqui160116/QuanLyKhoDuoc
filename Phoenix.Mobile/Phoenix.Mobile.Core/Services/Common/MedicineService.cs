using Phoenix.Mobile.Core.Models.Medicine;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Medicine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IMedicineService
    {
        Task<List<MedicineModel>> GetAllMedicine(MedicineRequest request);
    }

    public class MedicineService : IMedicineService
    {
        private readonly IMedicineProxy _MedicineProxy;
        public MedicineService(IMedicineProxy MedicineProxy)
        {
            _MedicineProxy = MedicineProxy;
        }
        public async Task<List<MedicineModel>> GetAllMedicine(MedicineRequest request)
        {
            var medicine = await _MedicineProxy.GetAllMedicine(request);
            return medicine.Data.MapTo<MedicineModel>();
        }
    }
}
