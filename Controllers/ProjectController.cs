using Microsoft.AspNetCore.Mvc;
using TimeMangementSystemAPI.Models;
using TimeMangementSystemAPI.Services;

namespace TimeMangementSystemAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly projectInterface _projectService;


        public ProjectController(projectInterface projectService)
        {
            _projectService = projectService;
        }

        //[HttpGet("ProjectName")]

        //public async Task<IActionResult> Getalldata()
        //{
        //    var user = await _projectService.GetProjectsAsync();
        //    return Ok(user);


        //}
        [HttpGet("ClientByProjectId/{projectId}")]
        public async Task<IActionResult> GetClientByProjectId(int projectId)
        {
            var client = await _projectService.GetClientByProjectIdAsync(projectId);
            if (client == null)
            {
                return NotFound("Client not found for selected project");
            }
            return Ok(client);
        }

        [HttpGet("department-name")]
        public async Task<IActionResult> GetDepartmentNameByProjectId(int projectId)
        {

            var users = await _projectService.GetDepartmentNameByProjectIdAsync(projectId);
            return Ok(users);


        }

        [HttpGet("ManagersByDepartment/{DepartmentID}")]
        public async Task<IActionResult> GetManagersByDepartment(int DepartmentID)
        {
            var managers = await _projectService.GetManagersByDepartmentAsync(DepartmentID);

            if (managers == null || !managers.Any())
            {
                return NotFound("No managers found for the specified department.");
            }

            return Ok(managers);
        }
        [HttpGet("GetProjectData")]

        public async Task<ActionResult<IEnumerable<GetData>>> GetAllProjectDetails()
        {
            var users = await _projectService.GetProjectData();
            return Ok(users);
        }

        [HttpPost("SaveProject")]
        public IActionResult SaveallProject([FromBody] Projects projects)
        {
            if (projects == null)
            {
                return BadRequest("Invalid Employee data");
            }
            var projectId = _projectService.SaveEmployee(projects);
            return CreatedAtAction(nameof(SaveallProject), new { id = projectId });
        }
        [HttpPut("UpdateProject")]

        public ActionResult GetUpdateProject([FromBody] ProjectData projectData)
        {
            if (projectData == null)
            {
                return BadRequest("Invalid Employee data.");
            }

            int rowAffected = _projectService.UpdateProject(projectData);

            if (rowAffected > 0)
            {
                return Ok("Employee update Successfully");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
