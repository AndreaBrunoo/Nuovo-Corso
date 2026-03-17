using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Rubrica.Api.Models;

namespace Rubrica.Api.Helpers;

public class JwtHelper
{
    private readonly IConfiguration _configuration;

    public JwtHelper(IConfiguration configuration)
    {
        // Salviamo la configurazione (serve per leggere appsettings.json)
        _configuration = configuration;
    }

    public string GenerateToken(ApplicationUser user)
    {
        // Leggiamo i valori dal file appsettings.json
        // Sono necessari per creare il token
        string? key = _configuration["Jwt:Key"];         // Chiave segreta
        string? issuer = _configuration["Jwt:Issuer"];   // Chi crea il token
        string? audience = _configuration["Jwt:Audience"]; // Chi può usarlo

        // Se manca qualcosa → errore
        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
        {
            throw new Exception("Configurazione JWT mancante.");
        }

        // Dentro il token mettiamo alcune informazioni sull'utente
        // Queste informazioni si chiamano "claims"
        Claim[] claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),          // Id dell'utente
            new Claim(ClaimTypes.Name, user.UserName ?? ""),        // Username
            new Claim(ClaimTypes.Email, user.Email ?? "")           // Email
        };

        // Convertiamo la chiave segreta in byte
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        // Diciamo che useremo l'algoritmo HmacSha256 per firmare il token
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Creiamo il token vero e proprio
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: issuer,          // Chi ha creato il token
            audience: audience,      // Chi può usarlo
            claims: claims,          // Informazioni sull'utente
            expires: DateTime.UtcNow.AddHours(1), // Scadenza (1 ora)
            signingCredentials: credentials        // Firma digitale
        );

        // Convertiamo il token in una stringa leggibile dal client
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
