namespace Rubrica.Api.Dtos;

public class UserProfileDto
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NomeCompleto { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }

    public  DateTime DataDiNascita { get; set; }
    public bool Preferiti { get; set; } = false;

}