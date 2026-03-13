using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ArchetipoWebAPI.Data;
using ArchetipoWebAPI.Helpers;
using ArchetipoWebAPI.Models;
using ArchetipoWebAPI.Services;
using ArchetipoWebAPI.Dtos;
using ArchetipoWebAPI.Seed;

var builder = WebApplication.CreateBuilder(args);

// aggiunge i controller
builder.Services.AddControllers();

// Configurazione DbContext con SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurazione CORS per permettere al frontend Angular di accedere all'api
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

//Configura Identity per gli utenti

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    //Regole password semplice per fare pratica
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    
})
.AddSignInManager<SignInManager<ApplicationUser>>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

//configurazione JWT

string? jwtKey      = builder.Configuration["Jwt:Key"];
string? jwtIssuer   = builder.Configuration["Jwt:Issuer"];
string? jwtAudience = builder.Configuration["Jwt:Audience"];

if(string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience))
{
    throw new Exception("Configurazione JWT mancante in appsettings.json");
}

// Configurazione autenticazione JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>
{
  options.TokenValidationParameters = new TokenValidationParameters
 {
    // controlla che il token sia stato emesso dall'issuer corretto
    ValidateIssuer = true,

    // controlla che il token sia destinato all'audience corretta
    ValidateAudience = true,

    //controlla che il token non sia scaduto
    ValidateLifetime = true,

    //controlla la firma del Token
    ValidateIssuerSigningKey = true,

    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))

 };
});

//abilita l'autorizzazione con [Authorize]
builder.Services.AddAuthorization();

// Dependency Injection : registriamo  services e helper

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<InterestService>();
builder.Services.AddScoped<JwtHelper>();

var app = builder.Build();

app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// applica automaticamente le migration all'avvio

using(var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}


// Richiama il seed iniziale con alcuni utenti demo e i loro interessi. Se i dati esistono già non vengono duplicati.

await DataSeeder.SeedAsync(app.Services);
app.Run();