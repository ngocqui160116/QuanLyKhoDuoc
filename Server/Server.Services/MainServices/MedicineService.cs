using Falcon.Core;
using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Medicine;
using Phoenix.Shared.Supplier;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IMedicineService
    {
        Task<BaseResponse<MedicineDto>> GetAllMedicine(MedicineRequest request);
        Task<CrudResult> CreateMedicine(MedicineRequest request);
        Task<CrudResult> UpdateMedicine(int IdMedicine, MedicineRequest request);
        Task<CrudResult> DeleteMedicine(int IdMedicine);

        //
        Task<BaseResponse<MedicineDto>> GetAll(MedicineRequest request);
        Task<BaseResponse<MedicineDto>> Create(MedicineRequest request);
        Medicine GetMedicineById(int id);
        Task<BaseResponse<MedicineDto>> Update(MedicineRequest request);
        Task<BaseResponse<MedicineDto>> Delete(int IdMedicine);
    }
    public class MedicineService : IMedicineService
    {
        private readonly DataContext _dataContext;
        public MedicineService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách thuốc
        public async Task<BaseResponse<MedicineDto>> GetAllMedicine(MedicineRequest request)
        {
            var result = new BaseResponse<MedicineDto>();
            try
            {
                //setup query
                var query = _dataContext.Medicines.AsQueryable();
                //filter
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(d => d.Name.Contains(request.Name));
                }
                if (!string.IsNullOrEmpty(request.RegistrationNumber))
                {
                    query = query.Where(d => d.RegistrationNumber.Contains(request.RegistrationNumber));
                }

                if (!string.IsNullOrEmpty(request.Status))
                {
                    query = query.Where(d => d.Status.Contains(request.Status));
                }
                //if (request.Status.ToString() == "đang bán")
                /*if(request.Status.Contains(request.Status)
                {
                    query = query.Where(d => d.Status.Contains(request.Status));
                }*/
                query = query.OrderByDescending(d => d.IdMedicine);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<MedicineDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        // Task<CrudResult> CreateMedicine(MedicineRequest request);
        public async Task<CrudResult> CreateMedicine(MedicineRequest request)
        {
            var Medicine = new Medicine();
            
            Medicine.RegistrationNumber = request.RegistrationNumber;
            Medicine.Name = request.Name;
            Medicine.IdGroup = request.IdGroup;
            Medicine.IdUnit = request.IdUnit;
            Medicine.Active = request.Active;
            Medicine.Content = request.Content;
            Medicine.Packing = request.Packing;
            Medicine.Image = request.Image;
            Medicine.Status = request.Status;

            _dataContext.Medicines.Add(Medicine);
            await _dataContext.SaveChangesAsync();

            var inventory = new Inventory();
            inventory.IdMedicine = Medicine.IdMedicine;
            inventory.Count = 0;

            _dataContext.Inventories.Add(inventory);
            await _dataContext.SaveChangesAsync();


            return new CrudResult() { IsOk = true };
        }

        //Task<CrudResult> UpdateMedicine(int IdMedicine, MedicineRequest request);
        //Task<CrudResult> DeleteMedicine(int IdMedicine);
        public async Task<CrudResult> UpdateMedicine(int IdMedicine, MedicineRequest request)
        {
            var Medicine = _dataContext.Medicines.Find(IdMedicine);
            Medicine.RegistrationNumber = request.RegistrationNumber;
            Medicine.Name = request.Name;
            Medicine.IdGroup = request.IdGroup;
            Medicine.IdUnit = request.IdUnit;
            Medicine.Active = request.Active;
            Medicine.Content = request.Content;
            Medicine.Packing = request.Packing;
            Medicine.Image = request.Image;
            Medicine.Status = request.Status;

            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> DeleteMedicine(int IdMedicine)
        {
            var Medicine = _dataContext.Medicines.Find(IdMedicine);
            if (Medicine == null)
                return new CrudResult()
                {
                    ErrorCode = CommonErrorStatus.KeyNotFound,
                    ErrorDescription = "Xoá không thành công."
                };
            _dataContext.Medicines.Remove(Medicine);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        //web
        public async Task<BaseResponse<MedicineDto>> GetAll(MedicineRequest request)
        {
            var result = new BaseResponse<MedicineDto>();
            try
            {
                //setup query
                var query = _dataContext.Medicines.AsQueryable();
                //filter
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(d => d.Name.Contains(request.Name));
                }
                if (!string.IsNullOrEmpty(request.RegistrationNumber))
                {
                    query = query.Where(d => d.RegistrationNumber.Contains(request.RegistrationNumber));
                }

                if (!string.IsNullOrEmpty(request.Status))
                {
                    query = query.Where(d => d.Status.Contains(request.Status));
                }
                //if (request.Status.ToString() == "đang bán")
                /*if(request.Status.Contains(request.Status)
                {
                    query = query.Where(d => d.Status.Contains(request.Status));
                }*/
                query = query.OrderByDescending(d => d.IdMedicine);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<MedicineDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public async Task<BaseResponse<MedicineDto>> Create(MedicineRequest request)
        {
            var result = new BaseResponse<MedicineDto>();
            try
            {
                Medicine medicines = new Medicine
                {
                    RegistrationNumber = request.RegistrationNumber,
                    Name = request.Name,
                    IdGroup = request.IdGroup,
                    Active = request.Active,
                    Content = request.Content,
                    Packing = request.Packing,
                    IdUnit = request.IdUnit,
                    Image = request.Image,
                    Status = request.Status
                };
                _dataContext.Medicines.Add(medicines);
                await _dataContext.SaveChangesAsync();

                result.Success = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public Medicine GetMedicineById(int id) => _dataContext.Medicines.Find(id);
        public async Task<BaseResponse<MedicineDto>> Update(MedicineRequest request)
        {
            var result = new BaseResponse<MedicineDto>();
            try
            {
                //Lay du lieu cu
                var medicine = GetMedicineById(request.IdMedicine);
                //cap nhat

                /*Supplier supplier = new Supplier
                {*/
                medicine.IdMedicine = request.IdMedicine;
                medicine.RegistrationNumber = request.RegistrationNumber;
                medicine.Name = request.Name;
                medicine.IdGroup = request.IdGroup;
                medicine.Active = request.Active;
                medicine.Content = request.Content;
                medicine.Packing = request.Packing;
                medicine.IdUnit = request.IdUnit;
                medicine.Image = request.Image;
                medicine.Status = request.Status;
                //};
                //_dataContext.Suppliers.Add(supplier);
                await _dataContext.SaveChangesAsync();

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<BaseResponse<MedicineDto>> Delete(int IdMedicine)
        {
            var result = new BaseResponse<MedicineDto>();
            try
            {
                var medicine = GetMedicineById(IdMedicine);
                //supplier.IdSupplier = request.IdSupplier;
                medicine.Status = "Nghỉ bán";
                //_dataContext.Suppliers.Remove(Supplier);
                await _dataContext.SaveChangesAsync();

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
