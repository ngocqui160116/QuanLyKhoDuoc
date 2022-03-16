using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.InputInfo;
using Phoenix.Shared.Medicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class Medicine_ImageController : Controller
    {
        // GET: Admin/Customer
        private readonly IMedicine_ImageService _medicine_imageService;

        public Medicine_ImageController(IMedicine_ImageService medicine_imageService)
        {
            _medicine_imageService = medicine_imageService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, Medicine_ImageModel model)
        {
            var medicine_images = await _medicine_imageService.GetAllMedicine_Image(new Medicine_ImageRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize
            });

            var gridModel = new DataSourceResult
            {
                Data = medicine_images.Data,
                Total = medicine_images.DataCount
            };
            return Json(gridModel);
        }
    }
}