using Phoenix.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Shared.Inventory
{
    public class InventoryRequest : BaseRequest
    {
        public int Id { get; set; }
        public int IdMedicine { get; set; }
        public int Count { get; set; }
        public int LotNumber { get; set; }
        public int IdInputInfo { get; set; }
    }
}
