using Falcon.Core;
using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Medicine;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Server.Services.MainServices
{
    public interface IMedicineService
    {
        List<MedicineDto> GetAllMedicine(MedicineRequest request);
      
    }
    public class MedicineService : IMedicineService
    {
        private readonly DataContext _dataContext;
        public MedicineService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách thuốc
        public List<MedicineDto> GetAllMedicine(MedicineRequest request)
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

            var data = query.ToList();
            return data.MapTo<MedicineDto>();
        }
    }
}
