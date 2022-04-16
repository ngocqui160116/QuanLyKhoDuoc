using Falcon.Web.Framework.Kendoui;
using Phoenix.Server.Services.Constants;
using Phoenix.Server.Services.Framework;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Web.Areas.Admin.Models.Medicine;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Phoenix.Server.Web.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Admin/Dashboard
        private readonly PermissionService _permissionService;
        private readonly DashboardService _dashboardService;
        public DashboardController(PermissionService permissionService, DashboardService dashboardService)
        {
            _permissionService = permissionService;
            _dashboardService = dashboardService;
        }
        public ActionResult Index()
        {
            if (_permissionService.UnAuthorize(ServerPermissions.AccessAdminPanel))
            {
                return AccessDeniedView();
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(DataSourceRequest command, MedicineModel model)
        {
            var inputs = await _dashboardService.GetAllMedicine(new Shared.Medicine.MedicineRequest()
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
    }
}