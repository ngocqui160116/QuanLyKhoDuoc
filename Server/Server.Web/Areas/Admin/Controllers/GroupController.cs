using Phoenix.Server.Web.Areas.Admin.Models.Group;
using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.MainServices;
using Phoenix.Shared.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Falcon.Web.Core.Helpers;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class GroupController : BaseController
    {
        // GET: Admin/Customer
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> List(DataSourceRequest command, GroupModel model)
        {
            var groups = await _groupService.GetAll(new GroupRequest()
            {
                Page = command.Page - 1,
                PageSize = command.PageSize,
                Name = model.Name
            });

            var gridModel = new DataSourceResult
            {
                Data = groups.Data,
                Total = groups.DataCount
            };
            return Json(gridModel);
        }
        public ActionResult Create()
        {
            var model = new GroupModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(GroupModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var group = await _groupService.Create(new GroupRequest
            {
                Name = model.Name
            });
            if (!group.Success)
            {
                ErrorNotification("Thêm mới nhóm thuốc thành công");
                return View(model);
            }
            SuccessNotification("Thêm mới nhóm thuốc thành công");
            return RedirectToAction("Index");
        }
        public ActionResult Update(int id)
        {
            var groupDto = _groupService.GetGroupById(id);
            if (groupDto == null)
            {
                return RedirectToAction("Index");
            }

            var groupModel = groupDto.MapTo<GroupModel>();
            return View(groupModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(GroupModel model)
        {
            var unit = _groupService.GetGroupById(model.IdGroup);
            if (unit == null)
                return RedirectToAction("Index");
            if (!ModelState.IsValid)
                return View(model);
            var units = await _groupService.Update(new GroupRequest
            {
                IdGroup = model.IdGroup,
                Name = model.Name
            });
            SuccessNotification("Chỉnh sửa thông tin nhóm thuốc thành công");
            return RedirectToAction("Index");
        }
    }
}