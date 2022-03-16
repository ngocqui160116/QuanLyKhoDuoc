using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Output;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/output")]
    public class OutputController : BaseApiController
    {
        private readonly IOutputService _OutputService;
        public OutputController(IOutputService OutputService)
        {
            _OutputService = OutputService;
        }

        [HttpPost]
        [Route("GetAllOutput")]
        public async Task<BaseResponse<OutputDto>> GetAllOutput([FromBody] OutputRequest request)
        {
            return await _OutputService.GetAllOutput(request);
        }

        [HttpPost]
        [Route("CreateOutput")]
        public Task<CrudResult> CreateOutput([FromBody] OutputRequest request)
        {
            return _OutputService.CreateOutput(request);
        }

    }
}