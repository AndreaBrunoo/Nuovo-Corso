using Microsoft.AspNetCore.Identity;
using ArchetipoWebAPI.Dtos;
using ArchetipoWebAPI.Helpers;
using ArchetipoWebAPI.Models;

namespace ArchetipoWebAPI.Services;

public class AuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtHelper _jwtHelper;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtHelper jwtHelper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtHelper = jwtHelper;
    }

    /*questo è un metodo asincrono che restituisce un IdentityResult, che indica se la registrazione è riuscita o no, e contiene eventuali errori eun metodo asicrono che è un metodo che può essere
     eseguito in modo non bloccante cioè puo fare operazioni che richiedono tempo senza bloccare il thread principale dell'applicazione
    */

    public async Task<IdentityResult> RegisterAsync(RegisterDto dto)
    {
        // controlliamo se esiste già un utente con questa email (await fa restare in attesa il thread finchè l'operazione non è completa)
        ApplicationUser? existingUser = await _userManager.FindByEmailAsync(dto.Email);

        if (existingUser != null)
        {
            IdentityError error = new IdentityError();
            error.Description = "Email già registrata.";

            List<IdentityError> errors = new List<IdentityError>();
            errors.Add(error);
        }

        // creiamo il nuovo utente
        ApplicationUser user = new ApplicationUser();
        user.UserName = dto.Email; // usiamo la mail anche come username
        user.Email = dto.Email;
        user.NomeCompleto = dto.NomeCompleto;
        user.PhoneNumber = dto.PhoneNumber;
        user.CreatedAt = DateTime.UtcNow;

        // Identity salva l'utente e crea l'has sicuro della password
        IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

        return result;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        // cerchiamo l'utente per email
        ApplicationUser? user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
        {
            return null;
        }

        // controlliamo se la paswword è giusta
        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

        if (!result.Succeeded)
        {
            return null;
        }

        // se tutto va bene creiamo il token
        string token = _jwtHelper.GenerateToken(user);

        AuthResponseDto response = new AuthResponseDto();
        response.Token = token;
        response.UserId = user.Id;
        response.Email = user.Email ?? "";
        response.NomeCompleto = user.NomeCompleto;
        return response;
    }
}