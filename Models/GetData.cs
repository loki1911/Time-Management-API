namespace TimeMangementSystemAPI.Models
{
    public class GetData
    {
        public int? ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public int? ClientID { get; set; }
        public string? ProjectDescription { get; set; }

        public string? ProjectManager { get; set; }

       
        public string? EmployeeName { get; set; }

        public int? DepartmentID { get; set; }

        public string? DepartmentName { get; set; }

        public int? ProjectManagerID { get; set; }
    }
}
