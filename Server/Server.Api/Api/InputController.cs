﻿using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
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
        public List<InputDto> GetAllInput([FromBody] InputRequest request)
        {
            return _InputService.GetAllInput(request);
        }

    }
}