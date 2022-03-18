using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.OutputInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IOutputInfoService
    {
        Task<BaseResponse<OutputInfoDto>> GetAllOutputInfo(OutputInfoRequest request);
        Task<CrudResult> CreateOutputInfo(OutputInfoRequest request);
        Task<CrudResult> UpdateOutputInfo(string IdOutput, OutputInfoRequest request);
        Task<CrudResult> DeleteOutputInfo(string IdOutput);
    }
    public class OutputInfoService : IOutputInfoService
    {
        private readonly DataContext _dataContext;
        public OutputInfoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<OutputInfoDto>> GetAllOutputInfo(OutputInfoRequest request)
        {
            //setup query
            var result = new BaseResponse<OutputInfoDto>();
            try
            {
                // setup query
                var query = _dataContext.OutputInfos.AsQueryable();

                // filter
                if (!string.IsNullOrEmpty(request.IdOutput))
                {
                    query = query.Where(d => d.IdOutput.Contains(request.IdOutput));
                }
                query = query.OrderByDescending(d => d.IdOutput);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<OutputInfoDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        // Task<CrudResult> CreateOutputInfo(OutputInfoRequest request);
        public async Task<CrudResult> CreateOutputInfo(OutputInfoRequest request)
        {
            var OutputInfo = new OutputInfo();
            OutputInfo.IdOutput = request.IdOutput;
            OutputInfo.IdMedicine = request.IdMedicine;
            //OutputInfo.IdInputInfo = request.IdInputInfo;
            //OutputInfo.IdReason = request.IdReason;
            OutputInfo.Count = request.Count;
            OutputInfo.Total = request.Total;
            OutputInfo.Status = request.Status;
           

            _dataContext.OutputInfos.Add(OutputInfo);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        //Task<CrudResult> UpdateOutputInfo(int IdOutputInfo, OutputInfoRequest request);
        //Task<CrudResult> DeleteOutputInfo(int IdOutputInfo);
        public async Task<CrudResult> UpdateOutputInfo(string IdOutput, OutputInfoRequest request)
        {
            var OutputInfo = _dataContext.OutputInfos.Find(IdOutput);
            OutputInfo.IdMedicine = request.IdMedicine;
            OutputInfo.IdInputInfo = request.IdInputInfo;
            
            OutputInfo.Count = request.Count;
            OutputInfo.Total = request.Total;
            OutputInfo.Status = request.Status;

            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> DeleteOutputInfo(string IdOutput)
        {
            var OutputInfo = _dataContext.OutputInfos.Find(IdOutput);
            if (OutputInfo == null)
                return new CrudResult()
                {
                    ErrorCode = CommonErrorStatus.KeyNotFound,
                    ErrorDescription = "Xoá không thành công."
                };
            _dataContext.OutputInfos.Remove(OutputInfo);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
    }
}
