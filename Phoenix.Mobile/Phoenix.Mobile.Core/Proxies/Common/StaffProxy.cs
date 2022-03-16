using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Staff;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IStaffProxy
    {
        Task<List<StaffDto>> GetAllStaff(StaffRequest request);
    }

    public class StaffProxy : BaseProxy, IStaffProxy
    {
        public async Task<List<StaffDto>> GetAllStaff(StaffRequest request)
        {
            try
            {
                var api = RestService.For<IStaffApi>(GetHttpClient());
                var result = await api.GetAllStaff(request);
                if (result == null) return new List<StaffDto>();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new List<StaffDto>();
            }
        }
        public interface IStaffApi
        {
            [Post("/staff/GetAllStaff")]
            Task<List<StaffDto>> GetAllStaff([Body] StaffRequest request);

        }
    }
}
