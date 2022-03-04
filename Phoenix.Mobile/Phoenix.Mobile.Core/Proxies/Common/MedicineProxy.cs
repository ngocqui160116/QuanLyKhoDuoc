using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
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
        Task<List<MedicineDto>> GetAllMedicine(MedicineRequest request);
    }

    public class MedicineProxy : BaseProxy, IMedicineProxy
    {
        public async Task<List<MedicineDto>> GetAllMedicine(MedicineRequest request)
        {
            try
            {
                var api = RestService.For<IMedicineApi>(GetHttpClient());
                var result = await api.GetAllMedicine(request);
                if (result == null) return new List<MedicineDto>();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new List<MedicineDto>();
            }
        }
        public interface IMedicineApi
        {
            [Post("/medicine/GetAllMedicine")]
            Task<List<MedicineDto>> GetAllMedicine([Body] MedicineRequest request);

        }
    }
}
