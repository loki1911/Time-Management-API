namespace TimeMangementSystemAPI.Models
{
    public class ProjecteUpdate
    {
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public string? ProjectDepartment { get; set; }
        public string? ProjectManager { get; set; }
        public string? ClientName { get; set; }
        public int? ProjectManagerID { get; set; }
        public string? DepartmentName { get; set; }

        public string? EmployeeName { get; set; }
    }
}
