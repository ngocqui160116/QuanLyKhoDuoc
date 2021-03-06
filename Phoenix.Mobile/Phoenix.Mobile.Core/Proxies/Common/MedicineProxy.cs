using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Medicine;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IMedicineProxy
    {
        Task<BaseResponse<MedicineDto>> GetAllMedicine(MedicineRequest request);
        Task<MedicineDto> AddMedicine(MedicineRequest request);
        Task<CrudResult> UpdateMedicine(int IdMedicine, MedicineRequest request);
        Task<CrudResult> DeleteMedicine(int IdMedicine);
    }

    public class MedicineProxy : BaseProxy, IMedicineProxy
    {
        public async Task<BaseResponse<MedicineDto>> GetAllMedicine(MedicineRequest request)
        {
            try
            {
                var api = RestService.For<IMedicineApi>(GetHttpClient());

                return await api.GetAllMedicine(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }
        public async Task<MedicineDto> AddMedicine(MedicineRequest request)
        {
            try
            {
                var api = RestService.For<IMedicineApi>(GetHttpClient());
                var result = await api.AddMedicine(request);
                if (result == null) return new MedicineDto();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new MedicineDto();
            }
        }
        public async Task<CrudResult> UpdateMedicine(int IdMedicine,MedicineRequest request)
        {
            try
            {
                var api = RestService.For<IMedicineApi>(GetHttpClient());
                var result = await api.UpdateMedicine(IdMedicine, request);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }

        public async Task<CrudResult> DeleteMedicine(int IdMedicine)
        {
            try
            {
                var api = RestService.For<IMedicineApi>(GetHttpClient());
                var result = await api.DeleteMedicine(IdMedicine);
                if (result == null) return new CrudResult();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new CrudResult();
            }
        }

        public interface IMedicineApi
        {
            [Post("/medicine/GetAllMedicine")]
            Task<BaseResponse<MedicineDto>> GetAllMedicine([Body] MedicineRequest request);

            [Post("/medicine/CreateMedicine")]
            Task<MedicineDto> AddMedicine([Body] MedicineRequest request);

            [Post("/medicine/UpdateMedicine")]
            Task<CrudResult> UpdateMedicine(int IdMedicine, [Body]  MedicineRequest request);
            
            [Delete("/medicine/DeleteMedicine")]
            Task<CrudResult> DeleteMedicine(int IdMedicine);
        }

    }
}
