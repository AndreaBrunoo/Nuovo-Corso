using System.ComponentModel.DataAnnotations;
class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        while (true)
        {
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
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(" 1) Aggiungi Studente");
                            Console.WriteLine(" 2) Esci");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine();
                            Console.Write("Seleziona un'opzione: ");
                            Console.ResetColor();

                            char scelta1 = Console.ReadKey().KeyChar;
                            if (scelta1 == '1')
                            {
                                Studente nuovoStudente = new Studente();

                                StampagialloConTitoloAggiunta("Inserisci il tuo nome: ");
                                string? aggiuntaNome = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(aggiuntaNome))
                                {
                                    ErroreAggiunta("La stringa è nulla o vuota.");
                                    continue;
                                }
                                nuovoStudente.Nome = aggiuntaNome;

                                StampagialloConTitoloAggiunta("Inserisci il tuo cognome: ");
                                string? aggiuntaCognome = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(aggiuntaCognome))
                                {
                                    ErroreAggiunta("La stringa è nulla o vuota.");
                                    continue;
                                }
                                nuovoStudente.Cognome = aggiuntaCognome;

                                StampagialloConTitoloAggiunta("Inserisci la tua età: ");
                                string? aggiuntaEta = Console.ReadLine();
                                Console.Clear();

                                if (int.TryParse(aggiuntaEta, out int aggiuntaEtaParse))
                                    nuovoStudente.Eta = aggiuntaEtaParse;
                                else
                                {
                                    ErroreAggiunta("Età non valida. Riprova.");
                                    continue;
                                }

                                StampagialloConTitoloAggiunta("Inserisci la tua Email: ");
                                string? aggiuntaEmail = Console.ReadLine().Trim();
                                nuovoStudente.Email = aggiuntaEmail;

                                StampagialloConTitoloAggiunta("Inserisci il tuo numero di telefono: ");
                                string? aggiuntaTelefono = Console.ReadLine().Trim();
                                nuovoStudente.Telefono = aggiuntaTelefono;

                                List<StudenteInteresse> interessi = new List<StudenteInteresse>();

                                while (true)
                                {
                                    StampagialloConTitoloAggiunta("Inserisci almeno due interessi.");
                                    Console.WriteLine();
                                    Console.Write(" Inserisci interesse  |  Annulla con 0: ");
                                    StudenteInteresse interesse = new StudenteInteresse();
                                    string aggiuntaInteresse = Console.ReadLine().Trim();

                                    if (aggiuntaInteresse != "0")
                                    {
                                        interesse.NomeInteresse = aggiuntaInteresse;
                                        interessi.Add(interesse);
                                    }
                                    else
                                        break;
                                }
                                nuovoStudente.Interessi = interessi;
                                TitoloAggiunta();


                                var context = new ValidationContext(nuovoStudente);

                                Validator.ValidateObject(nuovoStudente, context, validateAllProperties: true);

                                TabellaSenzaId();
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write($"{nuovoStudente.Nome,-16} {nuovoStudente.Cognome,-16} ");

                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"{nuovoStudente.Eta,-5}");
                                Console.WriteLine();

                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.WriteLine(new string('─', 47));
                                Console.WriteLine();

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Studente aggiunto con successo alla lista.");
                                Console.ResetColor();
                                studentiController.AggiungiStudente(nuovoStudente);
                                PremiTasto();
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

                        catch (ValidationException ex)
                        {
                            Console.Clear();
                            TitoloAggiunta();
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Errore di validazione:");
                            Console.WriteLine($"- {ex.Message}");
                            Console.ResetColor();
                            PremiTasto();
                        }
                    }
                    break;

                case '2':
                    Console.Clear();
                    while (true)
                    {
                        try
                        {
                            TitoloModifica();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(" 1) Modifica Studente");
                            Console.WriteLine(" 2) Esci");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine();
                            Console.Write("Seleziona un'opzione: ");
                            Console.ResetColor();

                            char scelta2 = Console.ReadKey().KeyChar;
                            if (scelta2 == '1')
                            {
                                List<Studente> studenti = studentiController.GetStudenti();
                                Console.Clear();
                                TitoloModifica();
                                Tabella();
                                foreach (var studente in studenti)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.Write($"{studente.Id,-5} ");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write($"{studente.Nome,-14} {studente.Cognome,-14} ");
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine($"{studente.Eta,-4}");
                                    Console.ResetColor();
                                }
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.WriteLine(new string('─', 47));
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("Inserisci l'Id da modificare: ");
                                Console.ResetColor();
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
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.Write("Inserisci il nuvo nome: ");
                                        Console.ResetColor();
                                        string? modificaNome = Console.ReadLine();

                                        if (string.IsNullOrWhiteSpace(modificaNome))
                                        {
                                            ErroreModifica("La stringa è nulla o vuota.");
                                            continue;
                                        }
                                        studenteModificato.Nome = modificaNome;

                                        Console.Clear();
                                        StudenteDaModificare(modificaIdParse, studenteEsistente);
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.Write("Inserisci il nuovo cognome: ");
                                        Console.ResetColor();
                                        string? modificaCognome = Console.ReadLine();

                                        if (string.IsNullOrWhiteSpace(modificaCognome))
                                        {
                                            ErroreModifica("La stringa è nulla o vuota.");
                                            continue;
                                        }
                                        studenteModificato.Cognome = modificaCognome;

                                        Console.Clear();
                                        StudenteDaModificare(modificaIdParse, studenteEsistente);
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.Write("Inserisci la nuova età: ");
                                        Console.ResetColor();
                                        string? eta = Console.ReadLine();
                                        Console.Clear();

                                        if (int.TryParse(eta, out int etaParse))
                                        {
                                            studenteModificato.Eta = etaParse;
                                        }

                                        else
                                        {
                                            ErroreModifica("Età non valida. Riprova.");
                                            continue;
                                        }


                                        StampagialloConTitoloAggiunta("Inserisci la tua Email: ");
                                        string? aggiuntaEmail = Console.ReadLine().Trim();
                                        studenteModificato.Email = aggiuntaEmail;

                                        StampagialloConTitoloAggiunta("Inserisci il tuo numero di telefono: ");
                                        string? aggiuntaTelefono = Console.ReadLine().Trim();
                                        studenteModificato.Telefono = aggiuntaTelefono;

                                        List<StudenteInteresse> interessi = new List<StudenteInteresse>();

                                        while (true)
                                        {
                                            StampagialloConTitoloAggiunta("Inserisci almeno due interessi.");
                                            Console.WriteLine();
                                            Console.Write(" Inserisci interesse  |  Annulla con 0: ");
                                            StudenteInteresse interesse = new StudenteInteresse();
                                            string aggiuntaInteresse = Console.ReadLine().Trim();

                                            if (aggiuntaInteresse != "0")
                                            {
                                                interesse.NomeInteresse = aggiuntaInteresse;
                                                interessi.Add(interesse);
                                            }
                                            else
                                                break;
                                        }
                                        studenteModificato.Interessi = interessi;

                                        TitoloModifica();


                                        var context = new ValidationContext(studenteModificato);

                                        Validator.ValidateObject(studenteModificato, context, validateAllProperties: true);

                                        studentiController.ModificaStudente(modificaIdParse, studenteModificato.Nome, studenteModificato.Cognome, etaParse, studenteModificato.Email, studenteModificato.Telefono,studenteModificato.Interessi );
                                        Tabella();
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.Write($"{modificaIdParse,-5} ");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write($"{studenteModificato.Nome,-14} {studenteModificato.Cognome,-14} ");
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.WriteLine($"{etaParse,-4}");
                                        Console.WriteLine();
                                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                                        Console.WriteLine(new string('─', 47));
                                        Console.WriteLine();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Modifica eseguita con successo");
                                        Console.ResetColor();
                                        PremiTasto();

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
                        catch (ValidationException ex)
                        {
                            Console.Clear();
                            TitoloModifica();
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Errore di validazione:");
                            Console.WriteLine($"- {ex.Message}");
                            Console.ResetColor();
                            PremiTasto();
                        }
                    }
                    break;

                case '3':
                    Console.Clear();

                    while (true)
                    {
                        TitoloElimina();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(" 1) Elimina Studente");
                        Console.WriteLine(" 2) Esci");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine();
                        Console.Write("Seleziona un'opzione: ");
                        Console.ResetColor();

                        char scelta3 = Console.ReadKey().KeyChar;
                        if (scelta3 == '1')
                        {
                            List<Studente> studentiDaEliminare = studentiController.GetStudenti();
                            Console.Clear();
                            TitoloElimina();
                            Tabella();

                            foreach (var studente in studentiDaEliminare)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write($"{studente.Id,-5} ");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write($"{studente.Nome,-14} {studente.Cognome,-14} ");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"{studente.Eta,-4}");
                                Console.ResetColor();
                            }

                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine(new string('─', 47));
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("Inserisci l'Id da Eliminare: ");
                            Console.ResetColor();
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
                                    TitoloElimina();
                                    Tabella();
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.Write($"{studenteEsistente2.Id,-5} ");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write($"{studenteEsistente2.Nome,-14} {studenteEsistente2.Cognome,-14} ");
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine($"{studenteEsistente2.Eta,-4}");
                                    Console.WriteLine();

                                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                                    Console.WriteLine(new string('─', 47));
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Vuoi davvero procedere?");

                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.Write(" Conferma con [S]  |  Annulla con qualsiasi altro tasto: ");
                                    Console.ResetColor();

                                    char scelta31 = Console.ReadKey().KeyChar;
                                    if (scelta31 == 's' || scelta31 == 'S')
                                    {
                                        Console.Clear();
                                        studentiController.EliminaStudente(idEliminareParse);
                                        TitoloElimina();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Lo studente è stato eliminato.");
                                        Console.ResetColor();
                                        PremiTasto();
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

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{new string('=', 47)}");
                    Console.WriteLine("                LISTA STUDENTI");
                    Console.WriteLine($"{new string('=', 47)}");
                    Console.ResetColor();
                    Console.WriteLine();
                    List<Studente> listaStudenti = studentiController.GetStudenti();
                    Tabella();
                    foreach (var studente in listaStudenti)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{studente.Id,-5} ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"{studente.Nome,-14} {studente.Cognome,-14} ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{studente.Eta,-4}");
                        Console.Write($"{studente.Email,-20} {studente.Telefono,-20}");
                        foreach (var interesse in studente.Interessi)
                        {
                            Console.Write($"{interesse.NomeInteresse} ");
                        }
                        Console.WriteLine();
                        Console.ResetColor();
                    }
                    PremiTastoConTabella();
                    break;

                case '5':

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"{new string('=', 47)}");
                    Console.WriteLine("                     ESCI");
                    Console.WriteLine($"{new string('=', 47)}");

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Vuoi davvero procedere?");

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" Conferma con [S]  |  Annulla con qualsiasi altro tasto: ");
                    Console.ResetColor();

                    char scelta5 = Console.ReadKey(true).KeyChar;

                    if (char.ToUpper(scelta5) == 'S')
                    {
                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine();
                        Console.WriteLine("Chiusura in corso...");
                        Console.ResetColor();

                        string messaggio = "Grazie per aver utilizzato il programma.";
                        foreach (char c in messaggio)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(c);
                            Thread.Sleep(40);
                        }
                        Thread.Sleep(600);
                        Environment.Exit(0);
                    }
                    else
                        Console.Clear();
                    break;

                default:
                    Console.Clear();
                    TitoloGestioneStudenti();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Scelta non valida. Riprova.");
                    Console.ResetColor();
                    PremiTasto();
                    break;
            }
        }

        //──────────────────────────────────── FUNZIONI ──────────────────────────────────────
        void StampagialloConTitoloAggiunta(string messaggio)
        {
            Console.Clear();
            TitoloAggiunta();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(messaggio);
            Console.ResetColor();
        }
        void ErroreElimina(string messaggio)
        {
            Console.Clear();
            TitoloElimina();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(messaggio);
            Console.ResetColor();
            PremiTasto();
        }
        void TitoloElimina()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{new string('=', 47)}");
            Console.WriteLine("               ELIMINA STUDENTI");
            Console.WriteLine($"{new string('=', 47)}");
            Console.ResetColor();
            Console.WriteLine();
        }
        void StudenteDaModificare(int modificaIdParse, Studente studenteEsistente)
        {
            TitoloModifica();
            Tabella();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{modificaIdParse,-5} ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{studenteEsistente.Nome,-14} {studenteEsistente.Cognome,-14} ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{studenteEsistente.Eta,-4}");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(new string('─', 47));
            Console.ResetColor();
            Console.WriteLine();
        }
        void ErroreModifica(string messaggio)
        {
            Console.Clear();
            TitoloModifica();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(messaggio);
            Console.ResetColor();
            PremiTasto();
        }
        void TitoloModifica()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{new string('=', 47)}");
            Console.WriteLine("              MODIFICA STUDENTI");
            Console.WriteLine($"{new string('=', 47)}");
            Console.ResetColor();
            Console.WriteLine();
        }
        void ErroreAggiunta(string messaggio)
        {
            Console.Clear();
            TitoloAggiunta();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(messaggio);
            Console.ResetColor();
            PremiTasto();
        }
        void TitoloAggiunta()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{new string('=', 47)}");
            Console.WriteLine("              AGGIUNTA STUDENTI");
            Console.WriteLine($"{new string('=', 47)}");
            Console.ResetColor();
            Console.WriteLine();
        }
        void PremiTasto()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('─', 47));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Premi un tasto per tornare al menu...");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }
        void PremiTastoConTabella()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(new string('─', 47));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Premi un tasto per tornare al menu...");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }
        void Tabella()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"{"ID",-5} {"NOME",-14} {"COGNOME",-14} {"ETA",-4}");
            Console.WriteLine(new string('─', 47));
            Console.WriteLine();
            Console.ResetColor();
        }
        void TabellaSenzaId()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"{"NOME",-16} {"COGNOME",-16} {"ETA",-5}");
            Console.WriteLine(new string('─', 47));
            Console.WriteLine();
            Console.ResetColor();
        }
        void Menu()
        {
            TitoloGestioneStudenti();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  1) Aggiungi studente");
            Console.WriteLine("  2) Modifica studente");
            Console.WriteLine("  3) Elimina studente");
            Console.WriteLine("  4) Visualizza lista studenti");
            Console.WriteLine("  5) Esci");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Seleziona un'opzione: ");
            Console.ResetColor();
        }
        void TitoloGestioneStudenti()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{new string('=', 47)}");
            Console.WriteLine("              GESTIONE STUDENTI");
            Console.WriteLine($"{new string('=', 47)}");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}