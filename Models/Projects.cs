﻿namespace TimeMangementSystemAPI.Models
{
    public class Projects
    {
        public string? ProjectName { get; set; }

        public string? ProjectDescription { get; set; }

        public int? ClientId { get; set; }

        public int? DepartmentID { get; set; }

        public int? ProjectManagerID { get; set; }
    }
}
