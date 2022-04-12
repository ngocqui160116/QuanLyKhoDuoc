using Falcon.Web.Core.Security;
using Falcon.Core.Domain.Users;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Auth;
using Falcon.Services.Users;
using System.Linq;
using Falcon.Web.Core.Auth;
using System.Threading.Tasks;
using Phoenix.Shared.Core;
using Phoenix.Shared;
using Phoenix.Server.Data.Entity;

namespace Phoenix.Server.Services.MainServices.Users
{
    public interface IUserService
    {
        User GetUserById(int id);
        Task<CrudResult> CreateUser(UserRequest request);
    }

    public class UserService : IUserService
    {
        private const int SaltLenght = 6;
        private readonly DataContext _dataContext;
        private readonly IEncryptionService _encryptionService;
        private readonly UserAuthService _userAuthService;


        public UserService(DataContext dataContext, IEncryptionService encryptionService, UserAuthService userAuthService)
        {
            _dataContext = dataContext;
            _encryptionService = encryptionService;
            _userAuthService = userAuthService;
        }
        public User GetUserById(int id) => _dataContext.Users.Find(id);
        //public LoginResponse ValidateUser(LoginRequest request)
        //{
        //    var result = new LoginResponse();
        //    var loginResult = ValidateFalconUser(request.UserName, request.Password);
        //    if (loginResult == FalconUserLoginResults.Successful)
        //    {
        //        var user = _dataContext.FalconUsers.FirstOrDefault(r => r.Username == request.UserName);
        //        if (user != null)
        //        {
        //            var employee = _dataContext.WMS_Employees.FirstOrDefault(r => r.FalconUserId == user.Id);
        //            if (employee == null || !employee.Active || employee.Deleted)
        //            {
        //                result.LoginResult = LoginResult.UserNotFound;
        //                return result;
        //            }
        //            result.EmployeeId = employee.Id;
        //            result.Name = employee.FullName;
        //            result.IsSuccess = true;
        //        }
        //    }
        //    return result;
        //}
        private FalconUserLoginResults ValidateFalconUser(string username, string password)
        {
            return FalconUserLoginResults.Successful;
        }

        public async Task<CrudResult> CreateUser(UserRequest request)
        {
            var User = new User();
            User.UserName = request.UserName;

            var salt = _encryptionService.CreateSaltKey(SaltLenght);
            User.Salt = salt;
            User.Password = _encryptionService.CreatePasswordHash(request.Password, salt);
           // User.Salt = request.Salt;
            User.Active = true;
            User.Roles = "Admin";
            User.Deleted = request.Deleted;

            _dataContext.Users.Add(User);
            await _dataContext.SaveChangesAsync();

            var Staff = new Staff();
            Staff.Name = request.Name;
            Staff.PhoneNumber = request.PhoneNumber;
            Staff.Authority = User.Roles;
            Staff.User_Id = User.Id;

            _dataContext.Staffs.Add(Staff);
            await _dataContext.SaveChangesAsync();

          
            return new CrudResult() { IsOk = true };
        }
    }
}