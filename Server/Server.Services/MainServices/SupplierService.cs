using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Supplier;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Server.Services.MainServices
{
    public interface ISupplierService
    {
        List<SupplierDto> GetAllSupplier(SupplierRequest request);
    }
    public class SupplierService : ISupplierService
    {
        private readonly DataContext _dataContext;
        public SupplierService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public List<SupplierDto> GetAllSupplier(SupplierRequest request)
        {
            //setup query
            var query = _dataContext.Suppliers.AsQueryable().Where(r => !r.Deleted);

            //if (!string.IsNullOrEmpty(request.IdSupplier.ToString()))
            //{
            //    query = query.Where(d => d.IdSupplier.ToString().Contains(request.IdSupplier.ToString()));
            //}

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(d => d.Name.Contains(request.Name));
            }
           
            
            //filter

            var data = query.ToList();
            return data.MapTo<SupplierDto>();
        }
    }
}
