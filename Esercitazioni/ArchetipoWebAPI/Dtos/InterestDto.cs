using System.ComponentModel.DataAnnotations;

namespace ArchetipoWebAPI.Dtos;

public class InterestDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
}