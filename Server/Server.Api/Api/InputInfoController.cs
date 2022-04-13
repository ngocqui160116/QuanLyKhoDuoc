using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Input;
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
        public async Task<BaseResponse<InputInfoDto>> GetAllInputInfo([FromBody] InputInfoRequest request)
        {
            return await _InputInfoService.GetAllInputInfo(request);
        }

        [HttpPost]
        [Route("CreateInputInfo")]
        public Task<BaseResponse<InputInfoDto>> CreateInputInfo(InputInfoRequest request)
        {
            return _InputInfoService.CreateInputInfo( request);
        }

        

        [HttpPost]
        [Route("Complete")]
        public Task<BaseResponse<InputInfoDto>> Complete(int Id, InputInfoRequest request)
        {
            return _InputInfoService.Complete(Id , request);
        }
        
    }
}