using Falcon.Web.Core.Helpers;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Input;
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
        public ActionResult Detail(int Id)
        {
            //DataContext db = new DataContext();
            //var inputinfo = db.InputInfos.Where(n => n.IdInput.Equals(Id)).ToList();
            var model = new InputInfoModel();
            model.Id = Id;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Detail(DataSourceRequest command, InputInfoModel model)
        {
            var inputinfos = await _inputinfoService.GetAllInputInfoById(model.Id,new InputInfoRequest()
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
            ViewBag.IdMedicine = new SelectList(db.Medicines.OrderBy(n => n.Name), "IdMedicine", "Name", selectedId);
        }
        public ActionResult Create()
        {
            SetViewBag();

            var inpputinfomodel = new InputInfoModel();
            var inputmodel = new InputModel();
            return View(inpputinfomodel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(InputInfoModel inputinfomodel, InputModel inputmodel)
        {
            SetViewBag();
            if (!ModelState.IsValid)
                return View(inputinfomodel);
            var inputs = await _inputinfoService.Create(new InputInfoRequest
            {
                IdStaff = inputmodel.IdStaff,
                IdSupplier = inputmodel.IdSupplier,
                DateInput = inputmodel.DateInput,
                Status = inputmodel.Status,
                   
            });
            if (!inputs.Success)
            {
                ErrorNotification("Thêm mới không thành công");
                return View(inputinfomodel);
            }
            SuccessNotification("Thêm mới thành công");
            return RedirectToAction("Create");
        }
        public ActionResult Expired()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Expired(DataSourceRequest command, InputInfoModel model)
        {
            var inputinfos = await _inputinfoService.GetExpiredMedicine(new InputInfoRequest()
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
        ///Cập nhật thuốc vào kho
        public ActionResult Complete(int Id)
        {
            var model = new InputInfoModel();
            // gán Id hóa đơn vào Id chi tiết hóa đơn
            model.Id = Id;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Complete(InputInfoModel inputinfomodel, InputModel model)
        {
            SetViewBag();
            if (!ModelState.IsValid)
                return View(inputinfomodel);
            //var inputinfos = await _inputinfoService.GetAllInputInfoById(model.Id, new );
            var inputs = await _inputinfoService.Complete(inputinfomodel.Id, new InputInfoRequest
            {
                IdStaff = model.IdStaff,
                IdSupplier = model.IdSupplier,
                DateInput = model.DateInput,
                Status = model.Status,

            });
            if (!inputs.Success)
            {
                ErrorNotification("Thêm mới không thành công");
                return View(inputinfomodel);
            }
            SuccessNotification("Thêm mới thành công");
            return RedirectToAction("../Input/Complete");
        }
    }
}