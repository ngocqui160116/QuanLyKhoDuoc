using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Unit;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Server.Services.MainServices
{
    public interface IUnitService
    {
        List<UnitDto> GetAllUnit(UnitRequest request);
    }
    public class UnitService : IUnitService
    {
        private readonly DataContext _dataContext;
        public UnitService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public List<UnitDto> GetAllUnit(UnitRequest request)
        {
            //setup query
            var query = _dataContext.Units.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(d => d.Name.Contains(request.Name));
            }
            //if (!string.IsNullOrEmpty(request.Id.ToString()))
            //{
            //    query = query.Where(d => d.Id.ToString().Contains(request.Id.ToString()));
            //}
           
            //filter
            
           

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
            return data.MapTo<UnitDto>();
        }
    }
}
