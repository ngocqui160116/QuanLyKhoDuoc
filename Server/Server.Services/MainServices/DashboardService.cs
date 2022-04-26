using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.InventoryTags;
using Phoenix.Shared.Medicine;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IDashboardService
    {
        Task<BaseResponse<MedicineDto>> GetAllMedicine(MedicineRequest request);
    }
    public class DashboardService : IDashboardService
    {
        private readonly DataContext _dataContext;
        public DashboardService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<BaseResponse<MedicineDto>> GetAllMedicine(MedicineRequest request)
        {
            var result = new BaseResponse<MedicineDto>();
            try
            {
                //setup query
                var query = _dataContext.Medicines.AsQueryable();
                query = query.OrderByDescending(d => d.IdMedicine);

                var data = await query.ToListAsync();
                result.Data = data.MapTo<MedicineDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

    }
}
