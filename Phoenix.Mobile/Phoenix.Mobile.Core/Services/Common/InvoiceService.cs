using Phoenix.Mobile.Core.Models.Invoice;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Invoice;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IInvoiceService
    {
        Task<List<InvoiceModel>> GetAllInvoice(InvoiceRequest request);
    }

    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceProxy _InvoiceProxy;
        public InvoiceService(IInvoiceProxy InvoiceProxy)
        {
            _InvoiceProxy = InvoiceProxy;
        }
        public async Task<List<InvoiceModel>> GetAllInvoice(InvoiceRequest request)
        {
            var data = await _InvoiceProxy.GetAllInvoice(request);
            return data.MapTo<InvoiceModel>();
        }
    }
}
