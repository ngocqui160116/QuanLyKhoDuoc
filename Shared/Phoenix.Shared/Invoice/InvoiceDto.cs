using System;

namespace Phoenix.Shared.Invoice
{
    public class InvoiceDto
    {
        
        public string IdInvoice { get; set; }
        public DateTime Date { get; set; }
        public int IdStaff { get; set; }
        public int IdCustomer { get; set; }
        public double Total { get; set; }
        public bool Deleted { get; set; }
    }
}
