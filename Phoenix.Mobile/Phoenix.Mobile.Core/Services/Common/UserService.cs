using Phoenix.Mobile.Core.Models.User;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared;
using Phoenix.Shared.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IUserService
    {
        Task<CrudResult> ChangePassword(string phone, string oldPwd, string newPwd);
        Task<CrudResult> ForgotPassword(string phone, string newPwd);
        Task<List<UserModel>> GetAllUser(UserRequest request);
        Task<CrudResult> CreateUser(UserRequest request);
    }

    public class UserService : IUserService
    {
        private readonly IUserProxy _userProxy;
        public UserService(IUserProxy userProxy)
        {
            _userProxy = userProxy;
        }

        public Task<CrudResult> ChangePassword(string phone, string oldPwd, string newPwd)
        {
            return _userProxy.ChangePassword(phone, oldPwd, newPwd);
        }

        public Task<CrudResult> ForgotPassword(string phone, string newPwd)
        {
            return _userProxy.ForgotPassword(phone, newPwd);
        }

        public async Task<List<UserModel>> GetAllUser(UserRequest request)
        {
            var User = await _userProxy.GetAllUser(request);
            return User.Data.MapTo<UserModel>();
        }
        public Task<CrudResult> CreateUser(UserRequest request)
        {
            return _userProxy.CreateUser(request);
        }
    }
}
