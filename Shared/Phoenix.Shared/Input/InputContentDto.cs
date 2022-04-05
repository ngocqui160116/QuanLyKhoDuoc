using Phoenix.Shared.InputInfo;
using System;
using System.Collections.Generic;

namespace Phoenix.Shared.Input
{
    public class InputContentDto
    {
        public int medicineId { get; set; }
        public string medicineName { get; set; }
        public int Batch { get; set; }
        public int? Count { get; set; }
        public double? InputPrice { get; set; }
        public double? Total { get; set; }
        public DateTime? DueDate { get; set; }

    }
}
