using Falcon.Web.Core.Auth;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Server.Services.MainServices.Users;
using Phoenix.Shared;
using Phoenix.Shared.Core;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/user")]
    public class UserApiController : BaseApiController
    {
        private readonly UserAuthService _userAuthService;
        private readonly UserService _userService;
        public UserApiController(UserAuthService userAuthService, UserService userService)
        {
            _userAuthService = userAuthService;
            _userService = userService;
        }

        [HttpPost]
        [Authorize]
        [Route("changepwd")]
        public async Task<CrudResult> ChangePassword(string phone, string oldPwd, string newPwd) => await _userAuthService.ChangePasswordNew(phone, oldPwd, newPwd);

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("forgotpwd")]
        //public async Task<CrudResult> ForgotPassword(string phone, string newPwd) => await _userAuthService.ForgotPassword(phone, newPwd);

        [HttpPost]
        [Route("CreateUser")]
        public Task<CrudResult> CreateUser([FromBody] UserRequest request)
        {
            return _userService.CreateUser(request);
        }
    }
}