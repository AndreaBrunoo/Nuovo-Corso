
namespace ArchetipoWebAPI.Dtos;
public class ContattoDto
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public List<string> Competenze { get; set; } = new();
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}