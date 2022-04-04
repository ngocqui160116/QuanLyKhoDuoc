using System;
using Phoenix.Shared.Common;

namespace Phoenix.Shared.Input
{
    public class InputRequest : BaseRequest
    {
        public int Id { get; set; }
      //  public string IdInput { get; set; }
        public int IdStaff { get; set; }
        public int IdSupplier { get; set; }
        public DateTime DateInput { get; set; }
        public bool Status { get; set; }
    }
}
