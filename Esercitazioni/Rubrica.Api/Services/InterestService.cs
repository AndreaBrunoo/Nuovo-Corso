using Rubrica.Api.Data;
using Rubrica.Api.Dtos;
using Rubrica.Api.Models;

namespace Rubrica.Api.Services;

// Questo servizio gestisce gli "Interessi" associati a un utente.
// Ogni interesse ha: Id, Nome, UserId.
public class InterestService
{
    // ApplicationDbContext è il collegamento al database
    private readonly ApplicationDbContext _context;

    // Il costruttore riceve il DbContext tramite Dependency Injection
    public InterestService(ApplicationDbContext context)
    {
        // Salvo il riferimento al database
        _context = context; 
    }

    // Restituisce tutti gli interessi di un certo utente
    public async Task<List<InterestDto>> GetAllByUserIdAsync(string userId)
    {
        List<InterestDto> result = new List<InterestDto>();

        // Leggiamo tutti gli interessi dal database
        List<Interest> allInterests = _context.Interests.ToList();

        // Cicliamo su tutti gli interessi
        for (int i = 0; i < allInterests.Count; i++)
        {
            Interest currentInterest = allInterests[i];

            // Se l'interesse appartiene all'utente richiesto
            if (currentInterest.UserId == userId)
            {
                // Creiamo il DTO da restituire
                InterestDto dto = new InterestDto();
                dto.Id = currentInterest.Id;
                dto.Nome = currentInterest.Nome;

                result.Add(dto);
            }
        }

        // Ritorniamo il risultato come Task completato
        return await Task.FromResult(result);
    }

    // Restituisce un singolo interesse tramite Id, ma solo se appartiene all'utente
    public async Task<InterestDto?> GetByIdAsync(int id, string userId)
    {
        // Cerchiamo l'interesse nel database tramite Id
        Interest? interest = await _context.Interests.FindAsync(id);

        // Se non esiste ritorniamo null
        if (interest == null)
        {
            return null;
        }

        // Se l'interesse non appartiene all'utente ritorniamo null
        if (interest.UserId != userId)
        {
            return null;
        }

        // Convertiamo in DTO
        InterestDto dto = new InterestDto();
        dto.Id = interest.Id;
        dto.Nome = interest.Nome;

        return dto;
    }

    // Crea un nuovo interesse per un utente
    public async Task<InterestDto?> CreateAsync(InterestCreateDto dto, string userId)
    {
        // Evitiamo interessi duplicati per lo stesso utente
        List<Interest> allInterests = _context.Interests.ToList();

        for (int i = 0; i < allInterests.Count; i++)
        {
            Interest currentInterest = allInterests[i];
            
            // Consideriamo solo gli interessi di questo utente
            if (currentInterest.UserId == userId)
            {
                bool sameName = string.Equals(currentInterest.Nome, dto.Nome, StringComparison.OrdinalIgnoreCase);

                if (sameName)
                {
                    // Interesse già esistente non lo creiamo
                    return null;
                }
            }
        }

        // Creiamo il nuovo interesse
        Interest interest = new Interest();
        interest.Nome = dto.Nome;
        interest.UserId = userId;

        // Lo aggiungiamo al database
        _context.Interests.Add(interest);
        await _context.SaveChangesAsync();

        // Creiamo il DTO da restituire
        InterestDto result = new InterestDto();
        result.Id = interest.Id;
        result.Nome = interest.Nome;

        return result;
    }

    // Modifica un interesse esistente
    public async Task<InterestDto?> UpdateAsync(int id, InterestCreateDto dto, string userId)
    {
        // Cerchiamo l'interesse da modificare
        Interest? interest = await _context.Interests.FindAsync(id);

        // Se non esiste → null
        if (interest == null)
        {
            return null;
        }

        // Se non appartiene all'utente → null
        if (interest.UserId != userId)
        {
            return null;
        }
 
        List<Interest> allInterests = _context.Interests.ToList();
        
        // Controlliamo che il nuovo nome non esista già in un altro interesse dello stesso utente
        for (int i = 0; i < allInterests.Count; i++)
        {
            Interest currentInterest = allInterests[i];

            // Consideriamo solo gli interessi dello stesso utente
            if (currentInterest.UserId == userId && currentInterest.Id != id)
            {
                bool sameName = string.Equals(currentInterest.Nome, dto.Nome, StringComparison.OrdinalIgnoreCase);

                if (sameName)
                {
                    // Nome già usato non possiamo aggiornare
                    return null;
                }
            }
        }

        // Aggiorniamo il nome
        interest.Nome = dto.Nome;

        // Salviamo le modifiche
        await _context.SaveChangesAsync();

        // Creaiamo il DTO da restituire
        InterestDto result = new InterestDto();
        result.Id = interest.Id;
        result.Nome = interest.Nome;

        return result;
    }

    // Elimina un interesse
    public async Task<bool> DeleteAsync(int id, string userId)
    {
        // Cerchiamo l'interesse
        Interest? interest = await _context.Interests.FindAsync(id);

        // Se non esiste → false
        if (interest == null)
        {
            return false;
        }

        // Se non appartiene all'utente → false
        if (interest.UserId != userId)
        {
            return false;
        }

        // Lo eliminiamo dal database
        _context.Interests.Remove(interest);
        await _context.SaveChangesAsync();
        
        // Eliminazione riuscita
        return true;
    }
}