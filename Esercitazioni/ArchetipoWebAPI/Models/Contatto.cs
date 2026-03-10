using System.ComponentModel.DataAnnotations;

namespace ArchetipoWebAPI.Models;
public class Contatto
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string NomeCompleto { get; set; } = string.Empty;
    
    [Required]
    [StringLength(30)]
    public string Telefono { get; set; } = string.Empty;
    public List<string> Competenze { get; set; } = new();
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int UserId { get; set; }
    public User User { get; set; } =null!;
}