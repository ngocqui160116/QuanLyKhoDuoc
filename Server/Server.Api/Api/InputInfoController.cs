using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Core;
using Phoenix.Shared.InputInfo;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/inputInfo")]
    public class InputInfoController : BaseApiController
    {
        private readonly IInputInfoService _InputInfoService;
        public InputInfoController(IInputInfoService InputInfoService)
        {
            _InputInfoService = InputInfoService;
        }

        [HttpPost]
        [Route("GetAllInputInfo")]
        public List<InputInfoDto> GetAllInputInfo([FromBody] InputInfoRequest request)
        {
            return _InputInfoService.GetAllInputInfo(request);
        }

    }
}