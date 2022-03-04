using Phoenix.Mobile.Core.Models.Invoice_Detail;
using Phoenix.Mobile.Core.Proxies.Common;
using Phoenix.Mobile.Core.Utils;
using Phoenix.Shared.Invoice_Detail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Services.Common
{
    public interface IInvoice_DetailService
    {
        Task<List<Invoice_DetailModel>> GetAllInvoice_Detail(Invoice_DetailRequest request);
    }

    public class Invoice_DetailService : IInvoice_DetailService
    {
        private readonly IInvoice_DetailProxy _Invoice_DetailProxy;
        public Invoice_DetailService(IInvoice_DetailProxy Invoice_DetailProxy)
        {
            _Invoice_DetailProxy = Invoice_DetailProxy;
        }
        public async Task<List<Invoice_DetailModel>> GetAllInvoice_Detail(Invoice_DetailRequest request)
        {
            var data = await _Invoice_DetailProxy.GetAllInvoice_Detail(request);
            return data.MapTo<Invoice_DetailModel>();
        }
    }
}
