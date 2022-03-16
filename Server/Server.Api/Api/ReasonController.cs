using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Reason;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/reason")]
    public class ReasonController : BaseApiController
    {
      

        private readonly IReasonService _ReasonService;
        public ReasonController(IReasonService ReasonService)
        {
            _ReasonService = ReasonService;
        }

        [HttpPost]
        [Route("GetAllReason")]
        public async Task<BaseResponse<ReasonDto>> GetAllReason([FromBody] ReasonRequest request)
        {
            return await _ReasonService.GetAllReason(request);
        }

    }
}