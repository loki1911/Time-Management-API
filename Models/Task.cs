namespace TimeMangementSystemAPI.Models
{
    public class Task
    {
        public int? ProjectId { get; set; }
        public int? ClientId { get; set; }
        public string? TaskName { get; set; }
        public string? BillingType { get; set; }
        public DateTime? Date { get; set; }
        public string? TimeWorked { get; set; }
        public string? Description { get; set; }
    }
}
