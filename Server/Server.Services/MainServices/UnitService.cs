using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Unit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IUnitService
    {
        Task<BaseResponse<UnitDto>> GetAllUnit(UnitRequest request);
        Task<CrudResult> CreateUnit(UnitRequest request);
    }
    public class UnitService : IUnitService
    {
        private readonly DataContext _dataContext;
        public UnitService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<UnitDto>> GetAllUnit(UnitRequest request)
        {
            //setup query
            var result = new BaseResponse<UnitDto>();
            try
            {
                // setup query
                var query = _dataContext.Units.AsQueryable();

                // filter
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(d => d.Name.Contains(request.Name));
                }

                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<UnitDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

         // Task<CrudResult> CreateUnit(UnitRequest request);
        public async Task<CrudResult> CreateUnit(UnitRequest request)
        {
            var Unit = new Unit();
            Unit.Name = request.Name;
            _dataContext.Units.Add(Unit);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
    }
}
