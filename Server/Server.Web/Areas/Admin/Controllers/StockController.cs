using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Stock;
using Phoenix.Shared.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class StockController : BaseController
    {
        // GET: Admin/Customer
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        public ActionResult Index()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, StockModel model)
        {
            var inputs = await _stockService.GetAll(new StockRequest()
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
        public async Task<ActionResult> Create()
        {

            SetViewBag();
            var model = new StockModel();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(OutputModel model)
        {
            SetViewBag();
            if (!ModelState.IsValid)
                return View(model);
            var inputs = await _outputService.Create(new OutputRequest
            {
                IdStaff = model.IdStaff,
                IdReason = model.IdReason,
                DateOutput = DateTime.Now,

                List = JsonConvert.DeserializeObject<List<OutputContentDto>>(model.TableContent)

            });
            if (!inputs.Success)
            {
                ErrorNotification("Thêm mới không thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới thành công");
            return RedirectToAction("Index");
        }
    }
}