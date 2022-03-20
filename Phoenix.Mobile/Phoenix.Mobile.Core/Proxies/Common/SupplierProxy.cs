using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
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
        Task<SupplierDto> AddSupplier(SupplierRequest request);
        Task<CrudResult> UpdateSupplier(int IdSupplier, SupplierRequest request);
        Task<CrudResult> DeleteSupplier(int IdSupplier);
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

        public async Task<SupplierDto> AddSupplier(SupplierRequest request)
        {
            try
            {
                var api = RestService.For<ISupplierApi>(GetHttpClient());
                var result = await api.AddSupplier(request);
                if (result == null) return new SupplierDto();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new SupplierDto();
            }
        }


        public async Task<CrudResult> UpdateSupplier(int IdSupplier, SupplierRequest request)
        {
            try
            {
                var api = RestService.For<ISupplierApi>(GetHttpClient());
                var result = await api.UpdateSupplier(IdSupplier, request);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }

        public async Task<CrudResult> DeleteSupplier(int IdSupplier)
        {
            try
            {
                var api = RestService.For<ISupplierApi>(GetHttpClient());
                var result = await api.DeleteSupplier(IdSupplier);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }

        public interface ISupplierApi
        {
            [Post("/supplier/GetAllSupplier")]
            Task<BaseResponse<SupplierDto>> GetAllSupplier([Body] SupplierRequest request);
            
            [Post("/supplier/CreateSupplier")]
            Task<SupplierDto> AddSupplier([Body] SupplierRequest request);

            [Post("/Supplier/UpdateSupplier")]
            Task<CrudResult> UpdateSupplier(int IdSupplier, [Body] SupplierRequest request);

            [Delete("/Supplier/DeleteSupplier")]
            Task<CrudResult> DeleteSupplier(int IdSupplier);
        }


    }
}
