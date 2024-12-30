namespace TimeMangementSystemAPI.Models
{
    public class ExportRecord
    {
        public string EmployeeEmailID { get; set; }
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public DateTime Date { get; set; }
        public string WorkedHours { get; set; }
    }
}
