using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Common;
using Phoenix.Shared.Stock;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{

    public interface IStockProxy
    {
        Task<BaseResponse<StockDto>> GetAllStock(StockRequest request);
        //Task<StockDto> AddStock(StockRequest request);
    }

    public class StockProxy : BaseProxy, IStockProxy
    {
        public async Task<BaseResponse<StockDto>> GetAllStock(StockRequest request)
        {
            try
            {
                var api = RestService.For<IStockApi>(GetHttpClient());

                return await api.GetAllStock(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return null;
            }
        }

        //public async Task<StockDto> AddStock(StockRequest request)
        //{
        //    try
        //    {
        //        var api = RestService.For<IStockApi>(GetHttpClient());
        //        var result = await api.AddStock(request);
        //        if (result == null) return new StockDto();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHandler.Handle(new NetworkException(ex), true);
        //        return new StockDto();
        //    }
        //}

        public interface IStockApi
        {
            [Post("/Stock/GetAllStock")]
            Task<BaseResponse<StockDto>> GetAllStock([Body] StockRequest request);

            //[Post("/Stock/CreateStock")]
            //Task<StockDto> AddStock([Body] StockRequest request);


        }

    }
}
