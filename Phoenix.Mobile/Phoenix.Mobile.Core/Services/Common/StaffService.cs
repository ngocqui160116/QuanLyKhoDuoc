using Phoenix.Mobile.Core.Models.Staff;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Core;
using Phoenix.Shared.Staff;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IStaffService
    {
        Task<List<StaffModel>> GetAllStaff(StaffRequest request);

        Task<StaffModel> GetStaffById(int User_Id);
        Task<StaffModel> AddStaff(StaffRequest request);
        Task<CrudResult> UpdateStaff(int IdStaff, StaffRequest request);
    }

    public class StaffService : IStaffService
    {
        private readonly IStaffProxy _StaffProxy;
        public StaffService(IStaffProxy StaffProxy)
        {
            _StaffProxy = StaffProxy;
        }
        public async Task<List<StaffModel>> GetAllStaff(StaffRequest request)
        {
            var staff = await _StaffProxy.GetAllStaff(request);
            return staff.Data.MapTo<StaffModel>();
        }

        public async Task<StaffModel> AddStaff(StaffRequest request)
        {
            var data = await _StaffProxy.AddStaff(request);
            return data.MapTo<StaffModel>();
        }

        public async Task<StaffModel> GetStaffById(int User_Id)
        {
            var data = await _StaffProxy.GetStaffById(User_Id);
            return data.MapTo<StaffModel>();
        }

        public Task<CrudResult> UpdateStaff(int IdStaff, StaffRequest request)
        {
            return _StaffProxy.UpdateStaff(IdStaff, request);
        }

    }
}
