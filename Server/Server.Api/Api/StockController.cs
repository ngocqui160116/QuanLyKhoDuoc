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

        //[HttpPost]
        //[Route("CreateStock")]
        //public Task<CrudResult> CreateStock([FromBody] StockRequest request)
        //{
        //    return _StockService.CreateStock(request);
        //}

        //[HttpPost]
        //[Route("UpdateStock")]
        //public Task<CrudResult> UpdateStock(int IdStock, [FromBody] StockRequest request)
        //{
        //    return _StockService.UpdateStock(IdStock, request);
        //}

        //[HttpDelete]
        //[Route("DeleteStock")]
        //public Task<CrudResult> DeleteStock(int IdStock)
        //{
        //    return _StockService.DeleteStock(IdStock);
        //}
    }
}