using Phoenix.Server.Web.Areas.Admin.Models.Medicine;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Medicine;
using Phoenix.Shared.Medicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class MedicineController : BaseController
    {
        //Phoenix.Server.Data.Entity.Medicine db =  
        // GET: Admin/Customer
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, MedicineModel model)
        {
            var medicines = await _medicineService.GetAllMedicine(new MedicineRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize
            });

            var gridModel = new DataSourceResult
            {
                Data = medicines.Data,
                Total = medicines.DataCount
            };
            return Json(gridModel);
        }
        /*public ActionResult Create()
        {
            var model = new MedicineModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(MedicineModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var medicine = await _medicineService.CreateMedicine(new MedicineRequest
            {
                Name = model.Name,
                RegistrationNumber = model.RegistrationNumber,
                //IdGroup = model.IdGroup,
                ViewBag.IdGroup = new SelectList(model.IdGroup(n => n.Name))
                Active = model.Active,
                Content = model.Content,
                Packing = model.Packing,
                Amount = model.Amount,
                Image = model.Image,
                Status = model.Status
            });
            if (!medicine.success)
            {
                ErrorNotification("Thêm mới đại lý thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới đại lý thành công");
            return RedirectToAction("Create");
        }*/
        [HttpGet]
        /*public ActionResult Create(MedicineModel model)
        {
            ViewBag.IdGroup = new SelectList();
        }*/
    }
}