

namespace Phoenix.Shared.OutputInfo
{
    public class OutputInfoDto
    {
        public int Id { get; set; }
        public string IdOutput { get; set; }
        public int IdMedicine { get; set; }
        public int IdInputInfo { get; set; }
        public int IdReason { get; set; }
        public int Count { get; set; }
        public double Total { get; set; }
        public string Status { get; set; }
        //add
        public string MedicineName { get; set; }
        public string Reason { get; set; }
    }
}
