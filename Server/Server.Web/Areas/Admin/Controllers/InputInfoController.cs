using Falcon.Web.Core.Helpers;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.InputInfo;
using Phoenix.Shared.InputInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class InputInfoController : BaseController
    {
        // GET: Admin/InputInfo
        private readonly IInputInfoService _inputinfoService;

        public InputInfoController(IInputInfoService inputinfoService)
        {
            _inputinfoService = inputinfoService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, InputInfoModel model)
        {
            var inputinfos = await _inputinfoService.GetAllInputInfo(new InputInfoRequest()
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
        public void SetViewBag(long? selectedId = null)
        {
            DataContext db = new DataContext();
            ViewBag.IdStaff = new SelectList(db.Staffs.OrderBy(n => n.Name), "IdStaff", "Name", selectedId);
            ViewBag.IdSupplier = new SelectList(db.Suppliers.OrderBy(n => n.Name), "IdSupplier", "Name", selectedId);
        }

        /*public ActionResult Detail(string id)
        {
            SetViewBag();
            var inputinfoDto = _inputinfoService.GetInputInfoById(id);
            if (inputinfoDto == null)
            {
                return RedirectToAction("Index");
            }

            var inputinfoModel = inputinfoDto.MapTo<InputInfoModel>();
            return View(inputinfoModel);
        }*/

        [HttpPost]
        public async Task<ActionResult> Detail(DataSourceRequest command, InputInfoModel model)
        {
            var inputinfos = await _inputinfoService.GetAllInputInfo(new InputInfoRequest()
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