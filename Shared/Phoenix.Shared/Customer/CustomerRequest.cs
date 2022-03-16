using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.Customer
{
    public class CustomerRequest : BaseRequest
    {
        public int IdCustomer { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool Deleted { get; set; }
    }
}
