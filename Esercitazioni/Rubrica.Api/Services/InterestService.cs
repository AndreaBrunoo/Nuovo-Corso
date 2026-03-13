using Rubrica.Api.Data;
using Rubrica.Api.Dtos;
using Rubrica.Api.Models;

namespace Rubrica.Api.Services;

public class InterestService
{
    private readonly ApplicationDbContext _context;

    public InterestService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<InterestDto>> GetAllByUserIdAsync(string userId)
    {
        List<InterestDto> result = new List<InterestDto>();

        // Leggiamo tutti gli interessi e poi filtriamo con un ciclo.
        // È meno efficiente del LINQ, ma è più facile da capire all'inizio.
        List<Interest> allInterests = _context.Interests.ToList();

        for (int i = 0; i < allInterests.Count; i++)
        {
            Interest currentInterest = allInterests[i];

            if (currentInterest.UserId == userId)
            {
                InterestDto dto = new InterestDto();
                dto.Id = currentInterest.Id;
                dto.Nome = currentInterest.Nome;

                result.Add(dto);
            }
        }

        return await Task.FromResult(result);
    }

    public async Task<InterestDto?> GetByIdAsync(int id, string userId)
    {
        Interest? interest = await _context.Interests.FindAsync(id);

        if (interest == null)
        {
            return null;
        }

        if (interest.UserId != userId)
        {
            return null;
        }

        InterestDto dto = new InterestDto();
        dto.Id = interest.Id;
        dto.Nome = interest.Nome;

        return dto;
    }

    public async Task<InterestDto?> CreateAsync(InterestCreateDto dto, string userId)
    {
        // Evitiamo interessi duplicati per lo stesso utente
        List<Interest> allInterests = _context.Interests.ToList();

        for (int i = 0; i < allInterests.Count; i++)
        {
            Interest currentInterest = allInterests[i];

            if (currentInterest.UserId == userId)
            {
                bool sameName = string.Equals(currentInterest.Nome, dto.Nome, StringComparison.OrdinalIgnoreCase);

                if (sameName)
                {
                    return null;
                }
            }
        }

        Interest interest = new Interest();
        interest.Nome = dto.Nome;
        interest.UserId = userId;

        _context.Interests.Add(interest);
        await _context.SaveChangesAsync();

        InterestDto result = new InterestDto();
        result.Id = interest.Id;
        result.Nome = interest.Nome;

        return result;
    }

    public async Task<InterestDto?> UpdateAsync(int id, InterestCreateDto dto, string userId)
    {
        Interest? interest = await _context.Interests.FindAsync(id);

        if (interest == null)
        {
            return null;
        }

        if (interest.UserId != userId)
        {
            return null;
        }

        // Controlliamo che il nuovo nome non esista già in un altro interesse dello stesso utente
        List<Interest> allInterests = _context.Interests.ToList();

        for (int i = 0; i < allInterests.Count; i++)
        {
            Interest currentInterest = allInterests[i];

            if (currentInterest.UserId == userId && currentInterest.Id != id)
            {
                bool sameName = string.Equals(currentInterest.Nome, dto.Nome, StringComparison.OrdinalIgnoreCase);

                if (sameName)
                {
                    return null;
                }
            }
        }

        interest.Nome = dto.Nome;
        await _context.SaveChangesAsync();

        InterestDto result = new InterestDto();
        result.Id = interest.Id;
        result.Nome = interest.Nome;

        return result;
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        Interest? interest = await _context.Interests.FindAsync(id);

        if (interest == null)
        {
            return false;
        }

        if (interest.UserId != userId)
        {
            return false;
        }

        _context.Interests.Remove(interest);
        await _context.SaveChangesAsync();

        return true;
    }
}