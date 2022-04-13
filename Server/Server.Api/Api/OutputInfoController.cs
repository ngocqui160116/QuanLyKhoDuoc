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

        //[HttpPost]
        //[Route("CreateOutputInfo")]
        //public Task<CrudResult> CreateOutputInfo([FromBody] OutputInfoRequest request)
        //{
        //    return _OutputInfoService.CreateOutputInfo(request);
        //}

        [HttpPost]
        [Route("CreateOutputInventory")]
        public Task<CrudResult> CreateOutputInventory(OutputInfoRequest request)
        {
            return _OutputInfoService.CreateOutputInventory(request);
        }

        
    }
}