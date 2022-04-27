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
        Task<List<MedicineItemModel>> GetMedicineItemById(int Id);
        Task<CrudResult> AddMedicineItem(MedicineItemRequest request);
        Task<CrudResult> AddItemInventory(MedicineItemRequest request);
        Task<CrudResult> UpdateMedicineItem(int Id, MedicineItemRequest request);
        Task<CrudResult> UpdateItemInventory(int Id, MedicineItemRequest request);
        Task<CrudResult> RemoveMedicineItem(int Id);
        Task<CrudResult> DeleteAll();
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

        public Task<CrudResult> AddItemInventory(MedicineItemRequest request)
        {
            return _MedicineItemProxy.AddItemInventory(request);
        }

        public async Task<List<MedicineItemModel>> GetMedicineItemById(int Id)
        {
            var MedicineItem = await _MedicineItemProxy.GetMedicineItemById(Id);
            return MedicineItem.Data.MapTo<MedicineItemModel>();
        }

        public Task<CrudResult> UpdateMedicineItem(int Id, MedicineItemRequest request)
        {
            return _MedicineItemProxy.UpdateMedicineItem(Id, request);
        }

        public Task<CrudResult> UpdateItemInventory(int Id, MedicineItemRequest request)
        {
            return _MedicineItemProxy.UpdateItemInventory(Id, request);
        }
        public Task<CrudResult> RemoveMedicineItem(int Id)
        {
            return _MedicineItemProxy.RemoveMedicineItem(Id);
        }

        public Task<CrudResult> DeleteAll()
        {
            return _MedicineItemProxy.DeleteAll();
        }

    }
}
