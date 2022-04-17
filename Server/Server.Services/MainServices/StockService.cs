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
        Task<BaseResponse<StockDto>> Create(StockRequest request);
        Stock GetLatestStock();
    }
    public class StockService : IStockService
    {
        private readonly DataContext _dataContext;
        private readonly IInventoryService _inventoryService;
        private readonly IStockInfoService _stockinfoService;
        public StockService(DataContext dataContext, IInventoryService inventoryService, IStockInfoService stockinfoService)
        {
            _dataContext = dataContext;
            _inventoryService = inventoryService;
            _stockinfoService = stockinfoService;
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
                //var data = await query.ToListAsync();
                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<StockDto>();

            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #region GetLatestStock
        public Stock GetLatestStock()
        {
            var query = _dataContext.Stocks.AsQueryable();

            query = query.OrderByDescending(d => d.Id);
            var da = query.FirstOrDefault();
            return da;
        }

        #endregion
        public async Task<BaseResponse<StockDto>> Create(StockRequest request)
        {
            var result = new BaseResponse<StockDto>();
            try
            {
                //thêm hóa đơn xuất
                Stock stocks = new Stock
                {
                    IdStaff = request.IdStaff,
                    Date = DateTime.Now,
                    Note = request.Note
                };

                _dataContext.Stocks.Add(stocks);
                await _dataContext.SaveChangesAsync();

                var LatestStock = GetLatestStock();
                //thêm chi tiết phiếu kiểm kho
                StockInfo stockinfos = new StockInfo();
                foreach (var item in request.List)
                {
                    stockinfos.Stock_Id = LatestStock.Id;
                    stockinfos.Medicine_Id = item.medicineId;
                    stockinfos.ActualAmount = item.ActualAmount;

                    _dataContext.StockInfos.Add(stockinfos);
                    await _dataContext.SaveChangesAsync();

                    //cập nhật số lượng thực vào kho
                    var inventories = _inventoryService.GetListInventory();
                    foreach (var i in inventories)
                    {
                        if (i.IdMedicine == item.medicineId && i.LotNumber == item.LotNumber)
                        {
                            i.Count = item.ActualAmount;
                            await _dataContext.SaveChangesAsync();
                        }
                    }

                    //thêm thẻ kho
                    var LatestStockInfo = _stockinfoService.GetLatestStockInfo();
                    InventoryTags inventoryTags = new InventoryTags();
                    inventoryTags.DocumentId = "PKT00" + LatestStockInfo.Id;
                    inventoryTags.DocumentDate = DateTime.Now;
                    inventoryTags.DocumentType = 5;
                    inventoryTags.MedicineId = item.medicineId;
                    inventoryTags.LotNumber = item.LotNumber;
                    inventoryTags.Qty = item.ActualAmount;
                    _dataContext.InventoryTags.Add(inventoryTags);
                    await _dataContext.SaveChangesAsync();

                }
                result.Success = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
