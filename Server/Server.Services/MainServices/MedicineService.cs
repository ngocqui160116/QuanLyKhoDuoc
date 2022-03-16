using Falcon.Core;
using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Medicine;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IMedicineService
    {
        Task<BaseResponse<MedicineDto>> GetAllMedicine(MedicineRequest request);
        Task<BaseResponse<MedicineDto>> CreateMedicine(MedicineRequest request);
    }
    public class MedicineService : IMedicineService
    {
        private readonly DataContext _dataContext;
        public MedicineService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách thuốc
        public async Task<BaseResponse<MedicineDto>> GetAllMedicine(MedicineRequest request)
        {
            var result = new BaseResponse<MedicineDto>();
            try
            {
                //setup query
                var query = _dataContext.Medicines.AsQueryable();
                //filter
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(d => d.Name.Contains(request.Name));
                }
                if (!string.IsNullOrEmpty(request.RegistrationNumber))
                {
                    query = query.Where(d => d.RegistrationNumber.Contains(request.RegistrationNumber));
                }

                //if (!string.IsNullOrEmpty(request.IdGroup.ToString()))
                //{
                //    query = query.Where(d => d.IdGroup.ToString().Contains(request.IdGroup.ToString()));
                //}
                //if (!string.IsNullOrEmpty(request.Unit.ToString()))
                //{
                //    query = query.Where(d => d.Unit.ToString().Contains(request.Unit.ToString()));
                //}
                //if (!string.IsNullOrEmpty(request.DateOfManufacture))
                //{
                //    query = query.Where(d => d.DateOfManufacture.Contains(request.DateOfManufacture.ToString()));
                //}
                //if (!string.IsNullOrEmpty(request.DueDate.ToString()))
                //{
                //    query = query.Where(d => d.DueDate.ToString().Contains(request.DueDate.ToString()));
                //}
                //if (!string.IsNullOrEmpty(request.IdCustomer.ToString()))
                //{
                //    query = query.Where(d => d.IdCustomer.ToString().Contains(request.IdCustomer.ToString()));
                //}
                if (!string.IsNullOrEmpty(request.Status))
                {
                    query = query.Where(d => d.Status.Contains(request.Status));
                }

                query = query.OrderByDescending(d => d.IdMedicine);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<MedicineDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public async Task<BaseResponse<MedicineDto>> CreateMedicine(MedicineRequest request)
        {
            var result = new BaseResponse<MedicineDto>();
            try
            {
                Medicine medicine = new Medicine
                {
                    Name = request.Name,
                    RegistrationNumber = request.RegistrationNumber,
                    IdGroup = request.IdGroup,
                    Active = request.Active,
                    Content = request.Content,
                    Packing = request.Packing,
                    Amount = request.Amount,
                    Image = request.Image,
                    Status = request.Status
                };
                _dataContext.Medicines.Add(medicine);
                await _dataContext.SaveChangesAsync();

                result.success = true;
            }
            catch
            {

            }
            return result;
        }
    }
}
