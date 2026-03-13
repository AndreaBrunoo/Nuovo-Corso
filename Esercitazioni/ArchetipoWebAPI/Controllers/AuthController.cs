using Microsoft.AspNetCore.Mvc;
using ArchetipoWebAPI.Dtos;
using ArchetipoWebAPI.Services;

namespace ArchetipoWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);

        if (!result.Succeeded)
        {
            List<string> errors = new List<string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }

            return BadRequest(errors);
        }

        return Ok(new { message = "Registrazione completata" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> login([FromBody] LoginDto dto)
    {
        AuthResponseDto? response = await _authService.LoginAsync(dto);

        if (response == null)
        {
            return Unauthorized(new { message = "Email o password non validi." });
        }

        return Ok(response);
    }
}