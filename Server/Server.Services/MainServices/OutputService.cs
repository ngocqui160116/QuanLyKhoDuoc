using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Output;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IOutputService
    {
        Task<BaseResponse<OutputDto>> GetAllOutput(OutputRequest request);
        Task<CrudResult> CreateOutput(OutputRequest request);
    }
    public class OutputService : IOutputService
    {
        private readonly DataContext _dataContext;
        public OutputService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<OutputDto>> GetAllOutput(OutputRequest request)
        {
            //setup query
            var result = new BaseResponse<OutputDto>();
            try
            {
                // setup query
                var query = _dataContext.Outputs.AsQueryable();



                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<OutputDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        // Task<CrudResult> CreateOutput(OutputRequest request);
        public async Task<CrudResult> CreateOutput(OutputRequest request)
        {
            var Output = new Output();
            Output.Id = request.Id;
            Output.IdStaff = request.IdStaff;
            Output.DateOutput = request.DateOutput;

            _dataContext.Outputs.Add(Output);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
    }
}
