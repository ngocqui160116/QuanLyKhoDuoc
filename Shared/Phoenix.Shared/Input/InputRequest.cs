using System;
using Phoenix.Shared.Common;

namespace Phoenix.Shared.Input
{
    public class InputRequest : BaseRequest
    {
        public string Id { get; set; }
        public int IdStaff { get; set; }
        public int IdSupplier { get; set; }
        public DateTime DateInput { get; set; }
    }
}
