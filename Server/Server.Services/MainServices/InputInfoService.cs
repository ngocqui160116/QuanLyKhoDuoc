using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.InputInfo;
using Phoenix.Shared.InputInfoDto;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Server.Services.MainServices
{
    public interface IInputInfoService
    {
        List<InputInfoDto> GetAllInputInfo(InputInfoRequest request);
    }
    public class InputInfoService : IInputInfoService
    {
        private readonly DataContext _dataContext;
        public InputInfoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public List<InputInfoDto> GetAllInputInfo(InputInfoRequest request)
        {
            //setup query
            var query = _dataContext.InputInfos.AsQueryable();

            if (!string.IsNullOrEmpty(request.IdInputInfo))
            {
                query = query.Where(d => d.IdInput.Contains(request.IdInputInfo));
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
            return data.MapTo<InputInfoDto>();
        }
    }
}
