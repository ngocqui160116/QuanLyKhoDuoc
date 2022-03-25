using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Input;
using Phoenix.Shared.Common;
using Phoenix.Shared.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class InputController : BaseController
    {
        // GET: Admin/Customer
        private readonly IInputService _inputService;

        public InputController(IInputService inputService)
        {
            _inputService = inputService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, InputModel model)
        {
            var inputs = await _inputService.GetAllInput(new InputRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize
            });

            var gridModel = new DataSourceResult
            {
                Data = inputs.Data,
                Total = inputs.DataCount
            };
            return Json(gridModel);
        }


        public void SetViewBag(long? selectedId = null)
        {
            DataContext db = new DataContext();
            ViewBag.IdStaff = new SelectList(db.Staffs.OrderBy(n => n.Name), "IdStaff", "Name", selectedId);
            ViewBag.IdSupplier = new SelectList(db.Suppliers.OrderBy(n => n.Name), "IdSupplier", "Name", selectedId);
        }

        // Create Vendor
        public ActionResult Create()
        {
            SetViewBag();

            var model = new InputModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(InputModel model)
        {
            SetViewBag();
            if (!ModelState.IsValid)
                return View(model);
            var inputs = await _inputService.Create(new InputRequest
            {
                Id = model.Id,
                IdStaff = model.IdStaff,
                IdSupplier = model.IdSupplier,
                DateInput = DateTime.Now
            });
            if (!inputs.Success)
            {
                ErrorNotification("Thêm mới không thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới thành công");
            return RedirectToAction("Create");
        }
       
        /*[HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var input = _inputService.GetInputById(id);
            if (input == null)
                //No email account found with the specified id
                return RedirectToAction("Index");

            await _inputService.Delete(input.Id);
            SuccessNotification("Xóa đại lý thành công");
            return RedirectToAction("Index");
        }*/
    }
}