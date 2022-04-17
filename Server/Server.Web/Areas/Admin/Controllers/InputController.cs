using Falcon.Web.Framework.Kendoui;
using Newtonsoft.Json;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Input;
using Phoenix.Server.Web.Areas.Admin.Models.InputInfo;
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
            SetViewBag();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, InputModel model)
        {
            var inputs = await _inputService.GetAll(new InputRequest()
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
            ViewBag.IdMedicine = new SelectList(db.Medicines.OrderBy(n => n.Name), "IdMedicine", "Name", selectedId);
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
                IdStaff = model.IdStaff,
                IdSupplier = model.IdSupplier,
                DateInput = DateTime.Now,
                
                List = JsonConvert.DeserializeObject<List<InputContentDto>>(model.TableContent)

            }) ;
            if (!inputs.Success)
            {
                ErrorNotification("Thêm mới không thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới thành công");
            return RedirectToAction("Index");
        }
        //hủy phiếu nhập
        public ActionResult Cancel(int Id)
        {
            var model = new InputModel();
            // gán Id hóa đơn vào Id chi tiết hóa đơn
            model.Id = Id;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Cancel(InputModel model)
        {
            var input = _inputService.GetInputById(model.Id);
            if (input == null)
                //No email account found with the specified id
                return RedirectToAction("Index");

            await _inputService.Cancel(model.Id);
            SuccessNotification("Đã hủy phiếu nhập");
            return RedirectToAction("InputInfo/Detail/" + model.Id);
        }
        //lấy ds hóa đơn đã hoàn thành
        public ActionResult Complete()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Complete(DataSourceRequest command, InputModel model)
        {
            var inputs = await _inputService.GetCompleteInput(new InputRequest()
            {
                Status = "Đã hoàn thành",
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
        //lấy ds hóa đơn đã lưu
        public ActionResult Saved()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Saved(DataSourceRequest command, InputModel model)
        {
            var inputs = await _inputService.GetSaveInput(new InputRequest()
            {
                Status = "Chờ duyệt",
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
        //lấy ds hóa đơn đã hủy
        public ActionResult Canceled()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Canceled(DataSourceRequest command, InputModel model)
        {
            var inputs = await _inputService.GetCancelInput(new InputRequest()
            {
                Status = "Đã hủy",
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
    }
}