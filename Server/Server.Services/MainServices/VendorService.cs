using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.OutputInfo;
using Phoenix.Shared.Vendor;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Server.Services.MainServices
{
    public interface IVendorService
    {
       // List<OutputInfoDto> GetAllVendors(VendorRequest request);
    }
    public class VendorService : IVendorService
    {
        private readonly DataContext _dataContext;
        public VendorService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        //public List<OutputInfoDto> GetAllVendors( )
        //{
        //    ////setup query
        //    //var query = 
        //    //filter


        //    //if (!string.IsNullOrEmpty(request.Name))
        //    //{
        //    //    query = query.Where(d => d.Name.Contains(request.Name));
        //    //}
        //    //if (!string.IsNullOrEmpty(request.Phone))
        //    //{
        //    //    query = query.Where(d => d.Phone.Contains(request.Phone));
        //    //}

        //    ////Vendor ===> VendorDto
        //    ////var data = query.Select(d => new OutputInfoDto
        //    ////{
        //    ////    Id = d.Id,
        //    ////    SoLuong = d.Count
        //    ////}).ToList();

        //    //return data.MapTo<OutputInfoDto>();
        //}
    }
}
