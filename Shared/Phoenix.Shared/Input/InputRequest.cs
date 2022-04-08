using System;
using System.Collections.Generic;
using Phoenix.Shared.Common;
using Phoenix.Shared.Medicine;
using Phoenix.Shared.MedicineItem;

namespace Phoenix.Shared.Input
{
    public class InputRequest : BaseRequest
    {
        public int Id { get; set; }
      //  public string IdInput { get; set; }
        public int IdStaff { get; set; }
        public int IdSupplier { get; set; }
        public DateTime DateInput { get; set; }
        public string Status { get; set; }

        //inputinfo
        public int IdInput { get; set; }
        public int IdMedicine { get; set; }
        public int IdBatch { get; set; }
        public int? Count { get; set; }
        public double? InputPrice { get; set; }
        public double? Total { get; set; }
        public DateTime? DueDate { get; set; }
        
        public List<InputContentDto> List { get; set; }
        public List<MedicineItemDto> ListMedicine { get; set; }
    }
}
