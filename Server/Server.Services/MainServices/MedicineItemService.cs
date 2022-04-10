using AutoMapper;
using Falcon.Web.Core.Helpers;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Server.Data.Entity;
using Phoenix.Shared.MedicineItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IMedicineItemService
    {
        //Task<BaseResponse<MedicineItemDto>> GetAllMedicineItems(MedicineItemRequest request);
        Task<BaseResponse<MedicineItemDto>> GetAllMedicineItems(MedicineItemRequest request);
        Task<BaseResponse<MedicineItemDto>> GetMedicineItemById(int Id);
        Task<CrudResult> UpdateMedicineItem(int Medicine_Id, MedicineItemRequest request);
        Task<CrudResult> AddMedicineItem(MedicineItemRequest request);
        Task<CrudResult> AddItemInventory(MedicineItemRequest request);
        Task<CrudResult> RemoveMedicineItem(int Id);

        //Task<CrudResult> UpdateCart(int Id, MedicineItemRequest request);
    }

    public class MedicineItemService : IMedicineItemService
    {
        private readonly DataContext _dataContext;

        public MedicineItemService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }



        // Lấy danh sách nhà cung cấp
        
        public async Task<BaseResponse<MedicineItemDto>> GetAllMedicineItems(MedicineItemRequest request)
        {
            var result = new BaseResponse<MedicineItemDto>();
            try
            {
                var query = (from c in _dataContext.MedicineItems
                             join s in _dataContext.Medicines on c.Medicine_Id equals s.IdMedicine
                             select new
                             {
                                 Id = c.Id,
                                 MedicineId = s.IdMedicine,
                                 MedicineName = s.Name,
                                 RegistrationNumber = s.RegistrationNumber
                             }).AsQueryable();

                var config = new MapperConfiguration(cfg => cfg.CreateMissingTypeMaps = true);
                var mapper = config.CreateMapper();
                var listcart = query.Select(mapper.Map<MedicineItemDto>).ToList();

                //var data = await query.ToListAsync();

                //result.Data = data.MapTo<MedicineItemDto>();
                result.Data = listcart.MapTo<MedicineItemDto>();

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public async Task<BaseResponse<MedicineItemDto>> GetMedicineItemById(int Id)
        {
            var result = new BaseResponse<MedicineItemDto>();
            try
            {
                var query = (from c in _dataContext.MedicineItems
                             join s in _dataContext.Medicines on c.Medicine_Id equals s.IdMedicine
                             select new
                             {
                                 Id = c.Id,
                                 MedicineId = s.IdMedicine,
                                 MedicineName = s.Name,
                                 RegistrationNumber = s.RegistrationNumber
                             }).AsQueryable();

                if (Id > 0)
                {
                    query = query.Where(d => d.Id == Id);
                }

                var config = new MapperConfiguration(cfg => cfg.CreateMissingTypeMaps = true);
                var mapper = config.CreateMapper();
                var listcart = query.Select(mapper.Map<MedicineItemDto>).ToList();
                result.Data = listcart.MapTo<MedicineItemDto>();

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public async Task<CrudResult> AddMedicineItem(MedicineItemRequest request)
        {
            var MedicineItem = new MedicineItem();
            MedicineItem.Medicine_Id = request.Medicine_Id;
            MedicineItem.Batch = 0;
            MedicineItem.Count = 0;
            MedicineItem.InputPrice = 0;
            MedicineItem.Total = request.InputPrice * request.Count;
            MedicineItem.DueDate = DateTime.Now;


            _dataContext.MedicineItems.Add(MedicineItem);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> AddItemInventory(MedicineItemRequest request)
        {
            var MedicineItem = new MedicineItem();
            MedicineItem.Medicine_Id = request.Medicine_Id;
            MedicineItem.Batch = request.Batch;
            MedicineItem.Count = request.Count;
            MedicineItem.InputPrice = request.InputPrice;
            MedicineItem.Total = request.InputPrice * request.Count;
            MedicineItem.DueDate = DateTime.Now;


            _dataContext.MedicineItems.Add(MedicineItem);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> UpdateMedicineItem(int Id, MedicineItemRequest request)
        {
            var MedicineItem = _dataContext.MedicineItems.Find(Id);
            MedicineItem.Medicine_Id = request.Medicine_Id;
            MedicineItem.Batch = request.Batch;
            MedicineItem.Count = request.Count;
            MedicineItem.InputPrice = request.InputPrice;
            MedicineItem.Total = request.InputPrice * request.Count;
            MedicineItem.DueDate = request.DueDate;

            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> RemoveMedicineItem(int Id)
        {
            var MedicineItem = _dataContext.MedicineItems.Find(Id);
            if (MedicineItem == null)
            {
                return new CrudResult()
                {
                    ErrorCode = CommonErrorStatus.KeyNotFound,
                    ErrorDescription = "Xóa không thành công."
                };
            }
            else
            {
                _dataContext.MedicineItems.Remove(MedicineItem);
                await _dataContext.SaveChangesAsync();
                return new CrudResult() { IsOk = true };
            }
        }

    }
}
