using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
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

        public interface IMedicineApi
        {
            [Post("/medicine/GetAllMedicine")]
            Task<BaseResponse<MedicineDto>> GetAllMedicine([Body] MedicineRequest request);

        }

    }
}
