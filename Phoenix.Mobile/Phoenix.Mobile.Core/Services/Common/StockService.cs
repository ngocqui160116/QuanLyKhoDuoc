using Phoenix.Mobile.Core.Models.Stock;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Stock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IStockService
    {
        Task<List<StockModel>> GetAllStock(StockRequest request);
        //Task<StockModel> AddStock(StockRequest request);
    }

    public class StockService : IStockService
    {
        private readonly IStockProxy _StockProxy;
        public StockService(IStockProxy StockProxy)
        {
            _StockProxy = StockProxy;
        }
        public async Task<List<StockModel>> GetAllStock(StockRequest request)
        {
            var Stock = await _StockProxy.GetAllStock(request);
            return Stock.Data.MapTo<StockModel>();
        }
        //public async Task<StockModel> AddStock(StockRequest request)
        //{
        //    var data = await _StockProxy.AddStock(request);
        //    return data.MapTo<StockModel>();
        //}
    }
}
