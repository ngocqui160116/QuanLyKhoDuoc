using Phoenix.Shared.StockInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Shared.Stock
{
    public class StockDtoWeb
    {
        public int Id { get; set; }
        public int IdStaff { get; set; }
        public DateTime Date { get; set; }
        public string Note{ get; set; }

        //Mapper
        public string StaffName { get; set; }

        //List
        //public List<StockInfoDto> StockInfo { get; set; }
    }
}
