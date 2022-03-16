﻿using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Input;
using Phoenix.Shared.Vendor;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IInputService
    {
        Task<BaseResponse<InputDto>> GetAllInput(InputRequest request);
        Task<CrudResult> CreateInput(InputRequest request);
    }
    public class InputService : IInputService
    {
        private readonly DataContext _dataContext;
        public InputService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<InputDto>> GetAllInput(InputRequest request)
        {
            //setup query
            var result = new BaseResponse<InputDto>();
            try
            {
                // setup query
                var query = _dataContext.Inputs.AsQueryable();

                

                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<InputDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        // Task<CrudResult> CreateInput(InputRequest request);
        public async Task<CrudResult> CreateInput(InputRequest request)
        {
            var Input = new Input();
            Input.Id = request.Id;
            Input.IdStaff = request.IdStaff;
            Input.DateInput = request.DateInput;
           
            _dataContext.Inputs.Add(Input);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
    }
}
