using Phoenix.Server.Web.Areas.Admin.Models.Staff;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class StaffController : Controller
    {
        // GET: Admin/Customer
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, StaffModel model)
        {
            var staffs = await _staffService.GetAllStaff(new StaffRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                Name = model.Name
            });

            var gridModel = new DataSourceResult
            {
                Data = staffs.Data,
                Total = staffs.DataCount
            };
            return Json(gridModel);
        }
    }
}