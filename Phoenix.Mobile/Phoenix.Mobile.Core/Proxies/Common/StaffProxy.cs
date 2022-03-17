using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
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
        Task<BaseResponse<StaffDto>> GetAllStaff(StaffRequest request);
        Task<StaffDto> AddStaff(StaffRequest request);
    }

    public class StaffProxy : BaseProxy, IStaffProxy
    {
        public async Task<BaseResponse<StaffDto>> GetAllStaff(StaffRequest request)
        {
            try
            {
                var api = RestService.For<IStaffApi>(GetHttpClient());

                return await api.GetAllStaff(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }
        public async Task<StaffDto> AddStaff(StaffRequest request)
        {
            try
            {
                var api = RestService.For<IStaffApi>(GetHttpClient());
                var result = await api.AddStaff(request);
                if (result == null) return new StaffDto();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new StaffDto();
            }
        }

        public interface IStaffApi
        {
            [Post("/staff/GetAllStaff")]
            Task<BaseResponse<StaffDto>> GetAllStaff([Body] StaffRequest request);

            [Post("/staff/CreateStaff")]
            Task<StaffDto> AddStaff([Body] StaffRequest request);
        }

    }
}
