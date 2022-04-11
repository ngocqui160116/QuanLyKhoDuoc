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
        Task<CrudResult> CreateStaff(StaffRequest request);
        Task<CrudResult> UpdateStaff(int IdStaff, StaffRequest request);
        Task<CrudResult> DeleteStaff(int IdStaff);
        StaffDto GetStaffById(int User_Id);

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

                query = query.OrderByDescending(d => d.IdStaff);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<StaffDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        //public StaffDto GetStaffById(int User_Id)
        //{

        //        // setup query
        //        var query = _dataContext.Staffs.ToList().FindIndex( d=> d.User_Id == User_Id);


        //    var ada = query.Equals(StaffDto);

        //    return query.MapTo<StaffDto>();



        //}

        public StaffDto GetStaffById(int User_Id)
        {
            var staff = _dataContext.Staffs.ToList().FindLast(d => d.User_Id == User_Id);


              return  staff.MapTo<StaffDto>();
        }
        //public StaffDto GetStaffById(int User_Id)
        //{
        //    var result = new StaffDto();
        //    try
        //    {
        //        // setup query
        //        var query = _dataContext.Staffs.AsQueryable().MapTo<StaffDto>();


        //        // filter

        //        if (!string.IsNullOrEmpty(User_Id.ToString()))
        //        {
        //            query = query.Where(d => d.User_Id.ToString().Contains(User_Id.ToString()));
        //        }

        //        query = query.OrderByDescending(d => d.IdStaff);

        //        return query.MapTo<StaffDto>();
               
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    //return data;
        //}

        // Task<CrudResult> CreateStaff(StaffRequest request);
        public async Task<CrudResult> CreateStaff(StaffRequest request)
        {
            var Staff = new Staff();
            Staff.Name = request.Name;
            Staff.Gender = request.Gender;
            Staff.Birth = request.Birth;
            Staff.PhoneNumber = request.PhoneNumber;
            Staff.Address = request.Address;
            Staff.Authority = request.Authority;

            _dataContext.Staffs.Add(Staff);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        //Task<CrudResult> UpdateStaff(int IdStaff, StaffRequest request);
        //Task<CrudResult> DeleteStaff(int IdStaff);
        public async Task<CrudResult> UpdateStaff(int IdStaff, StaffRequest request)
        {
            var Staff = _dataContext.Staffs.Find(IdStaff);
            Staff.Name = request.Name;
            Staff.Gender = request.Gender;
            Staff.Birth = request.Birth;
            Staff.PhoneNumber = request.PhoneNumber;
            Staff.Address = request.Address;
            Staff.Authority = request.Authority;

            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> DeleteStaff(int IdStaff)
        {
            var Staff = _dataContext.Staffs.Find(IdStaff);
            if (Staff == null)
                return new CrudResult()
                {
                    ErrorCode = CommonErrorStatus.KeyNotFound,
                    ErrorDescription = "Xoá không thành công."
                };
            _dataContext.Staffs.Remove(Staff);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

    }
}
