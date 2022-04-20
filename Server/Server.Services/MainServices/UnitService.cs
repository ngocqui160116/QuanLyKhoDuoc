using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Unit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IUnitService
    {
        Task<BaseResponse<UnitDto>> GetAllUnit(UnitRequest request);
        Task<CrudResult> CreateUnit(UnitRequest request);
        Task<CrudResult> UpdateUnit(int Id, UnitRequest request);
        Task<CrudResult> DeleteUnit(int IdUnit);
        //
        Task<BaseResponse<UnitDto>> Create(UnitRequest request);
        Unit GetUnitById(int id);
        Task<BaseResponse<UnitDto>> Update(UnitRequest request);
    }
    public class UnitService : IUnitService
    {
        private readonly DataContext _dataContext;
        public UnitService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<UnitDto>> GetAllUnit(UnitRequest request)
        {
            //setup query
            var result = new BaseResponse<UnitDto>();
            try
            {
                // setup query
                var query = _dataContext.Units.AsQueryable();

                // filter
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(d => d.Name.Contains(request.Name));
                }

                query = query.OrderByDescending(d => d.Id);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<UnitDto>();

            }
            catch (Exception ex)
            {
               // result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

         // Task<CrudResult> CreateUnit(UnitRequest request);
        public async Task<CrudResult> CreateUnit(UnitRequest request)
        {
            var Unit = new Unit();
            Unit.Name = request.Name;
            _dataContext.Units.Add(Unit);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
        
        public async Task<CrudResult> UpdateUnit(int Id, UnitRequest request)
        {
            var Unit = _dataContext.Units.Find(Id);
            Unit.Name = request.Name;
          
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> DeleteUnit(int Id)
        {
            var Unit = _dataContext.Staffs.Find(Id);
            if (Unit == null)
                return new CrudResult()
                {
                    ErrorCode = CommonErrorStatus.KeyNotFound,
                    ErrorDescription = "Xoá không thành công."
                };
            _dataContext.Staffs.Remove(Unit);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        //web
        public async Task<BaseResponse<UnitDto>> Create(UnitRequest request)
        {
            var result = new BaseResponse<UnitDto>();
            try
            {
                Unit unit = new Unit
                {
                    Name = request.Name
                };
                _dataContext.Units.Add(unit);
                await _dataContext.SaveChangesAsync();

                result.Success = true;
            }
            catch
            {
                //result.Message = ex.Message;
            }
            return result;
        }
        public Unit GetUnitById(int id) => _dataContext.Units.Find(id);
        public async Task<BaseResponse<UnitDto>> Update(UnitRequest request)
        {
            var result = new BaseResponse<UnitDto>();
            try
            {
                //Lay du lieu cu
                var unit = GetUnitById(request.Id);
                //cap nhat

                unit.Name = request.Name;
                //};
                //_dataContext.Suppliers.Add(supplier);
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
    }
}
