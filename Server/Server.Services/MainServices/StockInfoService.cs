using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.StockInfo;
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
using Phoenix.Shared.Core;

namespace Phoenix.Server.Services.MainServices
{
   
    public interface IStockInfoService
    {

        Task<BaseResponse<StockInfoDto>> GetAllStockInfo(StockInfoRequest request);
        Task<BaseResponse<StockInfoDto>> CreateStockInfo(StockInfoRequest request);
        /// 
        Task<BaseResponse<StockInfoDto>> GetAll(StockInfoRequest request);
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

        #region GetLatestStock
        public Stock GetLatestStock()
        {
            var query = _dataContext.Stocks.AsQueryable();

            query = query.OrderByDescending(d => d.Id);
            var da = query.FirstOrDefault();
            return da;
        }

        #endregion

        #region CreateStock
        public async Task<BaseResponse<StockInfoDto>> CreateStockInfo(StockInfoRequest request)
        {
            var result = new BaseResponse<StockInfoDto>();
            var medicineItems = _dataContext.MedicineItems.ToList();
            try
            {
                Stock Stocks = new Stock
                {
                    IdStaff = request.IdStaff,
                    Date = request.Date,
                    Note = request.Note
                };

                _dataContext.Stocks.Add(Stocks);
                await _dataContext.SaveChangesAsync();

                var Latest = GetLatestStock();

                StockInfo Stockinfos = new StockInfo();
                foreach (var item in medicineItems)
                {
                    Stockinfos.Stock_Id = Latest.Id;
                    Stockinfos.Inventory_Id = (int)item.Inventory_Id;
                    Stockinfos.Medicine_Id = item.Medicine_Id;
                    Stockinfos.Amount = item.Count;
                    Stockinfos.ActualAmount = item.Amount;

                    _dataContext.StockInfos.Add(Stockinfos);
                    await _dataContext.SaveChangesAsync();
                }
                result.Success = true;

                //lấy danh sách các chi tiết hóa đơn nhập
                var list = _dataContext.StockInfos.Where(p => p.Stock_Id.Equals(Stocks.Id));
                var data = await list.ToListAsync();
                //thêm chi tiết hóa đơn nhập vào kho
                foreach (var item in data)
                {
                    //Thuốc và lô trong kho trùng với hóa đơn nhập
                    var inventories = _dataContext.Inventories.ToList()
                                        .FindAll(d => d.IdMedicine == item.Medicine_Id && d.LotNumber == item.Inventory.LotNumber);
                    //đã có thuốc trong kho
                    if (inventories.Count != 0)
                    {
                        var inventory = inventories.FirstOrDefault();
                        //cập nhật lại số lượng tồn trong kho

                        inventory.Count = item.ActualAmount;

                        inventory.IdInputInfo = item.Inventory.IdInputInfo;
                        await _dataContext.SaveChangesAsync();

                        //thêm chi tiết hóa đơn nhập vào thẻ kho
                        InventoryTags inventoryTags = new InventoryTags();
                        inventoryTags.DocumentId = "PK00" + item.Id;
                        inventoryTags.DocumentDate = DateTime.Now;
                        inventoryTags.DocumentType = 2;
                        inventoryTags.MedicineId = item.Medicine_Id;
                        inventoryTags.LotNumber = (int)item.Inventory.LotNumber;
                        inventoryTags.ExpiredDate = DateTime.Now;
                        inventoryTags.Qty_Before = 0;
                        inventoryTags.Qty = item.Amount;
                        inventoryTags.Qty_After = inventory.Count;
                        inventoryTags.UnitPrice = item.Inventory.UnitPrice;
                        inventoryTags.TotalPrice = item.Inventory.Count * item.Inventory.UnitPrice;

                        _dataContext.InventoryTags.Add(inventoryTags);
                        await _dataContext.SaveChangesAsync();
                    }
                    //chưa có thuốc trong kho

                }

                result.Data = data.MapTo<StockInfoDto>();
                result.Success = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

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
       
    }
}
