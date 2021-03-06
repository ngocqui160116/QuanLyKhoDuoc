using Phoenix.Mobile.Core.Models.Supplier;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Core;
using Phoenix.Shared.Supplier;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface ISupplierService
    {
        Task<List<SupplierModel>> GetAllSupplier(SupplierRequest request);
        Task<SupplierModel> AddSupplier(SupplierRequest request);
        Task<CrudResult> UpdateSupplier(int IdSupplier, SupplierRequest request);
        Task<CrudResult> DeleteSupplier(int IdSupplier);
    }

    public class SupplierService : ISupplierService
    {
        private readonly ISupplierProxy _SupplierProxy;
        public SupplierService(ISupplierProxy SupplierProxy)
        {
            _SupplierProxy = SupplierProxy;
        }

        public async Task<List<SupplierModel>> GetAllSupplier(SupplierRequest request)
        {
            var supplier = await _SupplierProxy.GetAllSupplier(request);
            return supplier.Data.MapTo<SupplierModel>();
        }

        public async Task<SupplierModel> AddSupplier(SupplierRequest request)
        {
            var data = await _SupplierProxy.AddSupplier(request);
            return data.MapTo<SupplierModel>();
        }
        public Task<CrudResult> DeleteSupplier(int IdSupplier)
        {
            return _SupplierProxy.DeleteSupplier(IdSupplier);
        }

        public Task<CrudResult> UpdateSupplier(int IdSupplier, SupplierRequest request)
        {
            return _SupplierProxy.UpdateSupplier(IdSupplier, request);
        }



    }
}
