using Phoenix.Shared.Common;
using System;
using System.Collections.Generic;

namespace Phoenix.Shared.Output
{
    public class OutputRequest : BaseRequest
    {
        public int Id { get; set; }
        public int IdStaff { get; set; }
        public DateTime DateOutput { get; set; }
        public int IdReason { get; set; }
        public string Status { get; set; }

        public List<OutputContentDto> List { get; set; }

    }
}
