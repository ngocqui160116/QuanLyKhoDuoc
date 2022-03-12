using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.Unit
{
    public class UnitRequest : BaseRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
