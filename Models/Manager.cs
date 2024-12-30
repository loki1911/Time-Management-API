namespace TimeMangementSystemAPI.Models
{
    public class Manager
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmailId { get; set; }

        public int? DepartmentID { get; set; }
    }
}
