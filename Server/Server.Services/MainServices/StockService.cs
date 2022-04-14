using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.OutputInfo;
using Phoenix.Shared.Inventory;
using System.Collections.Generic;
using System.Linq;
using Phoenix.Shared.Common;
using System.Threading.Tasks;
using System;
using System.Data.Entity;
using AutoMapper;
using System.Collections.ObjectModel;
using Phoenix.Shared.Stock;

namespace Phoenix.Server.Services.MainServices
{
   
    public interface IStockService
    {

        Task<BaseResponse<StockDto>> GetAllStock(StockRequest request);

        /// 
        Task<BaseResponse<StockDto>> GetAll(StockRequest request);
    }
    public class StockService : IStockService
    {
        private readonly DataContext _dataContext;
        public StockService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<BaseResponse<StockDto>> GetAllStock(StockRequest request)
        {
            var result = new BaseResponse<StockDto>();

            try
            {
                // setup query
                var query = _dataContext.Stocks.AsQueryable();

                // filter
                
                query = query.OrderByDescending(d => d.Id);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<StockDto>();

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        /// 
        public async Task<BaseResponse<StockDto>> GetAll(StockRequest request)
        {
            var result = new BaseResponse<StockDto>();

            try
            {
                // setup query
                var query = _dataContext.Stocks.AsQueryable();

                // filter

                query = query.OrderByDescending(d => d.Id);
                var i = query.Count();
                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<StockDto>();

            }
            catch (Exception ex)
            {

            }

            return result;
        }
       
    }
}
