using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Rubrica.Api.Models;

namespace Rubrica.Api.Helpers;

// si occupa di generare i token jwt per gli utenti autenticati.
public class JwtHelper
{
     // leggere e gestire i valori di configurazione dell'applicazione.
    private readonly IConfiguration _configuration;

    public JwtHelper(IConfiguration configuration)
    {
        // Salviamo la configurazione (serve per leggere appsettings.json)
        _configuration = configuration;
    }

    public string GenerateToken(ApplicationUser user, IList<string> roles)
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
        List<Claim> claims = new List<Claim>();
            claims.Add(new Claim (ClaimTypes.NameIdentifier, user.Id));          // Id dell'utente
            claims.Add(new Claim (ClaimTypes.Name, user.UserName ?? ""));        // Username
            claims.Add(new Claim (ClaimTypes.Email, user.Email ?? ""));           // Email
        for(int i = 0; i < roles.Count; i++)
        {
            claims.Add(new Claim(ClaimTypes.Role, roles[i] ));
        }

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
