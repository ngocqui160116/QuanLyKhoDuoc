﻿using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Common;
using Phoenix.Shared.Stock;
using Phoenix.Shared.StockInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/StockInfo")]
    public class StockInfoController : BaseApiController
    {


        private readonly IStockInfoService _StockInfoService;
        public StockInfoController(IStockInfoService StockInfoService)
        {
            _StockInfoService = StockInfoService;
        }

        [HttpPost]
        [Route("GetAllStockInfo")]
        public async Task<BaseResponse<StockInfoDto>> GetAllStockInfo([FromBody] StockInfoRequest request)
        {
            return await _StockInfoService.GetAllStockInfo(request);
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