using Falcon.Web.Core.Helpers;
using Phoenix.Server.Data.Entity;
using Phoenix.Server.Services.Database;
using Phoenix.Shared.Invoice_Detail;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Server.Services.MainServices
{
    public interface IInvoice_DetailService
    {
        List<Invoice_DetailDto> GetAllInvoice_Detail(Invoice_DetailRequest request);
    }
    public class Invoice_DetailService : IInvoice_DetailService
    {
        private readonly DataContext _dataContext;
        public Invoice_DetailService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //lấy danh sách nhà cung cấp
        public List<Invoice_DetailDto> GetAllInvoice_Detail(Invoice_DetailRequest request)
        {
            //setup query
            var query = _dataContext.Invoice_Details.AsQueryable().Where(r => !r.Deleted);

            if (!string.IsNullOrEmpty(request.IdInvoice))
            {
                query = query.Where(d => d.IdInvoice.Contains(request.IdInvoice));
            }
            //if (!string.IsNullOrEmpty(request.IdMedicine.ToString()))
            //{
            //    query = query.Where(d => d.IdMedicine.ToString().Contains(request.IdMedicine.ToString()));
            //}
            //if (!string.IsNullOrEmpty(request.Unit.ToString()))
            //{
            //    query = query.Where(d => d.Unit.ToString().Contains(request.Unit.ToString()));
            //}


            var data = query.ToList();
            return data.MapTo<Invoice_DetailDto>();
        }
    }
}
