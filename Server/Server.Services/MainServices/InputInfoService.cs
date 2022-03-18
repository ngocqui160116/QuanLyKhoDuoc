﻿using Falcon.Core;
using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.InputInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IInputInfoService
    {
        Task<BaseResponse<InputInfoDto>> GetAllInputInfo(InputInfoRequest request);
        Task<CrudResult> CreateInputInfo(InputInfoRequest request);
        Task<CrudResult> UpdateInputInfo(string IdInput, InputInfoRequest request);
        Task<CrudResult> DeleteInputInfo(string IdInput);
    }
    public class InputInfoService : IInputInfoService
    {
        private readonly DataContext _dataContext;
        public InputInfoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<InputInfoDto>> GetAllInputInfo(InputInfoRequest request)
        {
            var result = new BaseResponse<InputInfoDto>();
            try
            {
                // setup query
                var query = _dataContext.InputInfos.AsQueryable();

                // filter
                if (!string.IsNullOrEmpty(request.IdInput))
                {
                    query = query.Where(d => d.IdInput.Contains(request.IdInput));
                }

                query = query.OrderByDescending(d => d.IdInput);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<InputInfoDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

       // Task<CrudResult> CreateInputInfo(InputInfoRequest request);
        public async Task<CrudResult> CreateInputInfo(InputInfoRequest request)
        {
            var InputInfo = new InputInfo();
            InputInfo.IdInput = request.IdInput;
            InputInfo.IdMedicine = request.IdMedicine;
            InputInfo.IdSupplier = request.IdSupplier;
            InputInfo.IdBatch = request.IdBatch;
            InputInfo.DateOfManufacture = request.DateOfManufacture;
            InputInfo.DueDate = request.DueDate;

            _dataContext.InputInfos.Add(InputInfo);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        
        public async Task<CrudResult> UpdateInputInfo(string IdInput, InputInfoRequest request)
        {
            var InputInfo = _dataContext.InputInfos.Find(IdInput);
            InputInfo.IdMedicine = request.IdMedicine;
            InputInfo.IdSupplier = request.IdSupplier;
            InputInfo.IdBatch = request.IdBatch;
            InputInfo.DateOfManufacture = request.DateOfManufacture;
            InputInfo.DueDate = request.DueDate;

            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> DeleteInputInfo(string IdInput)
        {
            var InputInfo = _dataContext.InputInfos.Find(IdInput);
            if (InputInfo == null)
                return new CrudResult()
                {
                    ErrorCode = CommonErrorStatus.KeyNotFound,
                    ErrorDescription = "Xoá không thành công."
                };
            _dataContext.InputInfos.Remove(InputInfo);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }


    }
}
