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
        RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        
        await EnsureRoleExistsAsync(roleManager, UserRoles.Admin);
        await EnsureRoleExistsAsync(roleManager, UserRoles.Editor);
        await EnsureRoleExistsAsync(roleManager, UserRoles.User);

        // Se il database non esiste, lo crea automaticamente
        /////////// await context.Database.EnsureCreatedAsync();

        DateTime oggi = DateTime.Today;
        int etaMario = oggi.Year - 1993;
        int etaLaura = oggi.Year - 2000;
        int etaGiulia = oggi.Year - 2010;

        // Creiamo alcuni utenti demo (solo se non esistono già)
        ApplicationUser admin = await EnsureUserExistsAsync
        (
            userManager,
            "mario@email.com",
            "123456",
            "Mario Rossi",
            "3331234567",
            new DateTime(1993, 01, 13).Date,
            false,
            etaMario
        );

        ApplicationUser editor = await EnsureUserExistsAsync
        (
            userManager,
            "laura@email.com",
            "123456",
            "Laura Bianchi",
            "3337654321",
            new DateTime(2000, 04, 23).Date,
            true,
            etaLaura
        );

        ApplicationUser normalUser = await EnsureUserExistsAsync
        (
            userManager,
            "giulia@email.com",
            "123456",
            "Giulia Verdi",
            "3331112222",
            new DateTime(2010, 02, 02).Date,
            true,
            etaGiulia
        );

        await EnsureSingleRoleAsync(userManager, admin, UserRoles.Admin);
        await EnsureSingleRoleAsync(userManager, editor, UserRoles.Editor);
        await EnsureSingleRoleAsync(userManager, normalUser, UserRoles.User);

        // Creiamo alcuni interessi per ogni utente (solo se non esistono già)
        await EnsureInterestExistsAsync(context, admin.Id,  "Calcio");
        await EnsureInterestExistsAsync(context, admin.Id,  "CSharp");
        await EnsureInterestExistsAsync(context, admin.Id,  "Cinema");

        await EnsureInterestExistsAsync(context, editor.Id, "Nuoto");
        await EnsureInterestExistsAsync(context, editor.Id, "Angular");
        await EnsureInterestExistsAsync(context, editor.Id, "Musica");

        await EnsureInterestExistsAsync(context, normalUser.Id, "Lettura");
        await EnsureInterestExistsAsync(context, normalUser.Id, "Viaggi");
        await EnsureInterestExistsAsync(context, normalUser.Id, "Fotografia");
    }

    private static async Task EnsureRoleExistsAsync(RoleManager<IdentityRole> roleManager, string roleName)
  {
    bool exists = await roleManager.RoleExistsAsync(roleName);
    if (!exists)
    {
      IdentityRole role = new IdentityRole();
      role.Name = roleName;

      await roleManager.CreateAsync(role);
    }
  }

  private static async Task<ApplicationUser> EnsureUserExistsAsync(
      UserManager<ApplicationUser> userManager,
        string email,
        string password,
        string nomeCompleto,
        string? phoneNumber,
        DateTime dataDiNascita,
        bool preferiti,     
        int eta
  )
  {
    // controlliamo se l'utente esiste già tramite email
    ApplicationUser? existingUser = await userManager.FindByEmailAsync(email);

    if (existingUser != null)
    {
      return existingUser;
    }

    ApplicationUser user = new ApplicationUser();
        user.UserName = email;
        user.Email = email;
        user.NomeCompleto = nomeCompleto;
        user.PhoneNumber = phoneNumber;
        user.CreatedAt = DateTime.UtcNow;
        user.DataDiNascita = dataDiNascita;
        user.Preferiti = preferiti;
        user.Eta = eta;



    IdentityResult result = await userManager.CreateAsync(user, password);

    if (!result.Succeeded)
    {
      List<string> errors = new List<string>();

      foreach (IdentityError error in result.Errors)
      {
        errors.Add(error.Description);
      }
      string message = string.Join("|", errors);
      throw new Exception($"Errore durante il seed dell'utente {email} : {message}");
    }
    return user;
  }

  private static async Task EnsureSingleRoleAsync(UserManager<ApplicationUser> userManager, ApplicationUser user, string targetRole)
  {
    IList<string> currentRoles = await userManager.GetRolesAsync(user);
    // rimuoviamo i ruoli classici se diversi da quello target

    for (int i = 0; i < currentRoles.Count; i++)
    {
      string currentRole = currentRoles[i];

      if (currentRole == UserRoles.Admin || currentRole == UserRoles.Editor || currentRole == UserRoles.User)
      {
        await userManager.RemoveFromRoleAsync(user, currentRole);
      }
    }
    bool alreadyInTargetRole = await userManager.IsInRoleAsync(user, targetRole);

    if (!alreadyInTargetRole)
    {
      await userManager.AddToRoleAsync(user, targetRole);
    }

  }


  private static async Task EnsureInterestExistsAsync(
   ApplicationDbContext context,
   string userId,
   string nome)
  {
    //leggiamo tutti gli interessi e controlliamo a mano
    // see questo interesse esiste già per quell'utente.

    List<Interest> interests = await context.Interests.ToListAsync();

    for (int i = 0; i < interests.Count; i++)
    {
      Interest currentInterest = interests[i];

      bool sameUser = currentInterest.UserId == userId;
      bool sameName = string.Equals(currentInterest.Nome, nome, StringComparison.OrdinalIgnoreCase);

      if (sameUser && sameName)
      {
        return;
      }
    }

    Interest interest = new Interest();
    interest.UserId = userId;
    interest.Nome = nome;

    context.Interests.Add(interest);
    await context.SaveChangesAsync();
  }

}
