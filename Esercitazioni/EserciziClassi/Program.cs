// guarda la lista 
using System.ComponentModel.DataAnnotations;
class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        while (true)
        {
            TitoloClasse();
            Menu();
            char scelta = Console.ReadKey().KeyChar;
            StudentiController studentiController = new StudentiController();

            switch (scelta)
            {
                case '1':
                    Console.Clear();
                    while (true)
                    {
                        try
                        {

                            TitoloAggiunta();
                            Console.WriteLine("1. Aggiungi Studente");
                            Console.WriteLine("2. Esci");
                            char scelta1 = Console.ReadKey().KeyChar;

                            if (scelta1 == '1')
                            {
                                Console.Clear();
                                TitoloAggiunta();
                                Studente nuovoStudente = new Studente();
                                Console.Write("Inserisci il tuo nome: ");
                                string? aggiuntaNome = Console.ReadLine().Trim();

                                if (string.IsNullOrWhiteSpace(aggiuntaNome))
                                {
                                    ErroreAggiunta("La stringa è nulla o vuota.");
                                    continue;
                                }
                                nuovoStudente.Nome = aggiuntaNome;

                                Console.Clear();
                                TitoloAggiunta();
                                Console.Write("Inserisci il tuo cognome: ");
                                string? aggiuntaCognome = Console.ReadLine().Trim();

                                if (string.IsNullOrWhiteSpace(aggiuntaCognome))
                                {
                                    ErroreAggiunta("La stringa è nulla o vuota.");
                                    continue;
                                }
                                nuovoStudente.Cognome = aggiuntaCognome;

                                Console.Clear();
                                TitoloAggiunta();
                                Console.Write("Inserisci la tua età: ");
                                string? aggiuntaEta = Console.ReadLine();
                                Console.Clear();

                                if (int.TryParse(aggiuntaEta, out int aggiuntaEtaParse))
                                {
                                    var context = new ValidationContext(nuovoStudente);

                                    Validator.ValidateObject(nuovoStudente, context, validateAllProperties: true);

                                    TitoloAggiunta();
                                    nuovoStudente.Eta = aggiuntaEtaParse;
                                    Console.WriteLine("Studente aggiunto con successo alla lista.");
                                    Console.WriteLine(" ");
                                    TabellaSenzaId();
                                    Console.WriteLine($"{nuovoStudente.Nome,-16} {nuovoStudente.Cognome,-16} {nuovoStudente.Eta,-5}");
                                    Console.WriteLine(" ");
                                    Console.WriteLine(new string('-', 47));
                                    Console.WriteLine(" ");
                                    studentiController.AggiungiStudente(nuovoStudente);
                                    PremiTasto();
                                }
                                else
                                    ErroreAggiunta("Età non valida. Riprova.");
                                continue;
                            }
                            else if (scelta1 == '2')
                            {
                                Console.Clear();
                                break;
                            }
                            else
                                ErroreAggiunta("Carattere non valido. Riprova.");
                            continue;

                        }

                        catch (Exception ex)
                        {
                            ErroreErrore("Errore di validazione:");
                            Console.WriteLine(" ");
                            Console.WriteLine($"- {ex.Message}");
                        }
                    }
                    break;

                case '2':
                    Console.Clear();
                    try
                    {
                        while (true)
                        {
                            TitoloModifica();
                            Console.WriteLine("1. Modifica Studente");
                            Console.WriteLine("2. Esci");
                            char scelta2 = Console.ReadKey().KeyChar;

                            if (scelta2 == '1')
                            {
                                List<Studente> studenti = studentiController.GetStudenti();
                                Console.Clear();
                                TitoloModifica();
                                Tabella();
                                foreach (var studente in studenti)
                                {
                                    Console.WriteLine($"{studente.Id,-5} {studente.Nome,-14} {studente.Cognome,-14} {studente.Eta,-4}");
                                }
                                Console.WriteLine(" ");
                                Console.WriteLine(new string('-', 47));
                                Console.WriteLine(" ");
                                Console.Write("Inserisci l'Id da modificare: ");
                                string? modificaId = Console.ReadLine();
                                Console.Clear();

                                if (int.TryParse(modificaId, out int modificaIdParse))
                                {
                                    Studente? studenteEsistente = null;
                                    foreach (var studente in studenti)
                                    {
                                        if (studente.Id == modificaIdParse)
                                        {
                                            studenteEsistente = studente;
                                            break;
                                        }
                                    }

                                    if (studenteEsistente != null)
                                    {
                                        StudenteDaModificare(modificaIdParse, studenteEsistente);
                                        Studente studenteModificato = new Studente();
                                        Console.Write("Inserisci il nuvo nome: ");
                                        string? modificaNome = Console.ReadLine().Trim();

                                        if (string.IsNullOrWhiteSpace(modificaNome))
                                        {
                                            ErroreModifica("La stringa è nulla o vuota.");
                                            continue;
                                        }
                                        studenteModificato.Nome = modificaNome;

                                        Console.Clear();
                                        StudenteDaModificare(modificaIdParse, studenteEsistente);
                                        Console.Write("Inserisci il nuovo cognome: ");
                                        string? modificaCognome = Console.ReadLine().Trim();

                                        if (string.IsNullOrWhiteSpace(modificaCognome))
                                        {
                                            ErroreModifica("La stringa è nulla o vuota.");
                                            continue;
                                        }
                                        studenteModificato.Cognome = modificaCognome;

                                        Console.Clear();
                                        StudenteDaModificare(modificaIdParse, studenteEsistente);
                                        Console.Write("Inserisci la nuova età: ");
                                        string? eta = Console.ReadLine();
                                        Console.Clear();

                                        if (int.TryParse(eta, out int etaParse))
                                        {
                                            TitoloModifica();
                                            studenteModificato.Eta = etaParse;

                                            var context = new ValidationContext(studenteModificato);
                                            Validator.ValidateObject(studenteModificato, context, validateAllProperties: true);

                                            studentiController.ModificaStudente(modificaIdParse, studenteModificato.Nome, studenteModificato.Cognome, studenteModificato.Eta);
                                            Tabella();
                                            Console.WriteLine($"{modificaIdParse,-5} {studenteModificato.Nome,-14} {studenteModificato.Cognome,-14} {etaParse,-4}");
                                            Console.WriteLine(" ");
                                            Console.WriteLine(new string('-', 47));
                                            Console.WriteLine(" ");
                                            Console.WriteLine("Modifica eseguita con successo");
                                            PremiTasto();
                                        }
                                        else
                                            ErroreModifica("Età non valida. Riprova.");
                                    }
                                    else
                                        ErroreModifica($"studente con ID {modificaIdParse} non trovato.");
                                }
                                else
                                    ErroreModifica("Id non valida. Riprova.");
                                continue;
                            }
                            else if (scelta2 == '2')
                            {
                                Console.Clear();
                                break;
                            }
                            else
                                ErroreModifica("Carattere non valido. Riprova.");
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        ErroreErrore("Errore di validazione:");
                        Console.WriteLine(" ");
                        Console.WriteLine($"- {ex.Message}");
                    }
                    break;

                case '3':
                    Console.Clear();

                    while (true)
                    {
                        TitoloElimina();
                        Console.WriteLine("1. Elimina Studente");
                        Console.WriteLine("2. Esci");
                        char scelta3 = Console.ReadKey().KeyChar;
                        if (scelta3 == '1')
                        {
                            List<Studente> studentiDaEliminare = studentiController.GetStudenti();
                            Console.Clear();
                            TitoloElimina();
                            Tabella();

                            foreach (var studente in studentiDaEliminare)
                            {
                                Console.WriteLine($"{studente.Id,-5} {studente.Nome,-14} {studente.Cognome,-14} {studente.Eta,-4}");
                            }
                            Console.WriteLine(" ");
                            Console.WriteLine(new string('-', 47));
                            Console.WriteLine(" ");
                            Console.Write("Inserisci l'Id da Eliminare: ");
                            string? idEliminare = Console.ReadLine();
                            Console.Clear();

                            if (int.TryParse(idEliminare, out int idEliminareParse))
                            {
                                Studente? studenteEsistente2 = null;
                                foreach (var studente in studentiDaEliminare)
                                {
                                    if (studente.Id == idEliminareParse)
                                    {
                                        studenteEsistente2 = studente;
                                        break;
                                    }
                                }
                                if (studenteEsistente2 != null)
                                {
                                    Console.Clear();
                                    Console.WriteLine(" ");
                                    Tabella();
                                    Console.WriteLine($"{studenteEsistente2.Id,-5} {studenteEsistente2.Nome,-14} {studenteEsistente2.Cognome,-14} {studenteEsistente2.Eta,-4}");
                                    Console.WriteLine(" ");
                                    Console.WriteLine(new string('-', 47));
                                    Console.WriteLine(" ");
                                    Console.WriteLine("Sei sicuro di voler eliminare questo studente?");
                                    Console.WriteLine("Premi 's' per confermare o");
                                    Console.Write("altro per tornare al menu: ");
                                    char scelta31 = Console.ReadKey().KeyChar;
                                    if (scelta31 == 's' || scelta31 == 'S')
                                    {
                                        studentiController.EliminaStudente(idEliminareParse);
                                        ErroreElimina("Eliminazione eseguita con successo.");
                                    }
                                    else
                                        Console.Clear();
                                }
                                else
                                    ErroreElimina($"studente con ID {idEliminareParse} non trovato.");
                            }
                            else
                                ErroreElimina("Id non valida. Riprova.");
                            continue;
                        }
                        else if (scelta3 == '2')
                        {
                            Console.Clear();
                            break;
                        }
                        else
                            ErroreElimina("Carattere non valido. Riprova.");
                        continue;
                    }
                    break;

                case '4':

                    // Volevo aggiungere se esiste il json o no
                    TestoConClear($"{new string('-', 15)} LISTA STUDENTI {new string('-', 16)}");
                    List<Studente> listaStudenti = studentiController.GetStudenti();
                    Tabella();
                    foreach (var studente in listaStudenti)
                    {
                        Console.WriteLine($"{studente.Id,-5} {studente.Nome,-14} {studente.Cognome,-14} {studente.Eta,-4}");
                    }
                    PremiTasto();
                    Console.Clear();
                    break;

                case '5':

                    TestoConClear($"{new string('-', 20)} ESCI {new string('-', 21)}");
                    Console.WriteLine("Sei sicuro di voler uscire?");
                    Console.WriteLine("Premi 's' per confermare o");
                    Console.Write("altro per tornare al menu: ");
                    char scelta7 = Console.ReadKey().KeyChar;
                    if (scelta7 == 's' || scelta7 == 'S')
                    {
                        TestoConClear("Arrivederci!");
                        Environment.Exit(0);
                    }
                    else
                        Console.Clear();
                    break;

                default:
                    Console.Clear();
                    TitoloClasse();
                    Console.WriteLine("Scelta non valida. Riprova.");
                    break;
            }
        }
    }

    static void ErroreErrore(string messaggio)
    {
        Console.Clear();
        TitoloErrore();
        Console.WriteLine(messaggio);
    }
    static void TitoloErrore()
    {
        Console.WriteLine(" ");
        Console.WriteLine($"{new string('-', 20)} ERRORE {new string('-', 19)}");
        Console.WriteLine(" ");
    }
    static void ErroreElimina(string messaggio)
    {
        Console.Clear();
        TitoloElimina();
        Console.WriteLine(messaggio);
    }
    static void TitoloElimina()
    {
        Console.WriteLine(" ");
        Console.WriteLine($"{new string('-', 15)} ELIMINA STUDENTE {new string('-', 14)}");
        Console.WriteLine(" ");
    }
    static void StudenteDaModificare(int modificaIdParse, Studente studenteEsistente)
    {
        TitoloModifica();
        Console.WriteLine("Studente che vuoi modificare: ");
        Console.WriteLine(" ");
        Tabella();
        Console.WriteLine($"{modificaIdParse,-5} {studenteEsistente.Nome,-14} {studenteEsistente.Cognome,-14} {studenteEsistente.Eta,-4}");
        Console.WriteLine(" ");
        Console.WriteLine(new string('-', 47));
        Console.WriteLine(" ");
    }
    static void ErroreModifica(string messaggio)
    {
        Console.Clear();
        TitoloModifica();
        Console.WriteLine(messaggio);
    }
    static void TitoloModifica()
    {
        Console.WriteLine(" ");
        Console.WriteLine($"{new string('-', 14)} MODIFICA STUDENTE {new string('-', 14)}");
        Console.WriteLine(" ");
    }
    static void ErroreAggiunta(string messaggio)
    {
        Console.Clear();
        TitoloAggiunta();
        Console.WriteLine(messaggio);
    }
    static void TitoloAggiunta()
    {
        Console.WriteLine(" ");
        Console.WriteLine($"{new string('-', 14)} AGGIUNGI STUDENTI {new string('-', 14)}");
        Console.WriteLine(" ");
    }
    static void PremiTasto()
    {
        Console.WriteLine(" ");
        Console.WriteLine(new string('-', 47));
        Console.Write("Premi un tasto per tornare al menu...");
        Console.ReadKey();
        Console.Clear();
    }
    static void Tabella()
    {
        Console.WriteLine($"{"ID",-5} {"NOME",-14} {"COGNOME",-14} {"ETA",-4}");
        Console.WriteLine(new string('-', 47));
        Console.WriteLine(" ");
    }
    static void TabellaSenzaId()
    {
        Console.WriteLine($"{"NOME",-16} {"COGNOME",-16} {"ETA",-5}");
        Console.WriteLine(new string('-', 47));
        Console.WriteLine(" ");
    }
    static void Menu()
    {
        Console.WriteLine("1. Aggiungi studente");
        Console.WriteLine("2. Modifica studente");
        Console.WriteLine("3. Elimina studente");
        Console.WriteLine("4. Visualizza lista studenti");
        Console.WriteLine("5. Esci");
    }
    static void TestoConClear(string messaggio)
    {
        Console.Clear();
        Console.WriteLine(" ");
        Console.WriteLine(messaggio);
        Console.WriteLine(" ");
    }
    static void TitoloClasse()
    {
        Console.WriteLine(" ");
        Console.WriteLine($"{new string('-', 20)} CLASSE {new string('-', 19)}");
        Console.WriteLine(" ");
    }
}