using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Output;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Server.Services.MainServices
{
    public interface IOutputService
    {
        List<OutputDto> GetAllOutput(OutputRequest request);
    }
    public class OutputService : IOutputService
    {
        private readonly DataContext _dataContext;
        public OutputService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public List<OutputDto> GetAllOutput(OutputRequest request)
        {
            //setup query
            var query = _dataContext.Outputs.AsQueryable();
            if (!string.IsNullOrEmpty(request.Id))
            {
                query = query.Where(d => d.Id.Contains(request.Id));
            }
            //if (!string.IsNullOrEmpty(request.IdStaff.ToString()))
            //{
            //    query = query.Where(d => d.IdStaff.ToString().Contains(request.IdStaff.ToString()));
            //}
            //if (!string.IsNullOrEmpty(request.IdCustomer.ToString()))
            //{
            //    query = query.Where(d => d.IdCustomer.ToString().Contains(request.IdCustomer.ToString()));
            //}

            var data = query.ToList();
            return data.MapTo<OutputDto>();
        }
    }
}
