using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.OutputInfo;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Server.Services.MainServices
{
    public interface IOutputInfoService
    {
        List<OutputInfoDto> GetAllOutputInfo(OutputInfoRequest request);
    }
    public class OutputInfoService : IOutputInfoService
    {
        private readonly DataContext _dataContext;
        public OutputInfoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public List<OutputInfoDto> GetAllOutputInfo(OutputInfoRequest request)
        {
            //setup query
            var query = _dataContext.OutputInfos.AsQueryable();

            if (!string.IsNullOrEmpty(request.IdOutputInfo))
            {
                query = query.Where(d => d.IdOutput.Contains(request.IdOutputInfo));
            }
            //if (!string.IsNullOrEmpty(request.IdMedicine.ToString()))
            //{
            //    query = query.Where(d => d.IdMedicine.ToString().Contains(request.IdMedicine.ToString()));
            //}
            //if (!string.IsNullOrEmpty(request.Unit.ToString()))
            //{
            //    query = query.Where(d => d.Unit.ToString().Contains(request.Unit.ToString()));
            //}


            var data = query.ToList();
            return data.MapTo<OutputInfoDto>();
        }
    }
}
