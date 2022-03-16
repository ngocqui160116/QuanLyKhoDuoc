using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.Output
{
    public class OutputRequest : BaseRequest
    {
        public string Id { get; set; }
        public int IdStaff { get; set; }
        public DateTime DateOutput { get; set; }
    }
}
