using Falcon.Core;
using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Input;
using Phoenix.Shared.InputInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IInputInfoService
    {
        Task<BaseResponse<InputInfoDto>> GetAllInputInfo(InputInfoRequest request);
        //Task<BaseResponse<InputInfoDto>> GetInputInfoById(InputInfoRequest request);
        Task<CrudResult> CreateInputInfo(InputInfoRequest request);
        Task<CrudResult> UpdateInputInfo(int Id, InputInfoRequest request);
        Task<CrudResult> DeleteInputInfo(int Id);
        Task<CrudResult> CreateInventory(InputInfoRequest request);

        //
        Task<BaseResponse<InputInfoDto>> GetAll(InputInfoRequest request);
        InputInfo GetInputInfoById(int Id);
        Task<BaseResponse<InputInfoDto>> Create(InputInfoRequest request);
        Task<BaseResponse<InputInfoDto>> GetAllInputInfoById(int Id,InputInfoRequest request);
        Task<BaseResponse<InputInfoDto>> GetExpiredMedicine(InputInfoRequest request);
        Task<BaseResponse<InputInfoDto>> Complete(int Id, InputInfoRequest request);
    }
    public class InputInfoService : IInputInfoService
    {
        private readonly DataContext _dataContext;
        public InputInfoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<InputInfoDto>> GetAllInputInfo(InputInfoRequest request)
        {
            var result = new BaseResponse<InputInfoDto>(); 
            try
            {
                // setup query
                var query = _dataContext.InputInfos.AsQueryable();
                // filter
                //if (!string.IsNullOrEmpty(request.IdInput))
                //{
                //    query = query.Where(d => d.IdInput.Contains(request.IdInput));
                //}
                
                query = query.OrderByDescending(d => d.Id);
                query = query.OrderByDescending(d => d.IdBatch);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<InputInfoDto>();
            }
            catch (Exception ex)
            {

            }
            return result;
        }


        // Task<CrudResult> CreateInputInfo(InputInfoRequest request);
        public async Task<CrudResult> CreateInputInfo(InputInfoRequest request)
        {

            var Input = new Input();

            Input.IdStaff = request.IdStaff;
            Input.IdSupplier = request.IdSupplier;
            Input.DateInput = request.DateInput;
            Input.Status = request.Status;

            _dataContext.Inputs.Add(Input);
            await _dataContext.SaveChangesAsync();

            var InputInfo = new InputInfo();
            InputInfo.IdInput = Input.Id;
            InputInfo.IdMedicine = request.IdMedicine;
            InputInfo.IdBatch = request.IdBatch;
            InputInfo.Count = request.Count;
            InputInfo.InputPrice = request.InputPrice;
            InputInfo.Total = request.Count * request.InputPrice;
            InputInfo.DueDate = request.DueDate;
            

            _dataContext.InputInfos.Add(InputInfo);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> CreateInventory(InputInfoRequest request)
        {

            var Input = new Input();

            Input.IdStaff = request.IdStaff;
            Input.IdSupplier = request.IdSupplier;
            Input.DateInput = request.DateInput;
            Input.Status = request.Status;

            _dataContext.Inputs.Add(Input);
            await _dataContext.SaveChangesAsync();

            var InputInfo = new InputInfo();
            InputInfo.IdInput = Input.Id;
            InputInfo.IdMedicine = request.IdMedicine;
            InputInfo.IdBatch = request.IdBatch;
            InputInfo.Count = request.Count;
            InputInfo.InputPrice = request.InputPrice;
            InputInfo.Total = request.Count * request.InputPrice;
            InputInfo.DueDate = request.DueDate;

            _dataContext.InputInfos.Add(InputInfo);
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
                inventory.IdInputInfo = InputInfo.Id;

                await _dataContext.SaveChangesAsync();


                var InventoryTags = new InventoryTags();
                InventoryTags.DocumentId = "PN00" + InputInfo.Id;
                InventoryTags.ExpiredDate = DateTime.Now;
                InventoryTags.DocumentDate = DateTime.Now;
                InventoryTags.LotNumber = request.IdBatch;
                InventoryTags.UnitPrice = InputInfo.InputPrice;
                InventoryTags.TotalPrice = InputInfo.Total;
                // InventoryTags.SupplierId = Input.IdSupplier;
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
                inventori.IdInputInfo = InputInfo.Id;

                _dataContext.Inventories.Add(inventori);
                await _dataContext.SaveChangesAsync();


                var InventoryTags = new InventoryTags();
                InventoryTags.DocumentId = "PN00" + InputInfo.Id;
                InventoryTags.ExpiredDate = DateTime.Now;
                InventoryTags.DocumentDate = DateTime.Now;
                InventoryTags.LotNumber = request.IdBatch;
                InventoryTags.UnitPrice = InputInfo.InputPrice;
                InventoryTags.TotalPrice = InputInfo.Total;
                // InventoryTags.SupplierId = Input.IdSupplier;
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


        public async Task<CrudResult> UpdateInputInfo(int Id, InputInfoRequest request)
        {
            var InputInfo = _dataContext.InputInfos.Find(Id);
            InputInfo.IdMedicine = request.IdMedicine;
           // InputInfo.IdSupplier = request.IdSupplier;
            InputInfo.IdBatch = request.IdBatch;
            InputInfo.DueDate = request.DueDate;

            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> DeleteInputInfo(int Id)
        {
            var InputInfo = _dataContext.InputInfos.Find(Id);
            if (InputInfo == null)
                return new CrudResult()
                {
                    ErrorCode = CommonErrorStatus.KeyNotFound,
                    ErrorDescription = "Xoá không thành công."
                };
            _dataContext.InputInfos.Remove(InputInfo);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        /////////////
        public async Task<BaseResponse<InputInfoDto>> GetAll(InputInfoRequest request)
        {
            var result = new BaseResponse<InputInfoDto>();
            try
            {
                // setup query
                var query = _dataContext.InputInfos.AsQueryable();
                // filter
                //if (!string.IsNullOrEmpty(request.IdInput))
                //{
                //    query = query.Where(d => d.IdInput.Contains(request.IdInput));
                //}

                query = query.OrderByDescending(d => d.Id);
                query = query.OrderByDescending(d => d.IdBatch);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<InputInfoDto>();
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public async Task<BaseResponse<InputInfoDto>> GetAllInputInfoById(int Id, InputInfoRequest request)
        {
            var result = new BaseResponse<InputInfoDto>();
            try
            {
                var query = _dataContext.InputInfos.AsQueryable();
                
                query = query.OrderByDescending(d => d.Id);
                query = query.OrderByDescending(d => d.IdBatch);
                //var get = GetInputInfoById(Id);
                var list = _dataContext.InputInfos.Where(p => p.IdInput.Equals(Id));
            
                var data = await list.ToListAsync();
                result.Data = data.MapTo<InputInfoDto>();
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public InputInfo GetInputInfoById(int Id) => _dataContext.InputInfos.Find(Id);        
        
        public async Task<BaseResponse<InputInfoDto>> Create(InputInfoRequest request)
        {
            var result = new BaseResponse<InputInfoDto>();
            try
            {

                Input inputs = new Input
                {
                    Id = request.IdInput,

                };
                _dataContext.Inputs.Add(inputs);
                await _dataContext.SaveChangesAsync();

                InputInfo inputinfos = new InputInfo
                {
                    //IdInput = request.IdInput,
                    IdMedicine = request.IdMedicine,
                    IdBatch =request.IdBatch,
                    Count = request.Count,
                    InputPrice = request.InputPrice,
                    Total = request.Total,
                    DueDate = request.DueDate
                };
                _dataContext.InputInfos.Add(inputinfos);
                await _dataContext.SaveChangesAsync();

               
                result.Success = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public async Task<BaseResponse<InputInfoDto>> GetExpiredMedicine(InputInfoRequest request)
        {
            var result = new BaseResponse<InputInfoDto>();
            try
            {
                var query = _dataContext.InputInfos.AsQueryable();
                /*if(){

                }*/
                query = query.OrderByDescending(d => d.Id);
                query = query.OrderByDescending(d => d.IdBatch);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<InputInfoDto>();
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public async Task<BaseResponse<InputInfoDto>> Complete(int Id, InputInfoRequest request)
        {
            var result = new BaseResponse<InputInfoDto>();
            try
            {
                var query = _dataContext.InputInfos.AsQueryable();

                query = query.OrderByDescending(d => d.Id);
                query = query.OrderByDescending(d => d.IdBatch);
                //lấy danh sách các chi tiết hóa đơn nhập
                var list = _dataContext.InputInfos.Where(p => p.IdInput.Equals(Id));
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
                        inventory.IdInputInfo = item.Id;
                        await _dataContext.SaveChangesAsync();

                        //thêm chi tiết hóa đơn nhập vào thẻ kho
                        InventoryTags inventoryTags = new InventoryTags();
                        inventoryTags.DocumentId = "PN00" + item.Id;
                        inventoryTags.DocumentDate = DateTime.Now;
                        inventoryTags.DocumentType = 1;
                        inventoryTags.MedicineId = item.IdMedicine;
                        inventoryTags.LotNumber = item.IdBatch;
                        inventoryTags.ExpiredDate = (DateTime)item.DueDate;
                        inventoryTags.Qty_Before = item.Count;
                        inventoryTags.Qty = 0;
                        inventoryTags.Qty_After = item.Count + inventory.Count;

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
                        inventory2.IdInputInfo = item.Id;

                        _dataContext.Inventories.Add(inventory2);
                        await _dataContext.SaveChangesAsync();

                        //thêm chi tiết hóa đơn nhập vào thẻ kho
                        InventoryTags inventoryTags = new InventoryTags();
                        inventoryTags.DocumentId = "PN00" + item.Id;
                        inventoryTags.DocumentDate = DateTime.Now;
                        inventoryTags.DocumentType = 1;
                        inventoryTags.MedicineId = item.IdMedicine;
                        inventoryTags.LotNumber = item.IdBatch;
                        inventoryTags.ExpiredDate = (DateTime)item.DueDate;
                        inventoryTags.Qty_Before = item.Count;
                        inventoryTags.Qty = 0;
                        inventoryTags.Qty_After = item.Count;

                        _dataContext.InventoryTags.Add(inventoryTags);
                        await _dataContext.SaveChangesAsync();
                    }
                }

                result.Data = data.MapTo<InputInfoDto>();
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
