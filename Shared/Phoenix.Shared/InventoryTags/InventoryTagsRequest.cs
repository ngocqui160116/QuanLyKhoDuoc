using Phoenix.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Shared.InventoryTags
{
    public class InventoryTagsRequest : BaseRequest
    {
        public int Id { get; set; }
        public string DocumentId { get; set; }
        public DateTime DocumentDate { get; set; }
        public int LotNumber { get; set; }
    }
}
