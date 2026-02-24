/*
Implementare la suddivisione delle scadenze in:
- Scadenze imminenti (entro 7 giorni)
- Scadenze passate (già scadute)
e utilizzare un nuovo dizionario chiamato `prodottiScaduti` contenente i prodotti scaduti,
eliminandoli dal dizionario principale per visualizzarli in un menu dedicato.
Nel menu prodotti scaduti creare due sottomenu:
- Visualizza prodotti scaduti
- Pulisci prodotti scaduti 
*/

Dictionary<string, DateTime> listaProdotti = new Dictionary<string, DateTime>
{
    { "latte", new DateTime(2026,02,25) },
    { "pane", new DateTime(2026,02,17) },
    { "biscotti", new DateTime(2026,02,20) },
    { "uova", new DateTime(2026,02,16) },
    { "acqua", new DateTime(2027,02,28) }
};

while (true)
{
    Console.Clear();
    Console.WriteLine(" ");
    Console.WriteLine("--------------GESTIONE PRODOTTI--------------");
    Console.WriteLine(" ");
    Console.WriteLine("1. Aggiungi un nuovo prodotto");
    Console.WriteLine("2. Modifica la scadenza di un prodotto");
    Console.WriteLine("3. Elimina un prodotto");
    Console.WriteLine("4. Lista dei prodotti");
    Console.WriteLine("5. Prossime scadenze");
    Console.WriteLine("6. Esci");
    char scelta = Console.ReadKey().KeyChar;

    switch (scelta)
    {
        case '1':
            Console.Clear();

            while (true)
            {
                Console.WriteLine(" ");
                Console.WriteLine("--------------AGGIUNGI PRODOTTI--------------");
                Console.WriteLine(" ");
                Console.WriteLine("1. Aggiungi un nuovo prodotto");
                Console.WriteLine("2. Esci");
                char scelta1 = Console.ReadKey().KeyChar;

                if (scelta1 == '1')
                {
                    Console.Clear();
                    Console.WriteLine(" ");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine(" ");
                    Console.Write("Inserisci il nome del prodotto: ");
                    string? nomeProdotto = Console.ReadLine().Trim().ToLower();
                    if (!listaProdotti.ContainsKey(nomeProdotto))
                    {
                        Console.WriteLine(" ");
                        Console.Write("Inserisci la data di scadenza (aaaa,mm,gg): ");
                        string? dataScadenza = Console.ReadLine().Trim().ToLower();
                        if (DateTime.TryParse(dataScadenza, out DateTime dataScadenzaParse))
                        {
                            if (dataScadenzaParse < DateTime.Now)
                            {
                                Console.Clear();
                                Console.WriteLine(" ");
                                Console.WriteLine("Il prodotto è già scaduto. Riprova.");
                                continue;
                            }
                            else
                            {
                                listaProdotti.Add(nomeProdotto, dataScadenzaParse);
                                Console.Clear();
                                Console.WriteLine(" ");
                                Console.WriteLine($"Il prodotto {nomeProdotto} è stato aggiunto con scadenza il {dataScadenzaParse}");
                                continue;
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine(" ");
                            Console.WriteLine("Data non valida. Riprova.");
                            continue;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(" ");
                        Console.WriteLine("Il prodotto è già presente. Riprova.");
                        continue;
                    }
                }
                else if (scelta1 == '2')
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(" ");
                    Console.WriteLine("Carattere non valido. Riprova.");
                    continue;
                }
            }
            break;

        case '2':
            Console.Clear();

            while (true)
            {
                Console.WriteLine(" ");
                Console.WriteLine("--------------MODIFICA SCADENZA--------------");
                Console.WriteLine(" ");
                Console.WriteLine("1. Modifica la scadenza di un prodotto");
                Console.WriteLine("2. Esci");
                char scelta2 = Console.ReadKey().KeyChar;

                if (scelta2 == '1')
                {
                    Console.Clear();
                    Console.WriteLine(" ");
                    Console.WriteLine("---------------LISTA PRODOTTI---------------");
                    Console.WriteLine(" ");
                    foreach (var prodotto in listaProdotti)
                    {
                        Console.WriteLine($"Il prodotto {prodotto.Key} scade il {prodotto.Value}");
                    }
                    Console.WriteLine(" ");
                    Console.Write("Inserisci il nome del prodotto da modificare: ");
                    string? nomeProdottoDaModificare = Console.ReadLine().Trim().ToLower();
                    if (listaProdotti.ContainsKey(nomeProdottoDaModificare))
                    {
                        Console.WriteLine(" ");
                        Console.Write("Inserisci la nuova scadenza (aaaa,mm,gg): ");
                        string? nuovaScadenza = Console.ReadLine().Trim().ToLower();
                        if (DateTime.TryParse(nuovaScadenza, out DateTime nuovaScadenzaParse))
                        {
                            if (nuovaScadenzaParse < DateTime.Now.AddDays(-1))
                            {
                                Console.Clear();
                                Console.WriteLine(" ");
                                Console.WriteLine("Il prodotto è già scaduto. Riprova.");
                                continue;
                            }
                            listaProdotti[nomeProdottoDaModificare] = nuovaScadenzaParse;
                            Console.Clear();
                            Console.WriteLine(" ");
                            Console.WriteLine($"Il prodotto {nomeProdottoDaModificare} è stato aggiornato con scadenza il {nuovaScadenzaParse}");
                            continue;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine(" ");
                            Console.WriteLine("Scadenza non valida. Riprova.");
                            continue;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(" ");
                        Console.WriteLine("Il prodotto non è presente nella lista. Riprova.");
                        continue;
                    }
                }
                else if (scelta2 == '2')
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(" ");
                    Console.WriteLine("Carattere non valido. Riprova.");
                    continue;
                }
            }
            break;

        case '3':
            Console.Clear();

            while (true)
            {
                Console.WriteLine(" ");
                Console.WriteLine("--------------ELIMINA PRODOTTO--------------");
                Console.WriteLine(" ");
                Console.WriteLine("1. Elimina un prodotto");
                Console.WriteLine("2. Esci");
                char scelta3 = Console.ReadKey().KeyChar;
                if (scelta3 == '1')
                {
                    Console.Clear();
                    Console.WriteLine(" ");
                    Console.Write("Inerisci il prodotto da eliminare: ");
                    string? prodottoDaEliminare = Console.ReadLine().Trim().ToLower();

                    if (listaProdotti.ContainsKey(prodottoDaEliminare))
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine($"Il prodotto {prodottoDaEliminare} scade il {listaProdotti[prodottoDaEliminare]}");
                        Console.WriteLine(" ");
                        Console.WriteLine("Sei sicuro di voler eliminare questo prodotto?");
                        Console.Write("Premi 's' per confermare o qualsiasi altro carattere per annullare: ");
                        char confermaEliminazione = Console.ReadKey().KeyChar;
                        if (confermaEliminazione == 's' || confermaEliminazione == 'S')
                        {
                            listaProdotti.Remove(prodottoDaEliminare);
                            Console.Clear();
                            Console.WriteLine(" ");
                            Console.WriteLine($"Il prodotto {prodottoDaEliminare} è stato eliminato.");
                            continue;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine(" ");
                            Console.WriteLine("Eliminazione annullata. Riprova.");
                            continue;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(" ");
                        Console.WriteLine("Il prodotto non è presente nella lista. Riprova.");
                        continue;
                    }

                }
                else if (scelta3 == '2')
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(" ");
                    Console.WriteLine("Carattere non valido. Riprova.");
                    continue;
                }
            }
            break;

        case '4':
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine("---------------LISTA PRODOTTI---------------");
            Console.WriteLine(" ");
            foreach (var prodotto in listaProdotti)
            {
                Console.WriteLine($"Il prodotto {prodotto.Key} scade il {prodotto.Value}");
            }
            Console.WriteLine(" ");
            Console.Write("Premi un tasto per tornare al menu...");
            Console.ReadKey();
            break;

        case '5':
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine("-------------PROSSIME SCADENZE-------------");
            Console.WriteLine(" ");
            var prossimeScadenze = listaProdotti.Where(prodotto => prodotto.Value <= DateTime.Now.AddDays(7)).OrderBy(prodotto => prodotto.Value);
            if (prossimeScadenze.Any())
            {
                foreach (var prodotto in prossimeScadenze)
                {
                    Console.WriteLine($"Il prodotto {prodotto.Key} scade il {prodotto.Value}");
                }
            }
            else
            {
                Console.WriteLine("Non ci sono prodotti in scadenza nei prossimi 7 giorni.");
            }
            Console.WriteLine(" ");
            Console.WriteLine("Premi un tasto per tornare al menu...");
            Console.ReadKey();
            break;

        case '6':
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine("------------------------ESCI------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("Sei sicuro di voler uscire?");
            Console.Write("Premi 's' per confermare o qualsiasi altro carattere per tornare al menu.");
            char scelta6 = Console.ReadKey().KeyChar;
            if (scelta6 == 's' || scelta6 == 'S')
            {
                Console.Clear();
                Console.WriteLine(" ");
                Console.WriteLine("Arrivederci!");
                Console.WriteLine(" ");
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                continue;
            }
            break;

        default:
            Console.WriteLine("Scelta non valida. Riprova.");
            break;
    }

}