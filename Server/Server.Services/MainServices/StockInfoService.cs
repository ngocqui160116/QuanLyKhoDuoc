using Falcon.Core;
using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Input;
using Phoenix.Shared.StockInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IStockInfoService
    {
        //Mobile
        Task<BaseResponse<StockInfoDto>> GetAllStockInfo(StockInfoRequest request);
        Task<CrudResult> CreateStockInfo(StockInfoRequest request);
        Task<CrudResult> UpdateStockInfo(int Id, StockInfoRequest request);
        Task<CrudResult> DeleteStockInfo(int Id);
        Task<CrudResult> CreateInventory(StockInfoRequest request);

        //Web
        Task<BaseResponse<StockInfoDto>> GetAll(StockInfoRequest request);
        StockInfo GetStockInfoById(int Id);
        Task<BaseResponse<StockInfoDto>> Create(StockInfoRequest request);
        Task<BaseResponse<StockInfoDto>> GetAllStockInfoById(int Id,StockInfoRequest request);
        Task<BaseResponse<StockInfoDto>> GetExpiredMedicine(StockInfoRequest request);
        Task<BaseResponse<StockInfoDto>> Complete(int Id, StockInfoRequest request);
    }
    public class StockInfoService : IStockInfoService
    {
        private readonly DataContext _dataContext;
        public StockInfoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        ////////Mobile

        #region GetAllStockInfo
        public async Task<BaseResponse<StockInfoDto>> GetAllStockInfo(StockInfoRequest request)
        {
            var result = new BaseResponse<StockInfoDto>(); 
            try
            {
                // setup query
                var query = _dataContext.StockInfos.AsQueryable();
                
                query = query.OrderByDescending(d => d.Id);
                query = query.OrderByDescending(d => d.IdBatch);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<StockInfoDto>();
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion

        #region CreateStockInfo
        public async Task<CrudResult> CreateStockInfo(StockInfoRequest request)
        {
            var StockInfo = new StockInfo();
            StockInfo.IdInput = request.Id;
            StockInfo.IdMedicine = request.IdMedicine;
            StockInfo.IdBatch = request.IdBatch;
            StockInfo.Count = request.Count;
            StockInfo.InputPrice = request.InputPrice;
            StockInfo.Total = request.Count * request.InputPrice;
            StockInfo.DueDate = request.DueDate;
            
            _dataContext.StockInfos.Add(StockInfo);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
        #endregion

        #region CreateInventory
        public async Task<CrudResult> CreateInventory(StockInfoRequest request)
        {

            var Input = new Input();

            Input.IdStaff = request.IdStaff;
            Input.IdSupplier = request.IdSupplier;
            Input.DateInput = request.DateInput;
            Input.Status = request.Status;

            _dataContext.Inputs.Add(Input);
            await _dataContext.SaveChangesAsync();

            var StockInfo = new StockInfo();
            StockInfo.IdInput = Input.Id;
            StockInfo.IdMedicine = request.IdMedicine;
            StockInfo.IdBatch = request.IdBatch;
            StockInfo.Count = request.Count;
            StockInfo.InputPrice = request.InputPrice;
            StockInfo.Total = request.Count * request.InputPrice;
            StockInfo.DueDate = request.DueDate;

            _dataContext.StockInfos.Add(StockInfo);
            await _dataContext.SaveChangesAsync();

            var inventories = _dataContext.Inventories.ToList()
                               .FindAll(d => d.IdMedicine == request.IdMedicine && d.LotNumber == request.IdBatch);
            if(inventories.Count != 0)
            {
                var inventory = inventories.FirstOrDefault();

                inventory.IdMedicine = request.IdMedicine;
                if (inventory.Count == null)
                {
                    inventory.Count = 0 + request.Count;
                }
                else
                {
                    inventory.Count = inventory.Count + request.Count;
                }

                inventory.LotNumber = request.IdBatch;
                inventory.IdStockInfo = StockInfo.Id;

                await _dataContext.SaveChangesAsync();

                var InventoryTags = new InventoryTags();
                InventoryTags.DocumentId = "PN00" + StockInfo.Id;
                InventoryTags.ExpiredDate = DateTime.Now;
                InventoryTags.DocumentDate = DateTime.Now;
                InventoryTags.LotNumber = request.IdBatch;
                InventoryTags.UnitPrice = StockInfo.InputPrice;
                InventoryTags.TotalPrice = StockInfo.Total;
                InventoryTags.DocumentType = 1;
                InventoryTags.MedicineId = request.IdMedicine;
                InventoryTags.Qty_After = inventory.Count;
                InventoryTags.Qty = 0;
                InventoryTags.Qty_Before = request.Count;

                _dataContext.InventoryTags.Add(InventoryTags);
                await _dataContext.SaveChangesAsync();
            }    
            else
            {
                var inventori = new Inventory();
                inventori.IdMedicine = request.IdMedicine;
                if (inventori.Count == null)
                {
                    inventori.Count = 0 + request.Count;
                }
                else
                {
                    inventori.Count = inventori.Count + request.Count;
                }

                inventori.LotNumber = request.IdBatch;
                inventori.IdStockInfo = StockInfo.Id;

                _dataContext.Inventories.Add(inventori);
                await _dataContext.SaveChangesAsync();

                var InventoryTags = new InventoryTags();
                InventoryTags.DocumentId = "PN00" + StockInfo.Id;
                InventoryTags.ExpiredDate = DateTime.Now;
                InventoryTags.DocumentDate = DateTime.Now;
                InventoryTags.LotNumber = request.IdBatch;
                InventoryTags.UnitPrice = StockInfo.InputPrice;
                InventoryTags.TotalPrice = StockInfo.Total;
                InventoryTags.DocumentType = 1;
                InventoryTags.MedicineId = request.IdMedicine;
                InventoryTags.Qty_After = inventori.Count;
                InventoryTags.Qty = 0;
                InventoryTags.Qty_Before = request.Count;

                _dataContext.InventoryTags.Add(InventoryTags);
                await _dataContext.SaveChangesAsync();
            }
            return new CrudResult() { IsOk = true };
        }
        #endregion

        #region UpdateStockInfo
        public async Task<CrudResult> UpdateStockInfo(int Id, StockInfoRequest request)
        {
            var StockInfo = _dataContext.StockInfos.Find(Id);
            StockInfo.IdMedicine = request.IdMedicine;
            StockInfo.IdBatch = request.IdBatch;
            StockInfo.DueDate = request.DueDate;

            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
        #endregion

        #region DeleteStockInfo
        public async Task<CrudResult> DeleteStockInfo(int Id)
        {
            var StockInfo = _dataContext.StockInfos.Find(Id);
            if (StockInfo == null)
                return new CrudResult()
                {
                    ErrorCode = CommonErrorStatus.KeyNotFound,
                    ErrorDescription = "Xoá không thành công."
                };
            _dataContext.StockInfos.Remove(StockInfo);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
        #endregion

        /////////////WEB

        #region GetAll
        public async Task<BaseResponse<StockInfoDto>> GetAll(StockInfoRequest request)
        {
            var result = new BaseResponse<StockInfoDto>();
            try
            {
                // setup query
                var query = _dataContext.StockInfos.AsQueryable();
                // filter
                //if (!string.IsNullOrEmpty(request.IdInput))
                //{
                //    query = query.Where(d => d.IdInput.Contains(request.IdInput));
                //}

                query = query.OrderByDescending(d => d.Id);
                query = query.OrderByDescending(d => d.IdBatch);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<StockInfoDto>();
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion
        public async Task<BaseResponse<StockInfoDto>> GetAllStockInfoById(int Id, StockInfoRequest request)
        {
            var result = new BaseResponse<StockInfoDto>();
            try
            {
                var query = _dataContext.StockInfos.AsQueryable();
                
                query = query.OrderByDescending(d => d.Id);
                query = query.OrderByDescending(d => d.IdBatch);
                //var get = GetStockInfoById(Id);
                var list = _dataContext.StockInfos.Where(p => p.IdInput.Equals(Id));
            
                var data = await list.ToListAsync();
                result.Data = data.MapTo<StockInfoDto>();
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public StockInfo GetStockInfoById(int Id) => _dataContext.StockInfos.Find(Id);        
        
        public async Task<BaseResponse<StockInfoDto>> Create(StockInfoRequest request)
        {
            var result = new BaseResponse<StockInfoDto>();
            try
            {

                Input inputs = new Input
                {
                    Id = request.IdInput,

                };
                _dataContext.Inputs.Add(inputs);
                await _dataContext.SaveChangesAsync();

                StockInfo StockInfos = new StockInfo
                {
                    //IdInput = request.IdInput,
                    IdMedicine = request.IdMedicine,
                    IdBatch =request.IdBatch,
                    Count = request.Count,
                    InputPrice = request.InputPrice,
                    Total = request.Total,
                    DueDate = request.DueDate
                };
                _dataContext.StockInfos.Add(StockInfos);
                await _dataContext.SaveChangesAsync();

               
                result.Success = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public async Task<BaseResponse<StockInfoDto>> GetExpiredMedicine(StockInfoRequest request)
        {
            var result = new BaseResponse<StockInfoDto>();
            try
            {
                var query = _dataContext.StockInfos.AsQueryable();
                /*if(){

                }*/
                query = query.OrderByDescending(d => d.Id);
                query = query.OrderByDescending(d => d.IdBatch);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<StockInfoDto>();
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public async Task<BaseResponse<StockInfoDto>> Complete(int Id, StockInfoRequest request)
        {
            var result = new BaseResponse<StockInfoDto>();
            try
            {
                var query = _dataContext.StockInfos.AsQueryable();

                query = query.OrderByDescending(d => d.Id);
                query = query.OrderByDescending(d => d.IdBatch);
                //lấy danh sách các chi tiết hóa đơn nhập
                var list = _dataContext.StockInfos.Where(p => p.IdInput.Equals(Id));
                var data = await list.ToListAsync();
                //thêm chi tiết hóa đơn nhập vào kho
                foreach (var item in data)
                {
                    //Thuốc và lô trong kho trùng với hóa đơn nhập
                    var inventories = _dataContext.Inventories.ToList()
                                        .FindAll(d => d.IdMedicine == item.IdMedicine && d.LotNumber == item.IdBatch);
                    //đã có thuốc trong kho
                    if(inventories.Count != 0)
                    {
                        var inventory = inventories.FirstOrDefault();
                        //cập nhật lại số lượng tồn trong kho
                        if(inventory.Count == null)
                        {
                            inventory.Count = 0 + item.Count;
                        }
                        else
                        {
                            inventory.Count = inventory.Count + item.Count;
                        }
                        inventory.IdStockInfo = item.Id;
                        await _dataContext.SaveChangesAsync();

                        //thêm chi tiết hóa đơn nhập vào thẻ kho
                        InventoryTags inventoryTags = new InventoryTags();
                        inventoryTags.DocumentId = "PN00" + item.Id;
                        inventoryTags.DocumentDate = DateTime.Now;
                        inventoryTags.DocumentType = 1;
                        inventoryTags.MedicineId = item.IdMedicine;
                        inventoryTags.LotNumber = item.IdBatch;
                        inventoryTags.ExpiredDate = item.DueDate;
                        inventoryTags.Qty_Before = item.Count;
                        inventoryTags.Qty = 0;
                        inventoryTags.Qty_After = item.Count + inventory.Count;
                        inventoryTags.UnitPrice = item.InputPrice;
                        inventoryTags.TotalPrice = item.Total;

                        _dataContext.InventoryTags.Add(inventoryTags);
                        await _dataContext.SaveChangesAsync();
                    }
                    //chưa có thuốc trong kho
                    else
                    {
                        var inventory2 = new Inventory();
                        inventory2.IdMedicine = item.IdMedicine;
                        inventory2.Count = item.Count;
                        inventory2.LotNumber = item.IdBatch;
                        inventory2.IdStockInfo = item.Id;

                        _dataContext.Inventories.Add(inventory2);
                        await _dataContext.SaveChangesAsync();

                        //thêm chi tiết hóa đơn nhập vào thẻ kho
                        InventoryTags inventoryTags = new InventoryTags();
                        inventoryTags.DocumentId = "PN00" + item.Id;
                        inventoryTags.DocumentDate = DateTime.Now;
                        inventoryTags.DocumentType = 1;
                        inventoryTags.MedicineId = item.IdMedicine;
                        inventoryTags.LotNumber = item.IdBatch;
                        inventoryTags.ExpiredDate = item.DueDate;
                        inventoryTags.Qty_Before = item.Count;
                        inventoryTags.Qty = 0;
                        inventoryTags.Qty_After = item.Count;
                        inventoryTags.UnitPrice = item.InputPrice;
                        inventoryTags.TotalPrice = item.Total;

                        _dataContext.InventoryTags.Add(inventoryTags);
                        await _dataContext.SaveChangesAsync();
                    }
                }
                
                result.Data = data.MapTo<StockInfoDto>();
                result.Success = true;
                if (result.Success == true)
                {
                    var input = _dataContext.Inputs.Find(Id);
                    input.Status = "Đã hoàn thành";
                    await _dataContext.SaveChangesAsync();
                }
                    
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
