using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace TimeMangementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskDetailsController : ControllerBase
    {
        private readonly string connectionstring;

        public TaskDetailsController(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet("GetWorkDetailsByEmail")]
        public async Task<IActionResult> GetWorkDetailsByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email ID is required.");
            }

            var query = @"
                SELECT 
                   
                    TD.Date,
                    TD.TimeWorked
                FROM 
                    [TimeSheets].[dbo].[Employee] AS E
                JOIN 
                    [TimeSheets].[dbo].[TaskDetails] AS TD
                ON 
                    E.EmployeeID = TD.ClientId -- Adjust the relationship key if needed
                WHERE 
                    E.EmployeeEmailId = @Email";

            try
            {
                using (var connection = new SqlConnection(connectionstring))
                {
                    var result = await connection.QueryAsync<dynamic>(query, new { Email = email });
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
