using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Reason;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IReasonService
    {
        Task<BaseResponse<ReasonDto>> GetAllReason(ReasonRequest request);
    }
    public class ReasonService : IReasonService
    {
        private readonly DataContext _dataContext;
        public ReasonService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<ReasonDto>> GetAllReason(ReasonRequest request)
        {
            var result = new BaseResponse<ReasonDto>();
            try
            {
                // setup query
                var query = _dataContext.Reasons.AsQueryable();

                // filter
                if (!string.IsNullOrEmpty(request.NameReason))
                {
                    query = query.Where(d => d.NameReason.Contains(request.NameReason));
                }
                query = query.OrderByDescending(d => d.IdReason);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<ReasonDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }


    }
}
