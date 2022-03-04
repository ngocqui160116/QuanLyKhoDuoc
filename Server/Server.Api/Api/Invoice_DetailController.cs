using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Core;
using Phoenix.Shared.Invoice_Detail;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/invoice_detail")]
    public class Invoice_DetailController : BaseApiController
    {
        private readonly IInvoice_DetailService _invoice_detailService;
        public Invoice_DetailController(IInvoice_DetailService invoice_detailService)
        {
            _invoice_detailService = invoice_detailService;
        }

        [HttpPost]
        [Route("GetAllInvoice_Detail")]
        public List<Invoice_DetailDto> GetAllInvoice_Detail([FromBody] Invoice_DetailRequest request)
        {
            return _invoice_detailService.GetAllInvoice_Detail(request);
        }

    }
}