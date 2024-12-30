using Microsoft.AspNetCore.Mvc;
using TimeMangementSystemAPI.Models;
using TimeMangementSystemAPI.Services;

namespace TimeMangementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserdetailsController : ControllerBase
    {
        public readonly IUserDetails userDetails;

        public UserdetailsController(IUserDetails userDetails)
        {
            this.userDetails = userDetails;
        }


        [HttpGet("tasks/{employeeEmailId}")]
        public IActionResult GetEmployeeTasks(string employeeEmailId)
        {
            if (string.IsNullOrEmpty(employeeEmailId))
            {
                return BadRequest("Employee email ID cannot be empty.");
            }

            List<UserDetailsDTO> tasks = userDetails.GetEmployeeTasksByEmail(employeeEmailId);

            if (tasks == null || tasks.Count == 0)
            {
                return NotFound("No tasks found for the given employee.");
            }

            return Ok(tasks);
        }
    }
}
