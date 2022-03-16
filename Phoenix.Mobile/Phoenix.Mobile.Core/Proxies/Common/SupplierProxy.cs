using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
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
        Task<BaseResponse<SupplierDto>> GetAllSupplier(SupplierRequest request);
    }

    public class SupplierProxy : BaseProxy, ISupplierProxy
    {
        public async Task<BaseResponse<SupplierDto>> GetAllSupplier(SupplierRequest request)
        {
            try
            {
                var api = RestService.For<ISupplierApi>(GetHttpClient());

                return await api.GetAllSupplier(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public interface ISupplierApi
        {
            [Post("/supplier/GetAllSupplier")]
            Task<BaseResponse<SupplierDto>> GetAllSupplier([Body] SupplierRequest request);

        }

    }
}
