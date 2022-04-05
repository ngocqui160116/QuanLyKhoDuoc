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
        Task<BaseResponse<InputDto>> GetAllInput(InputRequest request);
        List<InputDto> Search(string Id);
        Task<CrudResult> CreateInput(InputRequest request);
        Task<CrudResult> UpdateInput(string Id, InputRequest request);
        Task<CrudResult> DeleteInput(string Id);

        //
        Task<BaseResponse<InputDto>> GetAll(InputRequest request);
        Task<BaseResponse<InputDto>> Create(InputRequest request);
        Input GetInputById(string id);
        //Task<BaseResponse<Input>> Delete(string Id);
    }
    public class InputService : IInputService
    {
        
        private readonly DataContext _dataContext;
        public InputService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
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

        public List<InputDto> Search(string Id)
        {
                // setup query
                var query = _dataContext.Inputs.Where(x => x.Id.Equals(Id));

                var data =  query.ToList();
                return data.MapTo<InputDto>();
        }
        //int ID;
        //public int PhieuNhap()
        //{
           
        //    var input = _dataContext.Inputs.ToList().LastOrDefault();


        //    if (input.Id == null)
        //    {
        //        ID = 1;
        //    }
        //    else
        //    {
        //        //ID = input.IdInput;
        //        ID = Convert.ToInt32(input.Id += 1);
        //    }
        //    return ID;
        //}

      

        public async Task<CrudResult> CreateInput(InputRequest request)
        {
            //PhieuNhap();


            Input inputs = new Input
            {
                IdStaff = request.IdStaff,
                IdSupplier = request.IdSupplier,
                DateInput = request.DateInput
            };

            _dataContext.Inputs.Add(inputs);
            await _dataContext.SaveChangesAsync();

            return new CrudResult() { IsOk = true };
        }

        //Task<CrudResult> UpdateInput(int IdInput, InputRequest request);
        //Task<CrudResult> DeleteInput(int IdInput);
        public async Task<CrudResult> UpdateInput(string Id, InputRequest request)
        {
            var Input = _dataContext.Inputs.Find(Id);
            Input.IdStaff = request.IdStaff;
            Input.DateInput = request.DateInput;

            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

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

        // 
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
        public async Task<BaseResponse<InputDto>> Create(InputRequest request)
        {
            var result = new BaseResponse<InputDto>();
            try
            {
                Input inputs = new Input
                {
                    IdStaff = request.IdStaff,
                    IdSupplier = request.IdSupplier,
                    DateInput = request.DateInput,
                    Status = request.Status

                };
                _dataContext.Inputs.Add(inputs);
                await _dataContext.SaveChangesAsync();

                /*InputInfo list = new InputInfo();
                //list = 
                _dataContext.InputInfos.AddRange(List);
                await _dataContext.SaveChangesAsync();*/

                result.Success = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public Input GetInputById(string id) => _dataContext.Inputs.Find(id);
        
        /*public async Task<BaseResponse<Input>> Delete(string Id)
        {
            var result = new BaseResponse<InputDto>();
            try
            {
                var input = GetInputById(Id);

                //input.Status = True;
                
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
    }
}
