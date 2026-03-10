using System.ComponentModel.DataAnnotations;

namespace ArchetipoWebAPI.Models;
public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    
    [Required]
    [StringLength(20)]
    public string Ruolo { get; set; } = "User";
    public List<Contatto> Contatti { get; set; } = new();
}