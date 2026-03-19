using Microsoft.AspNetCore.Mvc;
using Rubrica.Api.Dtos;
using Rubrica.Api.Services;
using System.Security.Claims;

namespace Rubrica.Api.Controllers;

[ApiController]
/* [controller] indica che viene cancellata la parola controller dal nome del file quindi quando verrà richiamato
risulterà localhost/api/Auth/MetodoDaUtilizzare (i metodi sono quelli preceduti da un http. Esempio: [http...("-> <-")]) */
[Route("api/[controller]")] // definisce il percorso dell'api.
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        // dependency injection: le dipendenze(i services) vengono fornite in modo automatico.
        _authService = authService;
    }

    // mappa l'url di "register" ad un metodo di AuthService. Qui Register definisce l'endpoint
    [HttpPost("register")] 

    // IActionResult è una classe di Identity. Frombody significa che riceve il json e lo converte in ciò che il dto farà vedere
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

        // metodo ereditato da ControllerBase per la gestione di una specifica tipologia di errore. Ritorno se la richiesta non ha successo.
            return BadRequest(errors); 
        }

        // Ritorno se la richiesta ha successo.
        return Ok(new { message = "Registrazione completata." }); 
    }

    // mappa l'url di "login" ad un metodo.
    [HttpPost("login")] 
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        AuthResponseDto? response = await _authService.LoginAsync(dto);

        if (response == null)
        {
            return Unauthorized(new { message = "Email o password non validi." });
        }

        return Ok(response); // ritorno se il login ha successo. 
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetUserById()
    {
        string userId = GetUserIdFromToken();

        UserProfileDto? dto = await _authService.GetUserByIdAsync(userId);

        if (dto == null)
        {
            return NotFound(new { message = "Utente non trovato" });
        }
        
        return Ok(dto);
    }

    [HttpPut("update")]

    public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
    {
        string userId = GetUserIdFromToken();

        var result = await _authService.UpdateAsync(dto, userId);
        if (result == null)
        {
            return NotFound(new { message = "Utente non trovato" });
        }

        return Ok(result);
    }

    [HttpDelete("delete")]

    public async Task<IActionResult> Delete()
    {
        string userId = GetUserIdFromToken();

        var result = await _authService.DeleteAsync(userId);

        if (result == null)
        {
            return NotFound(new { message = "Utente non trovato." });
        }

        return Ok(result);
    }

    private string GetUserIdFromToken()
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            throw new Exception("UserId non trovato nel token");
        }

        return userId;
    }
}