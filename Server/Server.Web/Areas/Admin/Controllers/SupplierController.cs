using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Supplier;
using Phoenix.Shared.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class SupplierController : BaseController
    {
        // GET: Admin/Customer
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, SupplierModel model)
        {
            var suppliers = await _supplierService.GetAllSupplier(new SupplierRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize
            });

            var gridModel = new DataSourceResult
            {
                Data = suppliers.Data,
                Total = suppliers.DataCount
            };
            return Json(gridModel);
        }
        public ActionResult Create()
        {
            var model = new SupplierModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(SupplierModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var supplier = await _supplierService.CreateSupplier(new SupplierRequest
            {
                Name = model.Name,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
            });
            if (!supplier.success)
            {
                ErrorNotification("Thêm mới đại lý thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới đại lý thành công");
            return RedirectToAction("Create");
        }
    }
}