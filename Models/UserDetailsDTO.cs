namespace TimeMangementSystemAPI.Models
{
    public class UserDetailsDTO
    {
        public string EmployeeEmailId { get; set; }
        public int TaskId { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public string BillingName { get; set; }
        public string TaskDescription { get; set; }
        public string TimeWorked { get; set; }
        public DateTime Date { get; set; }
    }
}
