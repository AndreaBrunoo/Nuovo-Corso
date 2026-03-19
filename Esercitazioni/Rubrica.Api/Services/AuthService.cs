using Microsoft.AspNetCore.Identity;
using Rubrica.Api.Dtos;
using Rubrica.Api.Helpers;
using Rubrica.Api.Models;

namespace Rubrica.Api.Services;

// Questo servizio gestisce la registrazione e il login degli utenti
public class AuthService
{
    // UserManager gestisce la creazione e gestione degli utenti
    private readonly UserManager<ApplicationUser> _userManager;
    
    // SignInManager controlla la password e gestisce il login
    private readonly SignInManager<ApplicationUser> _signInManager;

    // JwtHelper serve per creare il token JWT dopo il login
    private readonly JwtHelper _jwtHelper;

    // Il costruttore riceve i servizi necessari tramite Dependency Injection
    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        JwtHelper jwtHelper)
    {
        _userManager = userManager; 
        _signInManager = signInManager; 
        _jwtHelper = jwtHelper; 
    }

    // Metodo per registrare un nuovo utente
    public async Task<IdentityResult> RegisterAsync(RegisterDto dto)
    {
        // Cerchiamo se la mail esiste già
        ApplicationUser? existingUser = await _userManager.FindByEmailAsync(dto.Email);

        // Se esiste, restituisco un errore
        if (existingUser != null)
        {
            IdentityError error = new IdentityError();
            error.Description = "Email già registrata.";

            return IdentityResult.Failed(error);
        }

        // Creo un nuovo oggetto utente
        ApplicationUser user = new ApplicationUser();
        user.UserName = dto.Email;
        user.Email = dto.Email;
        user.NomeCompleto = dto.NomeCompleto;
        user.PhoneNumber = dto.PhoneNumber;
        user.CreatedAt = DateTime.UtcNow;
        user.Preferiti = false;
        user.DataDiNascita = dto.DataDiNascita;

        user.Eta = TimeIntelligence.CalcolaEta(dto.DataDiNascita);

        // Identity salva l'utente e crea l'hash sicuro della password
        IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

        // Restituisco il risultato (successo o errori)
        return result;
    }

    // Metodo per effettuare il login
    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        // Cerchiamo l'utente con la mail
        ApplicationUser? user = await _userManager.FindByEmailAsync(dto.Email);

        // Se non esiste, login fallito
        if (user == null)
        {
            return null;
        }

        // Controlliamo se la password è corretta
        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

        // Se la password è sbagliata, login fallito
        if (!result.Succeeded)
        {
            return null;
        }

        // Genero il token JWT per l'utente
        string token = _jwtHelper.GenerateToken(user);

        // creo la risposta 
        AuthResponseDto response = new AuthResponseDto();
        response.Token = token;
        response.UserId = user.Id;
        response.Email = user.Email ?? string.Empty;
        response.NomeCompleto = user.NomeCompleto;

        // Restituisco i dati del login
        return response;
    }

// Metodo per ottenere i dati di un utente tramite il suo ID
public async Task<UserProfileDto?> GetUserByIdAsync(string userId) 
    {
        Console.WriteLine("Sono nella funzione GetUserByIdAsync");

        // Cerchiamo l'utente tramite l'ID
        ApplicationUser? user = await _userManager.FindByIdAsync(userId);

        // Se l'utente non esiste, restituiamo null
        if (user == null)
        {
            return null;
        }

        // Creiamo il DTO da restituire con i dati dell'utente
        UserProfileDto dto = new UserProfileDto();
        dto.UserId         = user.Id;
        dto.NomeCompleto   = user.NomeCompleto;
        dto.Email          = user.Email ?? string.Empty;
        dto.PhoneNumber    = user.PhoneNumber;
        dto.Preferiti      = user.Preferiti;
        dto.DataDiNascita  = user.DataDiNascita;

        // Restituiamo i dati del profilo
        return dto;
    }

    // Metodo per aggiornare i dati di un utente
    public async Task<IdentityResult> UpdateAsync(UpdateUserDto dto, string userId) 
    {
        // Cerchiamo l'utente tramite ID
        ApplicationUser? user = await _userManager.FindByIdAsync(userId);
        
        // Se l'utente non esiste, restituiamo un errore
        if (user == null)
        {
            IdentityError error = new IdentityError();
            return IdentityResult.Failed(error);
        }

        // Aggiorniamo i campi modificabili
        user.NomeCompleto     = dto.NomeCompleto;
        user.PhoneNumber      = dto.PhoneNumber;
        user.Preferiti        = dto.Preferiti;

        // Salviamo le modifiche nel database
        IdentityResult result = await _userManager.UpdateAsync(user); 

        // Restituiamo il risultato dell'operazione
        return result;
    }

    // Metodo per eliminare un utente tramite il suo ID
    public async Task<IdentityResult> DeleteAsync(string userId) 
    {
        // Cerchiamo l'utente tramite ID
        ApplicationUser? user = await _userManager.FindByIdAsync(userId);

        // Se l'utente non esiste, restituiamo un errore
        if (user == null)
        {
            IdentityError error = new IdentityError();
            return IdentityResult.Failed(error);
        }

        // Eliminazione dell'utente dal database
        IdentityResult result = await _userManager.DeleteAsync(user);

        // Restituiamo il risultato dell'operazione
        return result;
    }
}