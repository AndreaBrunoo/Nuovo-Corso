using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    // Costruttore che accetta le opzioni di configurazione del DbContext
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        //qui non serve aggiungere nulla, il costruttore base si occupa di configurare il Dbcontext con le opzioni fornite in Program.cs
    }
    // DbSet per la tabella contatti
    public DbSet<Contatto> contatti { get; set; }
    // DbSet per la tabelle Users
    public DbSet<User> users { get; set; }
}