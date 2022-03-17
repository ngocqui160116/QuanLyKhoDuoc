using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.OutputInfo;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/outputInfo")]
    public class OutputInfoController : BaseApiController
    {
        private readonly IOutputInfoService _OutputInfoService;
        public OutputInfoController(IOutputInfoService OutputInfoService)
        {
            _OutputInfoService = OutputInfoService;
        }

        [HttpPost]
        [Route("GetAllOutputInfo")]
        public async Task<BaseResponse<OutputInfoDto>> GetAllOutputInfo([FromBody] OutputInfoRequest request)
        {
            return await _OutputInfoService.GetAllOutputInfo(request);
        }

        [HttpPost]
        [Route("CreateOutputInfo")]
        public Task<CrudResult> CreateOutputInfo([FromBody] OutputInfoRequest request)
        {
            return _OutputInfoService.CreateOutputInfo(request);
        }

        [HttpPost]
        [Route("UpdateOutputInfo")]
        public Task<CrudResult> UpdateOutputInfo(int IdOutput, [FromBody] OutputInfoRequest request)
        {
            return _OutputInfoService.UpdateOutputInfo(IdOutput, request);
        }

        [HttpDelete]
        [Route("DeleteOutputInfo")]
        public Task<CrudResult> DeleteOutputInfo(int IdOutput)
        {
            return _OutputInfoService.DeleteOutputInfo(IdOutput);
        }

    }
}