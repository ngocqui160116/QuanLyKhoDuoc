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
using Phoenix.Shared.StockInfo;

namespace Phoenix.Server.Services.MainServices
{
   
    public interface IStockInfoService
    {

        Task<BaseResponse<StockInfoDto>> GetAllStockInfo(StockInfoRequest request);

        /// 
        Task<BaseResponse<StockInfoDto>> GetAll(StockInfoRequest request);
        StockInfo GetLatestStockInfo();
    }
    public class StockInfoService : IStockInfoService
    {
        private readonly DataContext _dataContext;
        public StockInfoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<BaseResponse<StockInfoDto>> GetAllStockInfo(StockInfoRequest request)
        {
            var result = new BaseResponse<StockInfoDto>();

            try
            {
                // setup query
                var query = _dataContext.StockInfos.AsQueryable();

                // filter
                
                query = query.OrderByDescending(d => d.Id);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<StockInfoDto>();

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        /// 
        public async Task<BaseResponse<StockInfoDto>> GetAll(StockInfoRequest request)
        {
            var result = new BaseResponse<StockInfoDto>();

            try
            {
                // setup query
                var query = _dataContext.Stocks.AsQueryable();

                // filter

                query = query.OrderByDescending(d => d.Id);
                var i = query.Count();
                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<StockInfoDto>();

            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public StockInfo GetLatestStockInfo()
        {
            var query = _dataContext.StockInfos.AsQueryable();

            query = query.OrderByDescending(d => d.Id);
            var da = query.FirstOrDefault();
            return da;
        }
    }
}
