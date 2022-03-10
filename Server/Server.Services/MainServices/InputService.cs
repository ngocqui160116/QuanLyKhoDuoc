using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Input;
using Phoenix.Shared.Vendor;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Server.Services.MainServices
{
    public interface IInputService
    {
        List<InputDto> GetAllInput(InputRequest request);
    }
    public class InputService : IInputService
    {
        private readonly DataContext _dataContext;
        public InputService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public List<InputDto> GetAllInput(InputRequest request)
        {
            //setup query
            var query = _dataContext.Inputs.AsQueryable();
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
            return data.MapTo<InputDto>();
        }
    }
}
