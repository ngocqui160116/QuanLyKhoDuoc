using Phoenix.Mobile.Core.Models.StockInfo;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.StockInfo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IStockInfoService
    {
        Task<List<StockInfoModel>> GetAllStockInfo(StockInfoRequest request);
        //Task<StockInfoModel> AddStockInfo(StockInfoRequest request);
    }

    public class StockInfoService : IStockInfoService
    {
        private readonly IStockInfoProxy _StockInfoProxy;
        public StockInfoService(IStockInfoProxy StockInfoProxy)
        {
            _StockInfoProxy = StockInfoProxy;
        }
        public async Task<List<StockInfoModel>> GetAllStockInfo(StockInfoRequest request)
        {
            var StockInfo = await _StockInfoProxy.GetAllStockInfo(request);
            return StockInfo.Data.MapTo<StockInfoModel>();
        }
        //public async Task<StockInfoModel> AddStockInfo(StockInfoRequest request)
        //{
        //    var data = await _StockInfoProxy.AddStockInfo(request);
        //    return data.MapTo<StockInfoModel>();
        //}
    }
}
