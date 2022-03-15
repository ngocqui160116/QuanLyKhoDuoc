using Phoenix.Mobile.Core.Models.Supplier;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Supplier;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface ISupplierService
    {
        Task<List<SupplierModel>> GetAllSupplier(SupplierRequest request);
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

        
    }
}
