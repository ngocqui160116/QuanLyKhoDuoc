using System;

namespace Phoenix.Shared.Customer
{
    public class CustomerRequest
    {
        public int IdCustomer { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }
}
