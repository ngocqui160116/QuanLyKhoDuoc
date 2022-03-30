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
using Phoenix.Server.Services.Database;
using Falcon.Web.Core.Helpers;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class MedicineController : BaseController
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
                PageSize = command.PageSize,
                IdGroup = model.IdGroup,
                IdUnit = model.IdUnit
            });

            var gridModel = new DataSourceResult
            {
                Data = medicines.Data,
                Total = medicines.DataCount
            };
            return Json(gridModel);
        }


        public void SetViewBag(long? selectedId = null)
        {
            DataContext db = new DataContext();
            ViewBag.IdGroup = new SelectList(db.Groups.OrderBy(n => n.Name), "IdGroup", "Name", selectedId);
            ViewBag.IdUnit = new SelectList(db.Units.OrderBy(n => n.Name), "Id", "Name", selectedId);
        }

        // Create Vendor
        public ActionResult Create()
        {
            SetViewBag();

            var model = new MedicineModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(MedicineModel model)
        {
            SetViewBag();
            if (!ModelState.IsValid)
                return View(model);
            var medicines = await _medicineService.Create(new MedicineRequest
            {
                RegistrationNumber = model.RegistrationNumber,
                Name = model.Name,
                IdGroup = model.IdGroup,
                Active = model.Active,
                Content = model.Content,
                Packing = model.Packing,
                IdUnit = model.IdUnit,
                Image = model.Image,
                Status = model.Status
            });
            if (!medicines.Success)
            {
                ErrorNotification("Thêm mới không thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới thành công");
            return RedirectToAction("Index");
        }
        public ActionResult Update(int id)
        {
            SetViewBag();
            var medicineDto = _medicineService.GetMedicineById(id);
            if (medicineDto == null)
            {
                return RedirectToAction("Index");
            }

            var medicineModel = medicineDto.MapTo<MedicineModel>();
            return View(medicineModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(MedicineModel model)
        {
            SetViewBag();
            var medicine = _medicineService.GetMedicineById(model.IdMedicine);
            if (medicine == null)
                return RedirectToAction("Index");
            if (!ModelState.IsValid)
                return View(model);
            var medicines = await _medicineService.Update(new MedicineRequest
            {
                IdMedicine = model.IdMedicine,
                RegistrationNumber = model.RegistrationNumber,
                Name = model.Name,
                IdGroup = model.IdGroup,
                Active = model.Active,
                Content = model.Content,
                Packing = model.Packing,
                IdUnit = model.IdUnit,
                Image = model.Image,
                Status = model.Status
            });
            SuccessNotification("Chỉnh sửa thông tin chương trình thành công");
            return RedirectToAction("Index", new { id = model.IdMedicine });
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            SetViewBag();
            var medicine = _medicineService.GetMedicineById(id);
            if (medicine == null)
                //No email account found with the specified id
                return RedirectToAction("Index");

            await _medicineService.Delete(medicine.IdMedicine);
            SuccessNotification("Xóa đại lý thành công");
            return RedirectToAction("Index");
        }

    }
}