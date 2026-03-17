using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rubrica.Api.Data;
using Rubrica.Api.Models;

namespace Rubrica.Api.Seed;

// Questa classe serve per inserire dati iniziali nel database.
// Viene usata all'avvio dell'app per creare utenti e interessi di esempio.
public static class DataSeeder
{
    // Metodo principale che esegue il seeding.
    // "Idempotente" significa che se i dati esistono già, non li ricrea.
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        // Creiamo uno "scope", cioè un contenitore temporaneo di servizi.
        using IServiceScope scope = serviceProvider.CreateScope();

        // Recuperiamo il DbContext dal contenitore dei servizi (Dependency Injection)
        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Recuperiamo anche il gestore utenti di Identity
        UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Se il database non esiste, lo crea automaticamente
        await context.Database.EnsureCreatedAsync();

        // Creiamo alcuni utenti demo (solo se non esistono già)
        ApplicationUser mario = await CreateUserIfNotExistsAsync
        (
            userManager,
            "mario@email.com",
            "123456",
            "Mario Rossi",
            "3331234567"
        );

        ApplicationUser laura = await CreateUserIfNotExistsAsync
        (
            userManager,
            "laura@email.com",
            "123456",
            "Laura Bianchi",
            "3337654321"
        );

        ApplicationUser giulia = await CreateUserIfNotExistsAsync
        (
            userManager,
            "giulia@email.com",
            "123456",
            "Giulia Verdi",
            "3331112222"
        );

        // Creiamo alcuni interessi per ogni utente (solo se non esistono già)
        await CreateInterestIfNotExistsAsync(context, mario.Id, "Calcio");
        await CreateInterestIfNotExistsAsync(context, mario.Id, "CSharp");
        await CreateInterestIfNotExistsAsync(context, mario.Id, "Cinema");

        await CreateInterestIfNotExistsAsync(context, laura.Id, "Nuoto");
        await CreateInterestIfNotExistsAsync(context, laura.Id, "Angular");
        await CreateInterestIfNotExistsAsync(context, laura.Id, "Musica");

        await CreateInterestIfNotExistsAsync(context, giulia.Id, "Lettura");
        await CreateInterestIfNotExistsAsync(context, giulia.Id, "Viaggi");
        await CreateInterestIfNotExistsAsync(context, giulia.Id, "Fotografia");
    }

    // Metodo che crea un utente solo se non esiste già
    private static async Task<ApplicationUser> CreateUserIfNotExistsAsync
    (
        UserManager<ApplicationUser> userManager,
        string email,
        string password,
        string nomeCompleto,
        string? phoneNumber
    )
    {
        // Cerchiamo l'utente tramite email
        ApplicationUser? existingUser = await userManager.FindByEmailAsync(email);

        // Se esiste già, lo restituiamo e non facciamo nulla
        if (existingUser != null)
        {
            return existingUser;
        }

        // Creiamo un nuovo utente
        ApplicationUser user = new ApplicationUser();
        user.UserName = email;
        user.Email = email;
        user.NomeCompleto = nomeCompleto;
        user.PhoneNumber = phoneNumber;
        user.CreatedAt = DateTime.UtcNow;

        // Creiamo l'utente tramite Identity (gestisce hash password, validazioni, ecc.)
        IdentityResult result = await userManager.CreateAsync(user, password);

        // Se ci sono errori, li raccogliamo e lanciamo un'eccezione
        if (!result.Succeeded)
        {
            List<string> errors = new List<string>();

            foreach (IdentityError error in result.Errors)
            {
                errors.Add(error.Description);
            }

            string message = string.Join(" | ", errors);
            throw new Exception($"Errore durante la creazione dell'utente {email}: {message}");
        }

        return user;
    }

    // Metodo che crea un interesse solo se non esiste già per quell'utente
    private static async Task CreateInterestIfNotExistsAsync(
        ApplicationDbContext context,
        string userId,
        string nome)
    {
        // Leggiamo tutti gli interessi dal database
        List<Interest> interests = await context.Interests.ToListAsync();

        // Controlliamo se esiste già un interesse con lo stesso nome per lo stesso utente
        for (int i = 0; i < interests.Count; i++)
        {
            Interest currentInterest = interests[i];

            bool sameUser = currentInterest.UserId == userId;
            bool sameName = string.Equals(currentInterest.Nome, nome, StringComparison.OrdinalIgnoreCase);

            // Se esiste già, non facciamo nulla
            if (sameUser && sameName)
            {
                return;
            }
        }

        // Creiamo un nuovo interesse
        Interest interest = new Interest();
        interest.UserId = userId;
        interest.Nome = nome;

        // Lo aggiungiamo al database
        context.Interests.Add(interest);
        await context.SaveChangesAsync();
    }
}
