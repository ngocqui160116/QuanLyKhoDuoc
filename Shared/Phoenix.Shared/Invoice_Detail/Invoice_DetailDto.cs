using System;

namespace Phoenix.Shared.Invoice_Detail
{
    public class Invoice_DetailDto
    {
        public string IdInvoice { get; set; }
        public int IdMedicine { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public int Unit { get; set; }
        public bool Deleted { get; set; }
    }
}
