using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeMangementSystemAPI.Models;
using TimeMangementSystemAPI.Services;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly Aeth _aethService;

    public AuthController(AuthService authService, Aeth aethService)
    {
        _authService = authService;
        _aethService = aethService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Employee employee)
    {
        try
        {
            var registeredEmployee = await _authService.RegisterAsync(employee);
            return Ok(registeredEmployee);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] EmployeeLogin login)
    {
        try
        {
            var token = _aethService.AuthenticateAsync(login);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid username or password.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error.");
        }
    }
}

