using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
  
    [RoutePrefix("api/Stock")]
    public class StockController : BaseApiController
    {

        private readonly IStockService _StockService;
        public StockController(IStockService StockService)
        {
            _StockService = StockService;
        }

        [HttpPost]
        [Route("GetAllStock")]
        public async Task<BaseResponse<StockDto>> GetAllStock([FromBody] StockRequest request)
        {
            return await _StockService.GetAllStock(request);
        }
    }
}