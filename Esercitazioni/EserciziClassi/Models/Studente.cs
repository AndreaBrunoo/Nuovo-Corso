using System.ComponentModel.DataAnnotations;
public class Studente
{
    public int Id { get; set; }
    [RegularExpression("^[A-Za-zÀ-ÖØ-öø-ÿ ']+$", ErrorMessage = "Il campo non può contenere numeri o caratteri speciali.")]
    [Required(ErrorMessage = "Il nome è obbligatorio.")]
    [StringLength(15, MinimumLength = 2, ErrorMessage = "Il nome deve essere compreso tra 2 e 15 caratteri.")]
    public string Nome { get; set; } = "";

    [RegularExpression("^[A-Za-zÀ-ÖØ-öø-ÿ ']+$", ErrorMessage = "Il campo non può contenere numeri o caratteri speciali.")]
    [Required(ErrorMessage = "Il cognome è obbligatorio.")]
    [StringLength(15, MinimumLength = 2, ErrorMessage = "Il cognome deve essere compreso tra 2 e 15 caratteri.")]
    public string Cognome { get; set; } = "";
    [Range(6, 99, ErrorMessage = "Inserisci un età compresa tra 6 e 99.")]
    public int Eta { get; set; }

    [EmailAddress(ErrorMessage = "Email non valida")]
    public string Email { get; set; }

    [Phone(ErrorMessage = "Numero di telefono non valido")]
    public string Telefono { get; set; }
    public List<StudenteInteresse> Interessi { get; set; }
}
