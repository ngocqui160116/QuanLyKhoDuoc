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
        InputInfo GetInputInfoById(string Id);
        Task<BaseResponse<InputInfoDto>> Create(InputInfoRequest request);
        Task<BaseResponse<InputInfoDto>> GetAllInputInfoById(string Id,InputInfoRequest request);
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
        public async Task<BaseResponse<InputInfoDto>> GetAllInputInfoById(string Id, InputInfoRequest request)
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
                //var i = "HD001";
                query = query.OrderByDescending(d => d.Id);
                query = query.OrderByDescending(d => d.IdBatch);

                var list = _dataContext.InputInfos.Where(p => p.IdInput.Equals(Id));
                var data = await list.ToListAsync();
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

            var inventory = _dataContext.Inventories.Find(request.IdMedicine);

            inventory.IdMedicine = request.IdMedicine;
            inventory.Count = inventory.Count + request.Count;

            await _dataContext.SaveChangesAsync();

            var InventoryTags = new InventoryTags();
            InventoryTags.DocumentId = "PN00" +InputInfo.Id;
            InventoryTags.ExpiredDate = DateTime.Now;
            InventoryTags.DocumentDate = DateTime.Now;
            InventoryTags.LotNumber = request.IdBatch;
            InventoryTags.UnitPrice = InputInfo.InputPrice;
            InventoryTags.TotalPrice = InputInfo.Total;
            InventoryTags.SupplierId = Input.IdSupplier;
            InventoryTags.DocumentType = 1;
            InventoryTags.MedicineId = request.IdMedicine;
            InventoryTags.Qty_After = InputInfo.Count;
            InventoryTags.Qty = 0;
            InventoryTags.Qty_Before = inventory.Count;


            _dataContext.InventoryTags.Add(InventoryTags);
            await _dataContext.SaveChangesAsync();

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
        public InputInfo GetInputInfoById(string Id) => _dataContext.InputInfos.Find(Id);        
        
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
       
    }
}
