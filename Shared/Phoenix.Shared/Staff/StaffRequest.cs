﻿using System;

namespace Phoenix.Shared.Staff
{
    public class StaffRequest
    {
        public int IdStaff { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Authority { get; set; }
        public bool Deleted { get; set; }
    }
}