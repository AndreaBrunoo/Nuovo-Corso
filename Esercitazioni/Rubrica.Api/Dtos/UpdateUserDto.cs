namespace  Rubrica.Api.Dtos;

public class UpdateUserDto
{
    public string NomeCompleto {get; set;} = string.Empty;
    public string PhoneNumber {get; set;}  = string.Empty;
    public bool Preferiti { get; set; } 
}