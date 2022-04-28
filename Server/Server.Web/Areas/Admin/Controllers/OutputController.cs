using Falcon.Web.Framework.Kendoui;
using Newtonsoft.Json;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Output;
using Phoenix.Shared.Common;
using Phoenix.Shared.Input;
using Phoenix.Shared.Inventory;
using Phoenix.Shared.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class OutputController : BaseController
    {
        // GET: Admin/Customer
        private readonly IOutputService _outputService;
        private readonly IInventoryService _inventoryService;

        public OutputController(IOutputService outputService, IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
            _outputService = outputService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, OutputModel model)
        {
            var outputs = await _outputService.GetAll(new OutputRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize
                //Id = model.Id
            });

            var gridModel = new DataSourceResult
            {
                Data = outputs.Data,
                Total = outputs.DataCount
            };
            return Json(gridModel);
        }

        public void SetViewBag(long? selectedId = null)
        {
            DataContext db = new DataContext();
            var inputs = _inventoryService.GetInventory();
            ViewBag.IdStaff = new SelectList(db.Staffs.OrderBy(n => n.Name), "IdStaff", "Name", selectedId);
            ViewBag.IdReason = new SelectList(db.Reasons.OrderBy(n => n.NameReason), "IdReason", "NameReason", selectedId);
            ViewBag.IdMedicine = new SelectList(db.Inventories.OrderBy(n => n.IdMedicine), "LotNumber", "IdMedicine", selectedId);
            
        }

        // Create Vendor
        public async Task<ActionResult> Create()
        {
            
            SetViewBag();
            var model = new OutputModel();

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
                DateOutput = model.DateOutput,

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