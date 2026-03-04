

public class StudentiController
{
    private readonly string path = "Data/studente.json";
    private List<Studente> studenti;
    private LastIdController lastIdController;

    public StudentiController()
    {
        studenti = JsonHelper.Leggi<List<Studente>>(path)?? new List<Studente>();
        lastIdController = new LastIdController();
    }

    public List<Studente> GetStudenti()
    {
        return studenti;
    }

    private void Salva()
    {
        JsonHelper.Salva(path, studenti);
    }

    public void AggiungiStudente(Studente nuovoStudente)
    {
        nuovoStudente.Id = lastIdController.GetNextId();
        studenti.Add(nuovoStudente);
        Salva();
    }

    public void ModificaStudente(int id, string nome, string cognome, int eta)
    {
        Studente studenteEsistente = null;
        foreach (var studente in studenti)
        {
            if (studente.Id == id)
            {
                studenteEsistente = studente;
                break;
            }
        }
        if (studenteEsistente != null)
        {
            studenteEsistente.Nome = nome;
            studenteEsistente.Cognome = cognome;
            studenteEsistente.Eta = eta;
            Salva();
        }
    }


    public void EliminaStudente(int id)
    {
        Studente studenteEsistente = null;
        foreach (var studente in studenti)
        {
            if (studente.Id == id)
            {
                studenteEsistente = studente;
                break;
            }
        }
        if (studenteEsistente != null)
        {
            studenti.Remove(studenteEsistente);
            Salva();
        }
    }

    public Studente VisualizzaStudente(int id)
    {
        Studente? studenteEsistente = null;
        foreach (var studente in studenti)
        {
            if (studente.Id == id)
            {
                studenteEsistente = studente;
                break;
            }
        }
        if (studenteEsistente == null)
        {
            throw new Exception($"Studente con ID {id} non trovato.");
        }
        return studenteEsistente;
    }
}