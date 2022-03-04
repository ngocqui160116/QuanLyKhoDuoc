using System;

namespace Phoenix.Shared.Invoice
{
    public class InvoiceRequest
    {
        public string IdInvoice { get; set; }
        public int IdStaff { get; set; }
        public int IdCustomer { get; set; }
        public bool Deleted { get; set; }
    }
}
