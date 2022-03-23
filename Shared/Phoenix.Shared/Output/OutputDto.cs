using Phoenix.Shared.OutputInfo;
using System;
using System.Collections.Generic;

namespace Phoenix.Shared.Output
{
    public class OutputDto
    {
        
        public string Id { get; set; }
        public int IdStaff { get; set; }
        public DateTime DateOutput { get; set; }
        //Add
        public string NameStaff { get; set; }
        public List<OutputInfoDto> OutputInfo { get; set; }
    }
}
