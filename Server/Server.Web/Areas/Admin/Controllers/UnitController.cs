using Phoenix.Server.Web.Areas.Admin.Models.Group;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Phoenix.Shared.Unit;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class UnitController : Controller
    {
        // GET: Admin/Customer
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, UnitModel model)
        {
            var units = await _unitService.GetAllUnit(new UnitRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                Name = model.Name
            });

            var gridModel = new DataSourceResult
            {
                Data = units.Data,
                Total = units.DataCount
            };
            return Json(gridModel);
        }
    }
}