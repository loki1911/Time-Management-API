using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeMangementSystemAPI.Models;
using TimeMangementSystemAPI.Services;

namespace TimeMangementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly Clientservice _client;
        public ClientController(Clientservice client)
        {
            _client = client;

        }
        [Authorize]
        [HttpGet("get-data")]
        public async Task<ActionResult<IEnumerable<Client>>> getdata()
        {
            var users = await _client.getClientdata();
            return Ok(users);
        }
        [HttpGet("get-roledata")]
        public async Task<ActionResult<IEnumerable<Role>>> getRoles()
        {
            var users = await _client.getRoleData();
            return Ok(users);
        }

        [HttpGet("get-managerdata")]
        public async Task<ActionResult<IEnumerable<ManagerData>>> getManagers()
        {
            var users = await _client.getManagerData();
            return Ok(users);
        }
        [HttpGet("get-deptdata")]
        public async Task<ActionResult<IEnumerable<Department>>> getDepartments()
        {
            var users = await _client.getDepartmentData();
            return Ok(users);
        }


        [HttpPost("insert-data")]
        public async Task<ActionResult<int>> Insertdata([FromBody] Client client)
        {
            var users = await _client.insertclient(client);
            return Ok(client);
        }

        [HttpPut("Edit-data")]

        public async Task<ActionResult<bool>> editdata([FromBody] Client client)
        {
            var users = await _client.editclient(client);
            return Ok(client);

        }
        [HttpGet("BillingType/{ProjectId}")]
        public async Task<IActionResult> GettingBillingtype(int ProjectId)
        {
            var taskName = await _client.GetBillingtypeByProjectIdAsync(ProjectId);
            return Ok(taskName);
        }


    }
}

