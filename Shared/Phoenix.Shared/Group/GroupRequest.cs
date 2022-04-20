using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.Group
{
    public class GroupRequest : BaseRequest
    {
       public int IdGroup { get; set; }
        public string Name { get; set; }
       // public string GroupName { get; set; }
    }
}
