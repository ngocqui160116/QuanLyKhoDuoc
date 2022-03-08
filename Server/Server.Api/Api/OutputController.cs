using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
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
        public List<OutputDto> GetAllOutput([FromBody] OutputRequest request)
        {
            return _OutputService.GetAllOutput(request);
        }

    }
}