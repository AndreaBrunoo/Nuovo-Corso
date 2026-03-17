using Microsoft.AspNetCore.Mvc;
using Rubrica.Api.Dtos;
using Rubrica.Api.Services;

namespace Rubrica.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // definisce il percorso dell'api.
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        // dependency injection: le dipendenze(i services) vengono fornite in modo automatico.
        _authService = authService;
    }

    [HttpPost("register")] // mappa l'url di "register" ad un metodo di AuthService. Qui Register definisce l'endpoint
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)  // IActionResult è una classe di Identity. Frombody significa che riceve il json e lo converte in ciò che il dto farà vedere
    {
        var result = await _authService.RegisterAsync(dto);

        if (!result.Succeeded)
        {
            List<string> errors = new List<string>();

            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }

            return BadRequest(errors); // metodo ereditato da ControllerBase per la gestione di una specifica tipologia di errore. Ritorno se la richiesta non ha successo.
        }

        return Ok(new { message = "Registrazione completata." }); // Ritorno se la richiesta ha successo.
    }

    [HttpPost("login")] // mappa l'url di "login" ad un metodo.
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        AuthResponseDto? response = await _authService.LoginAsync(dto);

        if (response == null)
        {
            return Unauthorized(new { message = "Email o password non validi." });
        }

        return Ok(response); // ritorno se il login ha successo. 
    }
}