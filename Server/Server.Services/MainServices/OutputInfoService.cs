using Falcon.Core;
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
            var result = new BaseResponse<OutputInfoDto>();
            try
            {
                //var inputList = _dataContext.InputInfos.Where(p => p.IdInput == request.Id);
                //var outputList = _dataContext.OutputInfos.Where(p => p.IdOutput == request.Id);

                //int sumInput = 0;
                //int sumOutput = 0;

                //if (inputList != null)
                //{
                //    sumInput = (int)inputList.Sum(p => p.Count);
                //}
                //if (outputList != null)
                //{
                //    sumOutput = (int)outputList.Sum(p => p.Count);
                //}

                //int Soluong = sumInput - sumOutput;

                // setup query
                var query = _dataContext.OutputInfos.AsQueryable();

                // filter
                if (!string.IsNullOrEmpty(request.IdOutput))
                {
                    query = query.Where(d => d.IdOutput.Contains(request.IdOutput));
                }
                //query = query.Where(d => d.Count.Equals(Soluong));

                


                query = query.OrderByDescending(d => d.Id);

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
            var Output = new Output();
            Output.Id = request.IdOutput;
            Output.IdStaff = request.IdStaff;
            Output.DateOutput = request.DateOutput;
            Output.IdReason = request.IdReason;
            Output.Status = request.Status;

            _dataContext.Outputs.Add(Output);
            await _dataContext.SaveChangesAsync();

            var OutputInfo = new OutputInfo();
            OutputInfo.IdOutput = request.IdOutput;
            OutputInfo.IdMedicine = request.IdMedicine;
            OutputInfo.IdInputInfo = request.IdInputInfo;
            OutputInfo.Count = request.Count;
            OutputInfo.Total = request.Total;


            _dataContext.OutputInfos.Add(OutputInfo);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
    }
}
