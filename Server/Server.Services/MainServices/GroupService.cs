using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Group;
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
        Task<CrudResult> CreateGroup (GroupRequest request);
        Task<CrudResult> UpdateGroup(int IdGroup, GroupRequest request);
        Task<CrudResult> DeleteGroup(int IdGroup);
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

                var data = await query.ToListAsync();
                result.Data = data.MapTo<GroupDto>();
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        public async Task<CrudResult> CreateGroup(GroupRequest request)
        {
            var Group = new Group();
            Group.Name = request.Name;      
            _dataContext.Groups.Add(Group);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        //Task<CrudResult> UpdateGroup(int IdGroup, GroupRequest request);
        //Task<CrudResult> DeleteGroup(int IdGroup);
        public async Task<CrudResult> UpdateGroup(int IdGroup, GroupRequest request)
        {
            var Group = _dataContext.Groups.Find(IdGroup);
            Group.Name = request.Name;
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }

        public async Task<CrudResult> DeleteGroup(int IdGroup)
        {
            var Group = _dataContext.Groups.Find(IdGroup);
            if (Group == null)
                return new CrudResult()
                {
                    ErrorCode = CommonErrorStatus.KeyNotFound,
                    ErrorDescription = "Xoá không thành công."
                };
            _dataContext.Groups.Remove(Group);
            await _dataContext.SaveChangesAsync();
            return new CrudResult() { IsOk = true };
        }
    }
}
