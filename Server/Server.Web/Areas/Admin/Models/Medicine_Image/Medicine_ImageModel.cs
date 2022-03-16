﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phoenix.Server.Web.Areas.Admin.Models.InputInfo
{
    public class Medicine_ImageModel
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