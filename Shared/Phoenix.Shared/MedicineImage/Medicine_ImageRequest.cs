using Phoenix.Shared.Common;
using System;

namespace Phoenix.Shared.Medicine
{
    public class Medicine_ImageRequest : BaseRequest
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string RelativePath { get; set; }
        public string AbsolutePath { get; set; }
        public bool IsExternal { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsUsed { get; set; }
        public bool Deleted { get; set; }
    }
}
