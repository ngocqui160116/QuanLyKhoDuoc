using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Stock;
using Phoenix.Shared.StockInfo;
using Phoenix.Shared.Vendor;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IStockService
    {
        //Mobile
        Task<BaseResponse<StockDto>> GetAllStock(StockRequest request);
        Task<BaseResponse<StockDto>> CreateStock(StockRequest request);
        Task<CrudResult> UpdateStock(string Id, StockRequest request);
        Task<CrudResult> DeleteStock(string Id);
        List<StockDto> Search(string Id);
        
        //Web
        Task<BaseResponse<StockDto>> GetAll(StockRequest request);
        Task<BaseResponse<StockDto>> Create(StockRequest request);
        Stock GetStockById(string id);
        //Task<BaseResponse<Stock>> Delete(string Id);
    }
    public class StockService : IStockService
    {
        
        private readonly DataContext _dataContext;
        public StockService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //Mobile
        #region Mobile

        #region GetAllStock
        public async Task<BaseResponse<StockDto>> GetAllStock(StockRequest request)
        {

            

            //setup query
            var result = new BaseResponse<StockDto>();
            try
            {
                // setup query
                var query = _dataContext.Stocks.AsQueryable();

                //if (!string.IsNullOrEmpty(request.Id))
                //{
                //    query = query.Where(d => d.Id.Contains(request.Id));
                //}


                query = query.OrderByDescending(d => d.Id);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<StockDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #region CreateStock
        public async Task<BaseResponse<StockDto>> CreateStock(StockRequest request)
        {
            var result = new BaseResponse<StockDto>();
            var medicineItems = _dataContext.MedicineItems.ToList();
            try
            {
                Stock Stocks = new Stock
                {
                    IdStaff = request.IdStaff,
                    IdSupplier = request.IdSupplier,
                    DateStock = request.DateStock,
                    Status = "Đã lưu"
                };

                _dataContext.Stocks.Add(Stocks);
                await _dataContext.SaveChangesAsync();

                var Latest = GetLatestStock();

                StockInfo Stockinfos = new StockInfo();
                foreach (var item in medicineItems)
                {
                    Stockinfos.IdStock = Latest.Id;
                    Stockinfos.IdMedicine = item.Medicine_Id;
                    Stockinfos.IdBatch = (int)item.Batch;
                    Stockinfos.Count = item.Count;
                    Stockinfos.StockPrice = item.StockPrice;
                    Stockinfos.Total = item.Count * item.StockPrice;
                    Stockinfos.DueDate = item.DueDate;

                    _dataContext.StockInfos.Add(Stockinfos);
                    await _dataContext.SaveChangesAsync();
                }
                result.Success = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #region UpdateStock
        public async Task<CrudResult> UpdateStock(string Id, StockRequest request)
        {
            var Stock = _dataContext.Stocks.Find(Id);
            Stock.IdStaff = request.IdStaff;
            Stock.DateStock = request.DateStock;

            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
        #endregion

        #region DeleteStock
        public async Task<CrudResult> DeleteStock(string Id)
        {
            var Stock = _dataContext.Stocks.Find(Id);
            if (Stock == null)
                return new CrudResult()
                {
                    ErrorCode = CommonErrorStatus.KeyNotFound,
                    ErrorDescription = "Xoá không thành công."
                };
            _dataContext.Stocks.Remove(Stock);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
        #endregion

        #region Search
        public List<StockDto> Search(string Id)
        {
            // setup query
            var query = _dataContext.Stocks.Where(x => x.Id.Equals(Id));
            var data = query.ToList();
            return data.MapTo<StockDto>();
        }
        #endregion

        #endregion

        //Web 
        #region Web

        #region GetAll
        public async Task<BaseResponse<StockDto>> GetAll(StockRequest request)
        {
            //setup query
            var result = new BaseResponse<StockDto>();
            try
            {
                // setup query
                var query = _dataContext.Stocks.AsQueryable();

                //if (!string.IsNullOrEmpty(request.Id))
                //{
                //    query = query.Where(d => d.Id.Contains(request.Id));
                //}


                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<StockDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #region Create
        //thêm hóa đơn nhập và chi tiết hóa đơn nhập
        public async Task<BaseResponse<StockDto>> Create(StockRequest request)
        {
            var result = new BaseResponse<StockDto>();
            var medicineItems = _dataContext.MedicineItems.ToList();
            try
            {
               
                Stock Stocks = new Stock
                {
                    IdStaff = request.IdStaff,
                    IdSupplier = request.IdSupplier,
                    DateStock = request.DateStock,
                    Status = "Đã lưu"

                };
                
                _dataContext.Stocks.Add(Stocks);
                await _dataContext.SaveChangesAsync();

                var Latest = GetLatestStock();

               

                StockInfo Stockinfos = new StockInfo();
                foreach (var item in request.List)
                {
                    Stockinfos.IdStock = Latest.Id;
                    Stockinfos.IdMedicine = item.medicineId;
                    Stockinfos.IdBatch = item.Batch;
                    Stockinfos.Count = item.Count;
                    Stockinfos.StockPrice = item.StockPrice;
                    Stockinfos.Total = item.Count * item.StockPrice;
                    Stockinfos.DueDate = item.DueDate;

                    _dataContext.StockInfos.Add(Stockinfos);
                    await _dataContext.SaveChangesAsync();
                }
                result.Success = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #region GetStockById
        public Stock GetStockById(string id) => _dataContext.Stocks.Find(id);

        /*public async Task<BaseResponse<Stock>> Delete(string Id)
        {
            var result = new BaseResponse<StockDto>();
            try
            {
                var Stock = GetStockById(Id);

                //Stock.Status = True;
                
                await _dataContext.SaveChangesAsync();

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }*/
        #endregion

        #endregion
        //Chung
        #region GetLatestStock
        public Stock GetLatestStock()
        {
            var query = _dataContext.Stocks.AsQueryable();

            query = query.OrderByDescending(d => d.Id);
            var da = query.FirstOrDefault();
            return da;
        }

        #endregion
    }
}
