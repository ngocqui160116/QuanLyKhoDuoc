using System;

namespace Phoenix.Shared.Staff
{
    public class StaffDto
    {
        public int IdStaff { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Authority { get; set; }
        public bool Deleted { get; set; }
    }
}
