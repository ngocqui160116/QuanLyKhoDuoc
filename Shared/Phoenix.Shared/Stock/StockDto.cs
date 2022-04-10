using Phoenix.Shared.InputInfo;
using System;
using System.Collections.Generic;

namespace Phoenix.Shared.Stock
{
    public class StockDto
    {
        public int Id { get; set; }
        // public string IdInput { get; set; } 
        public int IdStaff { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }

    }
}
