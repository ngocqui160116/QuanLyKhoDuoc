using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Supplier;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface ISupplierProxy
    {
        Task<List<SupplierDto>> GetAllSupplier(SupplierRequest request);
    }

    public class SupplierProxy : BaseProxy, ISupplierProxy
    {
        public async Task<List<SupplierDto>> GetAllSupplier(SupplierRequest request)
        {
            try
            {
                var api = RestService.For<ISupplierApi>(GetHttpClient());
                var result = await api.GetAllSupplier(request);
                if (result == null) return new List<SupplierDto>();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new List<SupplierDto>();
            }
        }
        public interface ISupplierApi
        {
            [Post("/supplier/GetAllSupplier")]
            Task<List<SupplierDto>> GetAllSupplier([Body] SupplierRequest request);

        }
    }
}
