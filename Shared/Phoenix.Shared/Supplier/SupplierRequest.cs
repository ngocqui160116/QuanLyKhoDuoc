using System;

namespace Phoenix.Shared.Supplier
{
    public class SupplierRequest
    {
        public int IdSupplier { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }
}
