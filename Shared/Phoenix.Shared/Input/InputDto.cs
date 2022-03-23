using Phoenix.Shared.InputInfo;
using System;
using System.Collections.Generic;

namespace Phoenix.Shared.Input
{
    public class InputDto
    {
        public string Id { get; set; }
        public DateTime DateInput { get; set; }

        public int IdStaff { get; set; }
        public int IdSupplier { get; set; }
        //Add
        public string NameStaff { get; set; }
        public string SupplierName { get; set; }


        public List<InputInfoDto> InputInfo { get; set; }

    }
}
