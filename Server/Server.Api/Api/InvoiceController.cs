using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.MainServices;
using Phoenix.Server.Services.MainServices.Auth;
using Phoenix.Shared.Core;
using Phoenix.Shared.Invoice;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Phoenix.Server.Api.Api
{
    [RoutePrefix("api/invoice")]
    public class InvoiceController : BaseApiController
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost]
        [Route("GetAllInvoice")]
        public List<InvoiceDto> GetAllInvoice([FromBody] InvoiceRequest request)
        {
            return _invoiceService.GetAllInvoice(request);
        }

    }
}