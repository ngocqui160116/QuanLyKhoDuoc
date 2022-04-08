using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.Output
{
    public class OutputRequest : BaseRequest
    {
        public int Id { get; set; }
        public int IdStaff { get; set; }
        public DateTime DateOutput { get; set; }
        public int IdReason { get; set; }
        public string Status { get; set; }

    }
}
