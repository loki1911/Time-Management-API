using Dapper;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using System.Data;
using TimeMangementSystemAPI.Models;

namespace TimeMangementSystemAPI.Services
{
    public class ExportService : IExportService
    {
        private readonly string _connectionString;

        public ExportService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<MemoryStream> ExportTimeSheetToExcelAsync(string email)
        {
            using var connection = new SqlConnection(_connectionString);

            string query = @"
                SELECT 
                    emp.EmployeeEmailId AS EmployeeEmailID,
                    cli.ClientName AS ClientName,
                    proj.ProjectName AS ProjectName,
                    task.Date AS Date,
                    task.TimeWorked AS WorkedHours
                FROM 
                    [TimeSheets].[dbo].[TaskDetails] task
                JOIN 
                    [TimeSheets].[dbo].[Project] proj ON task.ProjectId = proj.ProjectId
                JOIN 
                    [TimeSheets].[dbo].[Client] cli ON proj.ClientID = cli.ClientId
                JOIN 
                    [TimeSheets].[dbo].[Employee] emp ON task.ClientId = emp.ClientID
                WHERE 
                    emp.EmployeeEmailId = @Email
                ORDER BY 
                    task.Date DESC;";

            var data = await connection.QueryAsync<ExportRecord>(query, new { Email = email });

            if (!data.Any())
                throw new KeyNotFoundException("No records found for the provided email.");

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("TimeSheet Data");
            worksheet.Cells["A1"].LoadFromCollection(data, true);

            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;

            return stream;
        }
    }
}
