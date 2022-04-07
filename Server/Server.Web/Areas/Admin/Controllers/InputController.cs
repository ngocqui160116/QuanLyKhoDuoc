using Falcon.Web.Core.Helpers;
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
        //Nhập thuốc vào kho
        public ActionResult Complete(int id)
        {
            SetViewBag();
            var inputDto = _inputService.GetInputById(id);
            //var inputinfoDto = _inputService
            if (inputDto == null)
            {
                return RedirectToAction("Index");
            }

            var inputModel = inputDto.MapTo<InputModel>();
            return View(inputModel);
        }

        [HttpPost]
        public async Task<ActionResult> Complete(InputModel model)
        {
            SetViewBag();
            var input = _inputService.GetInputById(model.Id);
            if (input == null)
                return RedirectToAction("Index");
            if (!ModelState.IsValid)
                return View(model);
            var inputs = await _inputService.Complete(new InputRequest
            {
                List = JsonConvert.DeserializeObject<List<InputContentDto>>(model.TableContent),
                Status = model.Status
            }) ;
            SuccessNotification("Chỉnh sửa thông tin chương trình thành công");
            return RedirectToAction("Index", new { id = model.Id });
        }

        /*[HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var input = _inputService.GetInputById(id);
            if (input == null)
                //No email account found with the specified id
                return RedirectToAction("Index");

            await _inputService.Delete(input.Id);
            SuccessNotification("Xóa đại lý thành công");
            return RedirectToAction("Index");
        }*/
    }
}