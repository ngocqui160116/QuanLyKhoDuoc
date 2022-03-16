using Falcon.Core;
using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
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
        Task<CrudResult> CreateMedicine(MedicineRequest request);
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

        // Task<CrudResult> CreateMedicine(MedicineRequest request);
        public async Task<CrudResult> CreateMedicine(MedicineRequest request)
        {
            var Medicine = new Medicine();
            Medicine.RegistrationNumber = request.RegistrationNumber;
            Medicine.Name = request.Name;
            Medicine.IdGroup = request.IdGroup;
            Medicine.IdUnit = request.IdUnit;
            Medicine.Active = request.Active;
            Medicine.Content = request.Content;
            Medicine.Packing = request.Packing;
            Medicine.Image = request.Image;
            Medicine.Status = request.Status;

            _dataContext.Medicines.Add(Medicine);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
    }
}
