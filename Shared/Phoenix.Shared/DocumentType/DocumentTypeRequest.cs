using Phoenix.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Shared.DocumentType
{
 
    public class DocumentTypeRequest : BaseRequest
    {
        public string Name { get; set; }
    }
}
