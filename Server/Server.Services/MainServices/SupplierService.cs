using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Supplier;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface ISupplierService
    {
        Task<BaseResponse<SupplierDto>> GetAllSupplier(SupplierRequest request);
        Task<CrudResult> CreateSupplier(SupplierRequest request);
        Task<CrudResult> UpdateSupplier(int IdSupplier, SupplierRequest request);
        Task<CrudResult> DeleteSupplier(int IdSupplier);

        //
        Task<BaseResponse<SupplierDto>> Create(SupplierRequest request);
        Supplier GetSupplierById(int id);
        Task<BaseResponse<SupplierDto>> Update(SupplierRequest request);
        Task<BaseResponse<SupplierDto>> Delete(int IdSupplier);
    }
    public class SupplierService : ISupplierService
    {
        private readonly DataContext _dataContext;
        public SupplierService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<SupplierDto>> GetAllSupplier(SupplierRequest request)
        {
            var result = new BaseResponse<SupplierDto>();
            try
            {
                // setup query
                var query = _dataContext.Suppliers.AsQueryable();

                // filter
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(d => d.Address.Contains(request.Name));
                }
                if (!string.IsNullOrEmpty(request.Address))
                {
                    query = query.Where(d => d.Address.Contains(request.Address));
                }
                if (request.Deleted == false)
                {
                    query = query.Where(d => d.Deleted.Equals(request.Deleted));
                }
                query = query.OrderByDescending(d => d.IdSupplier);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<SupplierDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public async Task<CrudResult> CreateSupplier(SupplierRequest request)
        {
            var Supplier = new Supplier();
            Supplier.Name = request.Name;
            Supplier.PhoneNumber = request.PhoneNumber;
            Supplier.Email = request.Email;
            Supplier.Address = request.Address;
            _dataContext.Suppliers.Add(Supplier);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> UpdateSupplier(int IdSupplier, SupplierRequest request)
        {
            var Supplier = _dataContext.Suppliers.Find(IdSupplier);
            Supplier.Name = request.Name;
            Supplier.PhoneNumber = request.PhoneNumber;
            Supplier.Email = request.Email;
            Supplier.Address = request.Address;
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> DeleteSupplier(int IdSupplier)
        {
            var Supplier = _dataContext.Suppliers.Find(IdSupplier);
            if (Supplier == null)
                return new CrudResult()
                {
                    ErrorCode = CommonErrorStatus.KeyNotFound,
                    ErrorDescription = "Xoá không thành công."
                };
            _dataContext.Suppliers.Remove(Supplier);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        ////web
        public async Task<BaseResponse<SupplierDto>> Create(SupplierRequest request)
        {
            var result = new BaseResponse<SupplierDto>();
            try
            {
                Supplier supplier = new Supplier
                {
                    Name = request.Name,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    Email = request.Email,
                    Deleted = false
                };
                _dataContext.Suppliers.Add(supplier);
                await _dataContext.SaveChangesAsync();

                result.Success = true;
            }
            catch
            {

            }
            return result;
        }
        public Supplier GetSupplierById(int id) => _dataContext.Suppliers.Find(id);
        public async Task<BaseResponse<SupplierDto>> Update(SupplierRequest request)
        {
            var result = new BaseResponse<SupplierDto>();
            try
            {
                //Lay du lieu cu
                var supplier = GetSupplierById(request.IdSupplier);
                //cap nhat

                /*Supplier supplier = new Supplier
                {*/
                supplier.IdSupplier = request.IdSupplier;
                supplier.Name = request.Name;
                supplier.PhoneNumber = request.PhoneNumber;
                supplier.Email = request.Email;
                supplier.Address = request.Address;
                supplier.Deleted = false;
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
        public async Task<BaseResponse<SupplierDto>> Delete(int IdSupplier)
        {
            var result = new BaseResponse<SupplierDto>();
            try
            {
                var supplier = GetSupplierById(IdSupplier);
                //supplier.IdSupplier = request.IdSupplier;
                supplier.Deleted = true;
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
