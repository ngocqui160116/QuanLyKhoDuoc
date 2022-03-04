using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Invoice;
using Phoenix.Shared.Vendor;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Server.Services.MainServices
{
    public interface IInvoiceService
    {
        List<InvoiceDto> GetAllInvoice(InvoiceRequest request);
    }
    public class InvoiceService : IInvoiceService
    {
        private readonly DataContext _dataContext;
        public InvoiceService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public List<InvoiceDto> GetAllInvoice(InvoiceRequest request)
        {
            //setup query
            var query = _dataContext.Invoices.AsQueryable().Where(r => !r.Deleted);
            if (!string.IsNullOrEmpty(request.IdInvoice))
            {
                query = query.Where(d => d.IdInvoice.Contains(request.IdInvoice));
            }
            //if (!string.IsNullOrEmpty(request.IdStaff.ToString()))
            //{
            //    query = query.Where(d => d.IdStaff.ToString().Contains(request.IdStaff.ToString()));
            //}
            //if (!string.IsNullOrEmpty(request.IdCustomer.ToString()))
            //{
            //    query = query.Where(d => d.IdCustomer.ToString().Contains(request.IdCustomer.ToString()));
            //}

            var data = query.ToList();
            return data.MapTo<InvoiceDto>();
        }
    }
}
