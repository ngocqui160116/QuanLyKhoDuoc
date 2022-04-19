using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Input;
using Phoenix.Shared.InputInfo;
using Phoenix.Shared.Vendor;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IInputService
    {
        //Mobile
        Task<BaseResponse<InputDto>> GetAllInput(InputRequest request);
        Task<BaseResponse<InputDto>> CreateInput(InputRequest request);
        Task<CrudResult> UpdateStatus(int Id, InputRequest request);
        Task<CrudResult> DeleteInput(string Id);
        List<InputDto> Search(string Id);
        
        //Web
        Task<BaseResponse<InputDto>> GetAll(InputRequest request);
        Task<BaseResponse<InputDto>> Create(InputRequest request);
        Task<BaseResponse<InputDto>> GetCompleteInput(InputRequest request);
        Task<BaseResponse<InputDto>> GetSaveInput(InputRequest request);
        Task<BaseResponse<InputDto>> GetCancelInput(InputRequest request);
        Task<BaseResponse<InputDto>> Cancel(int Id);
        Input GetInputById(int id);
    }
    public class InputService : IInputService
    {
        
        private readonly DataContext _dataContext;
        public InputService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //Mobile
        #region Mobile

        #region GetAllInput
        public async Task<BaseResponse<InputDto>> GetAllInput(InputRequest request)
        {

         
            //setup query
            var result = new BaseResponse<InputDto>();
            try
            {
                // setup query
                var query = _dataContext.Inputs.AsQueryable();

                //if (!string.IsNullOrEmpty(request.Id))
                //{
                //    query = query.Where(d => d.Id.Contains(request.Id));
                //}


                query = query.OrderByDescending(d => d.Id);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<InputDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #region CreateInput
        public async Task<BaseResponse<InputDto>> CreateInput(InputRequest request)
        {
            var result = new BaseResponse<InputDto>();
            var medicineItems = _dataContext.MedicineItems.ToList();
            try
            {
                Input inputs = new Input
                {
                    IdStaff = request.IdStaff,
                    IdSupplier = request.IdSupplier,
                    DateInput = request.DateInput,
                    Status = "Chờ duyệt"
                };

                _dataContext.Inputs.Add(inputs);
                await _dataContext.SaveChangesAsync();

                var Latest = GetLatestInput();

                InputInfo inputinfos = new InputInfo();
                foreach (var item in medicineItems)
                {
                    inputinfos.IdInput = Latest.Id;
                    inputinfos.IdMedicine = item.Medicine_Id;
                    inputinfos.IdBatch = (int)item.Batch;
                    inputinfos.Count = item.Count;
                    inputinfos.InputPrice = item.InputPrice;
                    inputinfos.Total = item.Count * item.InputPrice;
                    inputinfos.DueDate = item.DueDate;

                    _dataContext.InputInfos.Add(inputinfos);
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

        #region UpdateStatus
        public async Task<CrudResult> UpdateStatus(int Id, InputRequest request)
        {
            var Input = _dataContext.Inputs.Find(Id);
            Input.Status = "Đã hủy";

            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
        #endregion

        #region DeleteInput
        public async Task<CrudResult> DeleteInput(string Id)
        {
            var Input = _dataContext.Inputs.Find(Id);
            if (Input == null)
                return new CrudResult()
                {
                    ErrorCode = CommonErrorStatus.KeyNotFound,
                    ErrorDescription = "Xoá không thành công."
                };
            _dataContext.Inputs.Remove(Input);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
        #endregion

        #region Search
        public List<InputDto> Search(string Id)
        {
            // setup query
            var data = _dataContext.Inputs.Where(x =>x.Id.ToString().Contains(Id)).ToList();

             return data.MapTo<InputDto>();
        }
        #endregion

        #endregion

        //Web 
        #region Web


        #region GetAll
        public async Task<BaseResponse<InputDto>> GetAll(InputRequest request)
        {
            //setup query
            var result = new BaseResponse<InputDto>();
            try
            {
                // setup query
                var query = _dataContext.Inputs.AsQueryable();

                //if (!string.IsNullOrEmpty(request.Id))
                //{
                //    query = query.Where(d => d.Id.Contains(request.Id));
                //}


                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<InputDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #region Create
        //thêm hóa đơn nhập và chi tiết hóa đơn nhập
        public async Task<BaseResponse<InputDto>> Create(InputRequest request)
        {
            var result = new BaseResponse<InputDto>();
            var medicineItems = _dataContext.MedicineItems.ToList();
            try
            {
               
                Input inputs = new Input
                {
                    IdStaff = request.IdStaff,
                    IdSupplier = request.IdSupplier,
                    DateInput = request.DateInput,
                    Status = "Chờ duyệt"

                };
                
                _dataContext.Inputs.Add(inputs);
                await _dataContext.SaveChangesAsync();

                var Latest = GetLatestInput();

               

                InputInfo inputinfos = new InputInfo();
                foreach (var item in request.List)
                {
                    inputinfos.IdInput = Latest.Id;
                    inputinfos.IdMedicine = item.medicineId;
                    inputinfos.IdBatch = item.Batch;
                    inputinfos.Count = item.Count;
                    inputinfos.InputPrice = item.InputPrice;
                    inputinfos.Total = item.Count * item.InputPrice;
                    inputinfos.DueDate = item.DueDate;

                    _dataContext.InputInfos.Add(inputinfos);
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

        #region Cancel
        //thêm hóa đơn nhập và chi tiết hóa đơn nhập
        public async Task<BaseResponse<InputDto>> Cancel(int Id)
        {
            var result = new BaseResponse<InputDto>();
            try
            {
                var input = GetInputById(Id);
                input.Status = "Đã hủy";
                await _dataContext.SaveChangesAsync();

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
        #endregion

        #region GetInputById
        public Input GetInputById(int id) => _dataContext.Inputs.Find(id);

        #endregion

        #region GetCompleteInput
        public async Task<BaseResponse<InputDto>> GetCompleteInput(InputRequest request)
        {
            //setup query
            var result = new BaseResponse<InputDto>();
            try
            {
                // setup query
                var query = _dataContext.Inputs.AsQueryable();

                if (request.Status == "Đã hoàn thành")
                {
                    query = query.Where(d => d.Status.Equals(request.Status));
                }
                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<InputDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #region GetSaveInput
        public async Task<BaseResponse<InputDto>> GetSaveInput(InputRequest request)
        {
            //setup query
            var result = new BaseResponse<InputDto>();
            try
            {
                // setup query
                var query = _dataContext.Inputs.AsQueryable();

                if (request.Status == "Chờ duyệt")
                {
                    query = query.Where(d => d.Status.Equals(request.Status));
                }
                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<InputDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #region GetCancelInput
        public async Task<BaseResponse<InputDto>> GetCancelInput(InputRequest request)
        {
            //setup query
            var result = new BaseResponse<InputDto>();
            try
            {
                // setup query
                var query = _dataContext.Inputs.AsQueryable();

                if (request.Status == "Đã hủy")
                {
                    query = query.Where(d => d.Status.Equals(request.Status));
                }
                query = query.OrderByDescending(d => d.Id);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<InputDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion

        #endregion
        //Chung
        #region GetLatestInput
        public Input GetLatestInput()
        {
            var query = _dataContext.Inputs.AsQueryable();

            query = query.OrderByDescending(d => d.Id);
            var da = query.FirstOrDefault();
            return da;
        }

        #endregion
    }
}
