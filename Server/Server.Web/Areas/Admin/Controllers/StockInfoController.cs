using Falcon.Web.Core.Helpers;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Input;
using Phoenix.Server.Web.Areas.Admin.Models.InputInfo;
using Phoenix.Server.Web.Areas.Admin.Models.StockInfo;
using Phoenix.Shared.InputInfo;
using Phoenix.Shared.StockInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class StockInfoController : BaseController
    {
        // GET: Admin/InputInfo
        private readonly IStockInfoService _stockinfoService;

        public StockInfoController(IStockInfoService stockinfoService)
        {
            _stockinfoService = stockinfoService;
        }

        public ActionResult Index()
        {
            return View();
        }
        /*public ActionResult Detail(int Id)
        {
            //DataContext db = new DataContext();
            //var inputinfo = db.InputInfos.Where(n => n.IdInput.Equals(Id)).ToList();
            var model = new StockInfoModel();
            model.Id = Id;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Detail(DataSourceRequest command, StockInfoModel model)
        {
            var stockinfos = await _stockinfoService.GetAllInputInfoById(model.Id,new StockInfoRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize
            });

            var gridModel = new DataSourceResult
            {
                Data = stockinfos.Data,
                Total = stockinfos.DataCount
            };
            return Json(gridModel);
        }*/
        public void SetViewBag(long? selectedId = null)
        {
            DataContext db = new DataContext();
            ViewBag.IdMedicine = new SelectList(db.Medicines.OrderBy(n => n.Name), "IdMedicine", "Name", selectedId);
        }
        /*public ActionResult Create()
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
        }*/
    }
}