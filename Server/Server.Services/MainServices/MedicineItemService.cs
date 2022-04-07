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
        Task<CrudResult> AddMedicineItem(MedicineItemRequest request);
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
        //public async Task<BaseResponse<MedicineItemDto>> GetAllMedicineItems(MedicineItemRequest request)
        public async Task<BaseResponse<MedicineItemDto>> GetAllMedicineItems(MedicineItemRequest request)
        {
            var result = new BaseResponse<MedicineItemDto>();
            try
            {
                var query = (from c in _dataContext.MedicineItems
                             join s in _dataContext.InputInfos on c.Inputinfo_Id equals s.Id
                             join p in _dataContext.Inputs on s.IdInput equals p.Id
                             select new
                             {
                                 Id = c.Id,
                                 MedicineId = s.IdMedicine,
                                 Name = s.Medicine.Name,
                                 IdSupplier = p.IdSupplier,
                                 SupplierName = p.Supplier.Name
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

        public async Task<CrudResult> AddMedicineItem(MedicineItemRequest request)
        {
            var MedicineItem = new MedicineItem();
            MedicineItem.Inputinfo_Id = request.Id;
      
            _dataContext.MedicineItems.Add(MedicineItem);
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
