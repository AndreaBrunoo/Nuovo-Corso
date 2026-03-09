# Web Api

L'archetipo Web Api è un progetto ASP.NET Core che espone endpoint HTTP per consentire a client frontend come Angular di interagire con i dati prodotti dal backend

Il comando per creare un applicazione Web Api è:

```bash
dotnet new webapi -o NomeCartella
```

# Struttura tipica di una Web API

```c#
NomeCartella
├── Controllers
├── Models
├── Services
├── Repositories
├── Data
├── Dtos
├── Migrations
├── Midlleware
├── Helpers
├── Properties
│    └── LaunchSettings.json
├── Program.cs
└── appSetting.json
```

# Cartelle Principali:

    ● Controllers: Contiene i controller che gestiscono le richieste HTTP e restituiscono risposte.
    ● Models: Contiene le classi che rappresentano i dati e le entità del dominio.
    ● Services: Contiene la logica di business e i servizi che interagiscono con i dati, cioè le operazioni CRUD e altre logiche complesse.
    ● Repositories: Contiene le classi che gestiscono l'accesso ai dati , ad esempio interagendo con Entity Framework o altri ORM.
    ● Data: Contiene il contesto del database e le classi di accesso ai dati.
    ● Dtos: Contiene le classi Data Transfer Object, che sono altri modelli specifici per il trasferimento dei dati tra client e server, spesso usati per evitare di 
      esporre direttamente le entità del dominio.
    ● Migrations: Contiene le migrazioni di Entity Framework per gestire le modifiche al database quando viene modificato un modello.
    ● Middleware: Contiene componenti middleware personalizzati per gestire richieste e risposte HTTP, ad esempio per la gestione degli errori o l'autenticazione.
    ● Helpers: Contiene classi di utilità e helper per operazioni comuni, come la gestione dei file, la validazione personalizzata, ecc.
    ● Properties: contiene file di configurazione specifici del progetto come LaunchSettings.json che definisce le configurazioni di avvio per l'applicazioni.
    ● Program.cs: il punto di ingresso dell'applicazione, dove viene configurato il pipeline di esecuzione e i servizi.
    ● appSetting.json: il file di configurazione principale dell'applicazione, dove vengono definiti parametri come stringhe di connessione al database, chiavi API, e altre
      impostazioni.

# Controllers

I controller sono classi che ereditano da ControllerBase e sono decorati con l'attributo [ApiController]. Ogni metodo all'interno di un controller rappresenta un endpoint HTTp e viene decorato con attributi come:

    ● [HttpGet]
    ● [HttpPost]
    ● [HttpPut]
    ● [HttpDelete]

per indicare il tipo di richiesta che gestisce.

Controller base:

```c#
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers()
    {
        return ok();
    }
}
```

Il Controller riceve richieste tipo:

```c#
GET /api/users
```

Di solito le richieste vengono inoltrate attraverso comandi CURL o client HTTP come Postman, oppure da un frontend Angular che consuma l'API.

# Models

I modelli rappresentano le entità del dominio e sono mappati a tabelle del database.

Ad esempio, un modello Contatto potrebbe essere:

```c#
public class Contatto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public List<ContattoInteresse> interessi { get; set; }
}
```

Quando usiamo Entity Framework Core, diventano tabelle.

# DTOs (Data Transfer Object)

Servono per non esporre direttamente i Models
Ad esempio, potremmo avere un ContattoDto che contiene solo alcune proprietà:

```c#
public class ContattoDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cognome { get; set; }
}
```

Utile per sicurezza e controllo dati

# Services

Qui mettiamo la logica business, tipo le operazioni CRUD e altre logiche complesse.
Ad esempio, un ContattoService potrebbe avere metodi come:

```c#
public class ContattoService 
{
    public List<Contatto> GetAll()
    {
        ...
    }
    public Contatto GetById(int id)
    {
        ...
    }
}
```

# Repositories

Accesso ai dati/database

Ad esempio, un ContattoRepository potrebbe usare Entity Framework per interagire con il database:

```c#
public class ContattoRepository
{
    private readonly ApplicationDbContext _context;
    public ContattoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Contatto> GetAll()
    {
        return _context.Contatti.ToList();
    }
}
```

separa database dalla logica.

# Data

Contiene il DbContext.

Ad esempio, ApplicationDbContext potrebbe essere:

```c#
public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
}
```

Il DbContext è la classe principale di Entity Framework che gestisce la connessione al database e le operazioni CRUD che vengono eseguite sulle entità dai services dell'applicazione.

# Migrations

Generate automaticamente da Entity Framework:

```bash
dotnet ef migration add InitialCreate 
dotnet ef database update
```

gestiscono modifiche schema database

# Middleware

Per intercettare richieste globali:

    ● logging 
    ● auth
    ● error handling

Ad esempio, un middleware per gestire eccezzioni globali:

```c#
public class ExceptionMidlleware
{   
}
```

# Helpers

Funzioni utility.

Esempio:

    ● JWT generator
    ● Date formatter
    ● Hashing password

Nello specifico JWT sarà quello che si usa per autenticare i client Angular.

# Program.cs

Qui si configura il pipeline di esecuzione e i servizi.

Ad esempio, per configurare Entity Framework e i servizi:

```c#
var builder = webApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Built();

app.MapC
```

# Esempio pratico

Contatto

Richiesta:

```c#
POST /api/contatto/5/
```

Flusso:

    ● Il Controller riceve richiesta
    ● Il Controller chiama ContattoService
    ● ContattoService chiama ContattoRepository
    ● ContattoRepository legge il db e restituisce dati
    ● I dati vengono ritornati al Model e poi al Controller
    ● Il Controller restituisce risposta HTTP a un DTO
    ● Viene generata la response in JSON a Angular

