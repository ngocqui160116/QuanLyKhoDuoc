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
using Phoenix.Shared.Supplier;
using Falcon.Web.Core.Helpers;
using Phoenix.Server.Web.Areas.Admin.Models.Unit;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class UnitController : BaseController
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
        public ActionResult Create()
        {
            var model = new UnitModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(UnitModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var supplier = await _unitService.Create(new UnitRequest
            {
                Name = model.Name
            });
            if (!supplier.Success)
            {
                ErrorNotification("Thêm mới đơn vị tính thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới đơn vị tính thành công");
            return RedirectToAction("Index");
        }
        public ActionResult Update(int id)
        {
            var unitDto = _unitService.GetUnitById(id);
            if (unitDto == null)
            {
                return RedirectToAction("Index");
            }

            var unitModel = unitDto.MapTo<UnitModel>();
            return View(unitModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(UnitModel model)
        {
            var unit = _unitService.GetUnitById(model.Id);
            if (unit == null)
                return RedirectToAction("Index");
            if (!ModelState.IsValid)
                return View(model);
            var units = await _unitService.Update(new UnitRequest
            {
                Id = model.Id,
                Name = model.Name
            });
            SuccessNotification("Chỉnh sửa thông tin chương trình thành công");
            return RedirectToAction("Index");
        }
    }
}