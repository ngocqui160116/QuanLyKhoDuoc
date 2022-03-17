using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Server.Services.MainServices.Media;
using Phoenix.Server.Services.MainServices.Media.Models;
using Phoenix.Shared.Common;
using Phoenix.Shared.Core;
using Phoenix.Shared.Vendor;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/vendor")]
    public class VendorController : BaseApiController
    {
        private readonly ImageService _vendorService;
        public VendorController(ImageService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet]
        [Route("GetImage")]
        public Medicine_Image GetImageById(int id)
        {
            return _vendorService.GetImageById(id);
        }

    }
}