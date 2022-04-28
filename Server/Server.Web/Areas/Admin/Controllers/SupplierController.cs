using Falcon.Web.Core.Helpers;
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
            var suppliers = await _supplierService.GetAll(new SupplierRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber
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
            var supplier = await _supplierService.Create(new SupplierRequest
            {
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Email = model.Email
            });
            if (!supplier.Success)
            {
                ErrorNotification("Thêm mới đại lý thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới đại lý thành công");
            return RedirectToAction("Index");
        }
        public ActionResult Update(int id)
        {
            var supplierDto = _supplierService.GetSupplierById(id);
            if (supplierDto == null)
            {
                return RedirectToAction("Index");
            }

            var supplierModel = supplierDto.MapTo<SupplierModel>();
            return View(supplierModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(SupplierModel model)
        {
            var supplier = _supplierService.GetSupplierById(model.IdSupplier);
            if (supplier == null)
                return RedirectToAction("Index");
            if (!ModelState.IsValid)
                return View(model);
            var suppliers = await _supplierService.Update(new SupplierRequest
            {
                IdSupplier = model.IdSupplier,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Address = model.Address
            });
            SuccessNotification("Chỉnh sửa thông tin nhà cung cấp thành công");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var supplier = _supplierService.GetSupplierById(id);
            if (supplier == null)
                //No email account found with the specified id
                return RedirectToAction("Index");

            await _supplierService.Delete(supplier.IdSupplier);
            SuccessNotification("Xóa nhà cung cấp thành công");
            return RedirectToAction("Index");
        }
    }
}