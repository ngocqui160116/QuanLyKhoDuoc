﻿using Phoenix.Shared.OutputInfo;
using System;
using System.Collections.Generic;

namespace Phoenix.Shared.Output
{
    public class OutputDto
    {
        
        public string Id { get; set; }
        public int IdStaff { get; set; }
        public DateTime DateOutput { get; set; }
        public int IdReason { get; set; }
        public bool Status { get; set; }
        //Add
        public string NameStaff { get; set; }
        public string NameReason { get; set; }

        //List
        public List<OutputInfoDto> OutputInfo { get; set; }
    }
}
