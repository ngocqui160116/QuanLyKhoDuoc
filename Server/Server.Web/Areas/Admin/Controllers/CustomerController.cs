using Phoenix.Server.Web.Areas.Admin.Models.Customer;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Falcon.Web.Core.Helpers;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Admin/Customer
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, CustomerModel model)
        {
            var customers = await _customerService.GetAllCustomer(new CustomerRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize
            });

            var gridModel = new DataSourceResult
            {
                Data = customers.Data,
                Total = customers.DataCount
            };
            return Json(gridModel);
        }
        public ActionResult Create()
        {
            var model = new CustomerModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CustomerModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var customer = await _customerService.CreateCustomer(new CustomerRequest
            {   
                 Name = model.Name, 
                 Address = model.Address,
                 PhoneNumber = model.PhoneNumber,
                 Email = model.Email,
            });
            if (!customer.success)
            {
                ErrorNotification("Thêm mới đại lý thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới đại lý thành công");
            return RedirectToAction("Create");
        }

    }
}