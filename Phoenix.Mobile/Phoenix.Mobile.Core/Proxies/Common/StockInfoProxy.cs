using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.Stock;
using Phoenix.Shared.StockInfo;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{

    public interface IStockInfoProxy
    {
        Task<BaseResponse<StockInfoDto>> GetAllStockInfo(StockInfoRequest request);
        Task<BaseResponse<StockInfoDto>> CreateStockInfo(StockInfoRequest request);
    }

    public class StockInfoProxy : BaseProxy, IStockInfoProxy
    {
        public async Task<BaseResponse<StockInfoDto>> GetAllStockInfo(StockInfoRequest request)
        {
            try
            {
                var api = RestService.For<IStockInfoApi>(GetHttpClient());

                return await api.GetAllStockInfo(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public async Task<BaseResponse<StockInfoDto>> CreateStockInfo(StockInfoRequest request)
        {
            try
            {
                var api = RestService.For<IStockInfoApi>(GetHttpClient());

                return await api.CreateStockInfo(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        public interface IStockInfoApi
        {
            [Post("/StockInfo/GetAllStockInfo")]
            Task<BaseResponse<StockInfoDto>> GetAllStockInfo([Body] StockInfoRequest request);

            [Post("/StockInfo/CreateStockInfo")]
            Task<BaseResponse<StockInfoDto>> CreateStockInfo(StockInfoRequest request);


        }

    }
}
