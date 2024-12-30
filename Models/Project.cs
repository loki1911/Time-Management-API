namespace TimeMangementSystemAPI.Models
{
    public class Project
    {
          public int? ProjectId { get; set; }
        public string? ProjectName { get; set; }

        public int? ClientID { get; set; }
        public string? ProjectDescription { get; set; }

        public string? ProjectManager { get; set; }
    }
}
