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

    }
}
