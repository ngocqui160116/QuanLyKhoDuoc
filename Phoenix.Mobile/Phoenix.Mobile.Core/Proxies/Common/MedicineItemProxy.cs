using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.MedicineItem;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{

    public interface IMedicineItemProxy
    {
        Task<BaseResponse<MedicineItemDto>> GetAllMedicineItems(MedicineItemRequest request);
        Task<BaseResponse<MedicineItemDto>> GetMedicineItemById(int Id);
        Task<CrudResult> AddMedicineItem(MedicineItemRequest request);
        Task<CrudResult> AddItemInventory(MedicineItemRequest request);
        Task<CrudResult> UpdateMedicineItem(int Id, MedicineItemRequest request);
        Task<CrudResult> UpdateItemInventory(int Id, MedicineItemRequest request);
        Task<CrudResult> RemoveMedicineItem(int Id);
        Task<CrudResult> DeleteAll();
    }

    public class MedicineItemProxy : BaseProxy, IMedicineItemProxy
    {
        public async Task<BaseResponse<MedicineItemDto>> GetAllMedicineItems(MedicineItemRequest request)
        {
            try
            {
                var api = RestService.For<IMedicineItemApi>(GetHttpClient());

                return await api.GetAllMedicineItems(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<BaseResponse<MedicineItemDto>> GetMedicineItemById(int Id)
        {
            try
            {
                var api = RestService.For<IMedicineItemApi>(GetHttpClient());

                return await api.GetMedicineItemById(Id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<CrudResult> AddMedicineItem(MedicineItemRequest request)
        {
            try
            {
                var api = RestService.For<IMedicineItemApi>(GetHttpClient());
                var result = await api.AddMedicineItem(request);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }
        public async Task<CrudResult> AddItemInventory(MedicineItemRequest request)
        {
            try
            {
                var api = RestService.For<IMedicineItemApi>(GetHttpClient());
                var result = await api.AddItemInventory(request);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }

        public async Task<CrudResult> UpdateMedicineItem(int Id, MedicineItemRequest request)
        {
            try
            {
                var api = RestService.For<IMedicineItemApi>(GetHttpClient());
                var result = await api.UpdateMedicineItem(Id, request);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }

        public async Task<CrudResult> UpdateItemInventory(int Id, MedicineItemRequest request)
        {
            try
            {
                var api = RestService.For<IMedicineItemApi>(GetHttpClient());
                var result = await api.UpdateItemInventory(Id, request);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }

        public async Task<CrudResult> RemoveMedicineItem(int Id)
        {
            try
            {
                var api = RestService.For<IMedicineItemApi>(GetHttpClient());
                var result = await api.RemoveMedicineItem(Id);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }

        public async Task<CrudResult> DeleteAll()
        {
            try
            {
                var api = RestService.For<IMedicineItemApi>(GetHttpClient());
                var result = await api.DeleteAll();
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }

        public interface IMedicineItemApi
        {
            [Post("/MedicineItem/GetAllMedicineItems")]
            Task<BaseResponse<MedicineItemDto>> GetAllMedicineItems( MedicineItemRequest request);
           
            [Post("/MedicineItem/GetMedicineItemById")]
            Task<BaseResponse<MedicineItemDto>> GetMedicineItemById(int Id);

            [Post("/MedicineItem/AddMedicineItem")]
            Task<CrudResult> AddMedicineItem(MedicineItemRequest request);

            [Post("/MedicineItem/AddItemInventory")]
            Task<CrudResult> AddItemInventory(MedicineItemRequest request);

            [Post("/MedicineItem/UpdateMedicineItem")]
            Task<CrudResult> UpdateMedicineItem(int Id, MedicineItemRequest request);

            [Post("/MedicineItem/UpdateItemInventory")]
            Task<CrudResult> UpdateItemInventory(int Id, MedicineItemRequest request);

            [Delete("/MedicineItem/RemoveMedicineItem")]
            Task<CrudResult> RemoveMedicineItem(int Id);

            [Delete("/MedicineItem/DeleteAll")]
            Task<CrudResult> DeleteAll();
        }

    }
}
