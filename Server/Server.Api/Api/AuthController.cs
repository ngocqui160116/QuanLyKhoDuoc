using System.Threading.Tasks;
using System.Web.Http;
using Phoenix.Shared.Auth;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared;
using System.Security.Claims;
using Falcon.Web.Core.Auth;
using Phoenix.Server.Services.MainServices.Users;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/auth")]
    public class AuthController : BaseApiController
    {
        private readonly UserAuthService _authService;
        private readonly UserService _userService;
        public AuthController(UserAuthService authService, UserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<TokenResponse> Login([FromBody] TokenRequest request)
        {
            var res = await _authService.LoginAsync(request);
            return res;
        }

        [HttpGet]
        [Route("GetUserFromToKen")]
        public async Task<UserDto> GetUserFromToken()
        {
            var identity = User.Identity as ClaimsIdentity;

            if (identity == null) return null;
            var userId = identity.FindFirst("UserId").Value;
            var user = _authService.GetUserById(int.Parse(userId));
            return user;
        }
    }
}