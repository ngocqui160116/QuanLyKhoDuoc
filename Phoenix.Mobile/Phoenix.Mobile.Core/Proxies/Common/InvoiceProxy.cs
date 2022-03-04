using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Invoice;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IInvoiceProxy
    {
        Task<List<InvoiceDto>> GetAllInvoice(InvoiceRequest request);
    }

    public class InvoiceProxy : BaseProxy, IInvoiceProxy
    {
        public async Task<List<InvoiceDto>> GetAllInvoice(InvoiceRequest request)
        {
            try
            {
                var api = RestService.For<IInvoiceApi>(GetHttpClient());
                var result = await api.GetAllInvoice(request);
                if (result == null) return new List<InvoiceDto>();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new List<InvoiceDto>();
            }
        }
        public interface IInvoiceApi
        {
            [Post("/invoice/GetAllInvoice")]
            Task<List<InvoiceDto>> GetAllInvoice([Body] InvoiceRequest request);

        }
    }
}
