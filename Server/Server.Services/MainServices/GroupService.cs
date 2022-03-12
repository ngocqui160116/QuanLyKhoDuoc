using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Group;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Server.Services.MainServices
{
    public interface IGroupService
    {
        Task<BaseResponse<GroupDto>> GetAllGroup(GroupRequest request);
    }
    public class GroupService : IGroupService
    {
        private readonly DataContext _dataContext;
        public GroupService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public async Task<BaseResponse<GroupDto>> GetAllGroup(GroupRequest request)
        {
            var result = new BaseResponse<GroupDto>();
            try
            {
                // setup query
                var query = _dataContext.Groups.AsQueryable();

                // filter
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(d => d.Name.Contains(request.Name));
                }

                query = query.OrderByDescending(d => d.IdGroup);

                var data = await query.Skip(request.Page * request.PageSize).Take(request.PageSize).ToListAsync();
                result.DataCount = (int)((await query.CountAsync()) / request.PageSize) + 1;
                result.Data = data.MapTo<GroupDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
