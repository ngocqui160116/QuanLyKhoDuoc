using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.Staff
{
    public class StaffRequest : BaseRequest
    {
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Authority { get; set; }
    }
}
