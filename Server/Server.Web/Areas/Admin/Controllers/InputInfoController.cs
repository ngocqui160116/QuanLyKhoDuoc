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
        public ActionResult Detail(string Id)
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

            var model = new InputInfoModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(InputInfoModel model)
        {
            SetViewBag();
            if (!ModelState.IsValid)
                return View(model);
            var inputs = await _inputinfoService.Create(new InputInfoRequest
            {
                IdInput = model.IdInput,
                IdMedicine = model.IdMedicine,
                IdBatch = model.IdBatch,
                Count = model.Count,
                InputPrice = model.InputPrice,
                Total = model.Total,
                DueDate = model.DueDate,
                IdUnit = model.IdUnit
            });
            if (!inputs.Success)
            {
                ErrorNotification("Thêm mới không thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới thành công");
            return RedirectToAction("Create");
        }
    }
}