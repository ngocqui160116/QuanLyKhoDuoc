using Falcon.Web.Framework.Kendoui;
using Newtonsoft.Json;
using Phoenix.Server.Services.Database;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.InventoryTags;
using Phoenix.Shared.Common;
using Phoenix.Shared.Input;
using Phoenix.Shared.InventoryTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class InventoryTagsController : BaseController
    {
        // GET: Admin/Customer
        private readonly IInventoryTagsService _inventorytagsService;

        public InventoryTagsController(IInventoryTagsService inventorytagsService)
        {
            _inventorytagsService = inventorytagsService;
        }

        public ActionResult Index()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, InventoryTagsModel model)
        {
            var inputs = await _inventorytagsService.GetAll(new InventoryTagsRequest()
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

    }
}