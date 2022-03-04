using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Staff;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Server.Services.MainServices
{
    public interface IStaffService
    {
        List<StaffDto> GetAllStaff(StaffRequest request);
    }
    public class StaffService : IStaffService
    {
        private readonly DataContext _dataContext;
        public StaffService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public List<StaffDto> GetAllStaff(StaffRequest request)
        {
            //setup query
            var query = _dataContext.Staffs.AsQueryable();
            //filter

            //if (!string.IsNullOrEmpty(request.IdStaff.ToString()))
            //{
            //    query = query.Where(d => d.IdStaff.ToString().Contains(request.IdStaff.ToString()));
            //}
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(d => d.Name.Contains(request.Name));
            }
            
            if (!string.IsNullOrEmpty(request.Gender))
            {
                query = query.Where(d => d.Gender.Contains(request.Gender));
            }
          
            if (!string.IsNullOrEmpty(request.Authority))
            {
                query = query.Where(d => d.Authority.Contains(request.Authority));
            }
          
         
            var data = query.ToList();
            return data.MapTo<StaffDto>();
        }
    }
}
