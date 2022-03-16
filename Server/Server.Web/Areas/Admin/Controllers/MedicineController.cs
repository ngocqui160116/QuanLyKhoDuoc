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
    public class MedicineController : Controller
    {
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
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        } 
    }
}