using Phoenix.Framework.Core;
using Phoenix.Mobile.Core.Framework;
using Phoenix.Shared.Invoice_Detail;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Proxies.Common
{
    public interface IInvoice_DetailProxy
    {
        Task<List<Invoice_DetailDto>> GetAllInvoice_Detail(Invoice_DetailRequest request);
    }

    public class Invoice_DetailProxy : BaseProxy, IInvoice_DetailProxy
    {
        public async Task<List<Invoice_DetailDto>> GetAllInvoice_Detail(Invoice_DetailRequest request)
        {
            try
            {
                var api = RestService.For<IInvoice_DetailApi>(GetHttpClient());
                var result = await api.GetAllInvoice_Detail(request);
                if (result == null) return new List<Invoice_DetailDto>();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(new NetworkException(ex), true);
                return new List<Invoice_DetailDto>();
            }
        }
        public interface IInvoice_DetailApi
        {
            [Post("/invoice_Detail/GetAllInvoice_Detail")]
            Task<List<Invoice_DetailDto>> GetAllInvoice_Detail([Body] Invoice_DetailRequest request);

        }
    }
}
