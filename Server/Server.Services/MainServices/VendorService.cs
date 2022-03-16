using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Vendor;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Server.Services.MainServices
{
    public interface IVendorService
    {
        List<VendorDto> GetAllVendors(VendorRequest request);
    }
    public class VendorService : IVendorService
    {
        private readonly DataContext _dataContext;
        public VendorService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public List<VendorDto> GetAllVendors(VendorRequest request)
        {
            //setup query
            var query = _dataContext.Vendors.AsQueryable().Where(r => !r.Deleted);
            //filter
          
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(d => d.Name.Contains(request.Name));
            }
            if (!string.IsNullOrEmpty(request.Phone))
            {
                query = query.Where(d => d.Phone.Contains(request.Phone));
            }

            //Vendor ===> VendorDto
            //var data = query.Select(d => new VendorDto
            //{
            //    Id = d.Id,
            //    Name = d.Name,
            //    Phone = d.Phone,
            //    Address = d.Address,
            //    ImageUrl = d.ImageRecord.AbsolutePath
            //}).ToList();
            var data = query.ToList();
            return data.MapTo<VendorDto>();
        }
    }
}
