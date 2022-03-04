using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Group;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Server.Services.MainServices
{
    public interface IGroupService
    {
        List<GroupDto> GetAllGroup(GroupRequest request);
    }
    public class GroupService : IGroupService
    {
        private readonly DataContext _dataContext;
        public GroupService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public List<GroupDto> GetAllGroup(GroupRequest request)
        {
            //setup query
            var query = _dataContext.Groups.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(d => d.Name.Contains(request.Name));
            }
            //if (!string.IsNullOrEmpty(request.IdGroup.ToString()))
            //{
            //    query = query.Where(d => d.IdGroup.ToString().Contains(request.IdGroup.ToString()));
            //}
           
            //filter
            
           

           
            var data = query.ToList();
            return data.MapTo<GroupDto>();
        }
    }
}
