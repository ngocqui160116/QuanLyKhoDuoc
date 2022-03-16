﻿using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Staff;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IStaffService
    {
        Task<BaseResponse<StaffDto>> GetAllStaff(StaffRequest request);
    }
    public class StaffService : IStaffService
    {
        private readonly DataContext _dataContext;
        public StaffService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<StaffDto>> GetAllStaff(StaffRequest request)
        {
            var result = new BaseResponse<StaffDto>();
            try
            {
                // setup query
                var query = _dataContext.Staffs.AsQueryable();

                // filter
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

                query = query.OrderBy(d => d.IdStaff);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<StaffDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
       
        //public int IdStaff { get; set; }
        //public string Name { get; set; }
        //public string Gender { get; set; }
        //public string Authority { get; set; }
        //public bool Deleted { get; set; }
    }
}
