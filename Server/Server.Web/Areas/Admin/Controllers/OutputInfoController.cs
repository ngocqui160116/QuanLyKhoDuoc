using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.InputInfo;
using Phoenix.Shared.InputInfo;
using Phoenix.Shared.OutputInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class OutputInfoController : Controller
    {
        // GET: Admin/Customer
        private readonly IOutputInfoService _outputinfoService;

        public OutputInfoController(IOutputInfoService outputinfoService)
        {
            _outputinfoService = outputinfoService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, OutputInfoModel model)
        {
            var outputinfos = await _outputinfoService.GetAllOutputInfo(new OutputInfoRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize
            });

            var gridModel = new DataSourceResult
            {
                Data = outputinfos.Data,
                Total = outputinfos.DataCount
            };
            return Json(gridModel);
        }
        public ActionResult Detail(int Id)
        {
            var model = new OutputInfoModel();
            model.Id = Id;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Detail(DataSourceRequest command, OutputInfoModel model)
        {
            var inputinfos = await _outputinfoService.GetAllOutputInfoById(model.Id, new OutputInfoRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize
            });

            var gridModel = new DataSourceResult
            {
                Data = inputinfos.Data,
                Total = inputinfos.DataCount
            };
            return Json(gridModel);
        }
    }
}