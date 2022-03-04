using System;

namespace Phoenix.Shared.Invoice_Detail
{
    public class Invoice_DetailRequest
    {
        public string IdInvoice { get; set; }
        public int IdMedicine { get; set; }
        public int Unit { get; set; }
        public bool Deleted { get; set; }
    }
}
