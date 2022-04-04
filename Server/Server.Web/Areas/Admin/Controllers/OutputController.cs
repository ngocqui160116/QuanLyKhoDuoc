using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Input;
using Phoenix.Shared.Common;
using Phoenix.Shared.Input;
using Phoenix.Shared.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class OutputController : BaseController
    {
        // GET: Admin/Customer
        private readonly IOutputService _outputService;

        public OutputController(IOutputService outputService)
        {
            _outputService = outputService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, OutputModel model)
        {
            var outputs = await _outputService.GetAll(new OutputRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize
            });

            var gridModel = new DataSourceResult
            {
                Data = outputs.Data,
                Total = outputs.DataCount
            };
            return Json(gridModel);
        }
    }
}