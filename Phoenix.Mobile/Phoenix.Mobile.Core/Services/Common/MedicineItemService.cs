using Phoenix.Mobile.Core.Models.MedicineItem;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Core;
using Phoenix.Shared.MedicineItem;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
  

    public interface IMedicineItemService
    {
        Task<List<MedicineItemModel>> GetAllMedicineItem(MedicineItemRequest request);
        Task<CrudResult> AddMedicineItem(MedicineItemRequest request);
    }

    public class MedicineItemService : IMedicineItemService
    {
        private readonly IMedicineItemProxy _MedicineItemProxy;
        public MedicineItemService(IMedicineItemProxy MedicineItemProxy)
        {
            _MedicineItemProxy = MedicineItemProxy;
        }
        public async Task<List<MedicineItemModel>> GetAllMedicineItem(MedicineItemRequest request)
        {
            var MedicineItem = await _MedicineItemProxy.GetAllMedicineItems(request);
            return MedicineItem.Data.MapTo<MedicineItemModel>();
        }
        public Task<CrudResult> AddMedicineItem(MedicineItemRequest request)
        {
            return _MedicineItemProxy.AddMedicineItem(request);
        }
    }
}
