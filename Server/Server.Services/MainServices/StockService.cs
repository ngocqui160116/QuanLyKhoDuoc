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
    public interface IStockService
    {
        Task<BaseResponse<StockDto>> GetAllStock(StockRequest request);
        /// 
        Task<BaseResponse<StockDtoWeb>> GetAll(StockRequest request);
        Task<BaseResponse<StockDto>> Create(StockRequest request);
        Task<BaseResponse<StockInfoDto>> GetAllStockInfoById(int Id, StockRequest request);
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
        public async Task<BaseResponse<StockDtoWeb>> GetAll(StockRequest request)
        {
            var result = new BaseResponse<StockDtoWeb>();

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
                result.Data = data.MapTo<StockDtoWeb>();

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
                //thêm phiếu kiểm kho
                Stock stocks = new Stock
                {
                    IdStaff = request.IdStaff,
                    Date = DateTime.Now,
                    Note = request.Note
                };

                _dataContext.Stocks.Add(stocks);
                await _dataContext.SaveChangesAsync();

                var LatestStock = GetLatestStock();

                foreach (var item in request.List)
                {
                    //thêm thẻ kho
                    var LatestStockInfo = _stockinfoService.GetLatestStockInfo();
                    InventoryTags inventoryTags = new InventoryTags();
                    inventoryTags.DocumentId = "PKT00" + LatestStockInfo.Id;
                    inventoryTags.DocumentDate = DateTime.Now;
                    inventoryTags.DocumentType = 5;
                    inventoryTags.MedicineId = item.medicineId;
                    inventoryTags.LotNumber = item.LotNumber;
                    inventoryTags.Qty = item.Count;
                    _dataContext.InventoryTags.Add(inventoryTags);
                    await _dataContext.SaveChangesAsync();

                    //cập nhật số lượng thực vào kho
                    var inventories = _inventoryService.GetListInventory();
                    foreach (var i in inventories)
                    {
                        if (i.IdMedicine == item.medicineId && i.LotNumber == item.LotNumber)
                        {
                            i.Count = item.Count;
                            await _dataContext.SaveChangesAsync();

                            //thêm chi tiết phiếu kiểm kho
                            StockInfo stockinfos = new StockInfo();
                            stockinfos.Stock_Id = LatestStock.Id;
                            stockinfos.Medicine_Id = item.medicineId;
                            stockinfos.ActualAmount = item.Count;
                            stockinfos.Inventory_Id = i.Id;
                            _dataContext.StockInfos.Add(stockinfos);
                            await _dataContext.SaveChangesAsync();
                        }
                    }
                }
                result.Success = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public async Task<BaseResponse<StockInfoDto>> GetAllStockInfoById(int Id,StockRequest request)
        {
            var result = new BaseResponse<StockInfoDto>();
            try
            {
                //var query = _dataContext.StockInfos.AsQueryable();

                //query = query.OrderByDescending(d => d.Id);
                //var get = GetInputInfoById(Id);
                var list = _dataContext.StockInfos.Where(p => p.Stock_Id.Equals(Id));

                var data = await list.ToListAsync();
                result.Data = data.MapTo<StockInfoDto>();
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
