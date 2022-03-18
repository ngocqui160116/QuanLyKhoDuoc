using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/input")]
    public class InputController : BaseApiController
    {
        private readonly IInputService _InputService;
        public InputController(IInputService InputService)
        {
            _InputService = InputService;
        }

        [HttpPost]
        [Route("GetAllInput")]
        public async Task<BaseResponse<InputDto>> GetAllInput([FromBody] InputRequest request)
        {
            return await _InputService.GetAllInput(request);
        }

        [HttpPost]
        [Route("CreateInput")]
        public Task<CrudResult> CreateInput([FromBody] InputRequest request)
        {
            return _InputService.CreateInput(request);
        }

        [HttpPost]
        [Route("UpdateInput")]
        public Task<CrudResult> UpdateInput(string Id, [FromBody] InputRequest request)
        {
            return _InputService.UpdateInput(Id, request);
        }

        [HttpDelete]
        [Route("DeleteInput")]
        public Task<CrudResult> DeleteInput(string Id)
        {
            return _InputService.DeleteInput(Id);
        }

    }
}