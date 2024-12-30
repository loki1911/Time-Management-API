using Microsoft.AspNetCore.Mvc;
using TimeMangementSystemAPI.DTOs;
using TimeMangementSystemAPI.Models;
using TimeMangementSystemAPI.Services;

namespace TimeMangementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmployeeDto>> GetEmployees()
        {
            return Ok(_employeeService.GetAllEmployees());
        }

        [HttpGet("{emailId}")]
        public ActionResult<EmployeeDto> GetEmployee(string emailId)
        {
            var employee = _employeeService.GetEmployeeByEmail(emailId);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost("SaveEmployee")]
        public IActionResult SaveTaskDetails([FromBody] EmployeeData employeeData)
        {
            if (employeeData == null)
            {
                return BadRequest("Invalid Employee details.");
            }

            var taskId = _employeeService.SaveEmployee(employeeData);
            return CreatedAtAction(nameof(SaveTaskDetails), new { id = taskId });
        }

        [HttpPut("update")]
        public ActionResult UpdateEmployee([FromBody] EmployeeData employeeData)
        {
           
            int rowsAffected = _employeeService.UpdateEmployee(employeeData);

            if (rowsAffected > 0)
            {
                return Ok("Employee updated successfully.");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
