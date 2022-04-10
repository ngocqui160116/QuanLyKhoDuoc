using System;
using System.Collections.Generic;
using Phoenix.Shared.Common;
using Phoenix.Shared.Medicine;
using Phoenix.Shared.MedicineItem;

namespace Phoenix.Shared.Stock
{
    public class StockRequest : BaseRequest
    {
        public int Id { get; set; }
        // public string IdInput { get; set; } 
        public int IdStaff { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
    }
}
