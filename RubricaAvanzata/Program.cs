/*
    
*/

Dictionary<string, DateTime> listaProdotti = new Dictionary<string, DateTime>
{
    { "latte", new DateTime(2026,02,25) },
    { "pane", new DateTime(2026,02,23) },
    { "biscotti", new DateTime(2026,02,22) },
    { "uova", new DateTime(2026,02,24) },
    { "acqua", new DateTime(2027,02,28) }
};
Dictionary<string, DateTime> listaProdottiScaduti = new Dictionary<string, DateTime>
{
    { "olive", new DateTime(2026,02,17) },
    { "wurstel", new DateTime(2026,02,21) }
};
DateTime oggi = DateTime.Today;
Console.Clear();

while (true)
{
    AggiornaListeScadenze();
    TitoloGestioneProdotti();
    Console.WriteLine("1. Aggiungi un nuovo prodotto");
    Console.WriteLine("2. Modifica la scadenza di un prodotto");
    Console.WriteLine("3. Elimina un prodotto");
    Console.WriteLine("4. Lista dei prodotti");
    Console.WriteLine("5. Scadenze imminenti");
    Console.WriteLine("6. Prodotti scaduti");
    Console.WriteLine("7. Esci");
    char scelta = Console.ReadKey().KeyChar;

    switch (scelta)
    {
        case '1':
            Console.Clear();

            while (true)
            {
                TitoloAggiungiProdotto();
                Console.WriteLine("1. Aggiungi un nuovo prodotto");
                Console.WriteLine("2. Esci");
                char scelta1 = Console.ReadKey().KeyChar;

                if (scelta1 == '1')
                {
                    Console.Clear();
                    TitoloAggiungiProdotto();
                    Console.Write("Inserisci il nome del prodotto: ");
                    string? nomeProdotto = Console.ReadLine().Trim().ToLower();
                    if (!listaProdotti.ContainsKey(nomeProdotto))
                    {
                        Console.WriteLine(" ");
                        Console.Write("Inserisci la data di scadenza (aaaa,mm,gg): ");
                        AggiuntaScadenza(listaProdotti, nomeProdotto, "Prodotto aggiunto con successo alla lista");
                    }
                    else
                    {
                        ErroreAggiungiProdotto("Il prodotto è già presente nella lista.");
                        TitoloAggiungiProdotto();
                        Console.WriteLine("Premi 's' per modificarne la data di scadenza o");
                        Console.Write("qualsiasi altro carattere per tornare indietro");
                        char scelta1p1 = Console.ReadKey().KeyChar;
                        if (scelta1p1 == 's' || scelta1p1 == 'S')
                        {
                            Console.Clear();
                            TitoloModificaProdotto();
                            ModificaScadenza(listaProdotti, nomeProdotto, "Prodotto modificato con successo.");
                        }
                        else
                            Console.Clear();
                    }
                }
                else if (scelta1 == '2')
                {
                    Console.Clear();
                    break;
                }
                else
                    ErroreAggiungiProdotto("Carattere non valido. Riprova.");
                continue;
            }
            break;

        case '2':
            Console.Clear();

            while (true)
            {
                TitoloModificaProdotto();
                Console.WriteLine("1. Modifica la scadenza di un prodotto");
                Console.WriteLine("2. Esci");
                char scelta2 = Console.ReadKey().KeyChar;

                if (scelta2 == '1')
                {
                    Console.Clear();
                    if (listaProdotti.Any())
                    {
                        TitoloModificaProdotto();
                        var listaProdottiOrdinatiPerNome = listaProdotti
                        .OrderBy(prodotto => prodotto.Key);
                        Tabella();
                        foreach (var prodotto in listaProdottiOrdinatiPerNome)
                        {
                            TimeSpan giorni = prodotto.Value - oggi;
                            Console.WriteLine($"{prodotto.Key,-20} {prodotto.Value.ToShortDateString(),-15}   {giorni.TotalDays,-3}");
                        }
                        Console.WriteLine(" ");
                        Console.WriteLine(new string('-', 47));
                        Console.WriteLine(" ");
                        Console.Write("Inserisci il nome del prodotto da modificare: ");
                        string? nomeProdottoDaModificare = Console.ReadLine().Trim().ToLower();
                        if (listaProdotti.ContainsKey(nomeProdottoDaModificare))
                        {
                            Console.Clear();
                            TitoloModificaProdotto();
                            ModificaScadenza(listaProdotti, nomeProdottoDaModificare, "Prodotto modificato con successo.");
                        }
                        else
                            ErroreModificaProdotto("Il prodotto non è presente nella lista. Riprova.");
                    }
                    else
                    {
                        TestoConClear($"{new string('-', 16)}LISTA PRODOTTI{new string('-', 17)}");
                        Console.WriteLine("La lista prodotti è vuota");
                    }
                }
                else if (scelta2 == '2')
                {
                    Console.Clear();
                    break;
                }
                else
                    ErroreModificaProdotto("Carattere non valido. Riprova.");
                continue;
            }
            break;

        case '3':
            Console.Clear();

            while (true)
            {
                TitoloEliminaProdotto();
                Console.WriteLine("1. Elimina un prodotto");
                Console.WriteLine("2. Esci");
                char scelta3 = Console.ReadKey().KeyChar;
                if (scelta3 == '1')
                {
                    Console.Clear();
                    if (listaProdotti.Any())
                    {
                        TitoloEliminaProdotto();
                        var listaProdottiOrdinatiPerNome = listaProdotti
                        .OrderBy(prodotto => prodotto.Key);
                        Tabella();
                        foreach (var prodotto in listaProdottiOrdinatiPerNome)
                        {
                            TimeSpan giorni = prodotto.Value - oggi;
                            Console.WriteLine($"{prodotto.Key,-20} {prodotto.Value.ToShortDateString(),-15}   {giorni.TotalDays,-3}");
                        }
                        Console.WriteLine(" ");
                        Console.WriteLine(new string('-', 47));
                        Console.WriteLine(" ");
                        Console.Write("Inerisci il prodotto da eliminare: ");
                        string? prodottoDaEliminare = Console.ReadLine().Trim().ToLower();

                        if (listaProdotti.ContainsKey(prodottoDaEliminare))
                        {
                            Console.Clear();
                            TitoloModificaProdotto();
                            Tabella();
                            TimeSpan giorni = listaProdotti[prodottoDaEliminare] - oggi;
                            Console.WriteLine($"{prodottoDaEliminare,-20} {listaProdotti[prodottoDaEliminare].ToShortDateString(),-15}   {giorni.TotalDays,-3}");
                            Console.WriteLine(" ");
                            Console.WriteLine("Sei sicuro di voler eliminare questo prodotto?");
                            Console.Write("Premi 's' per confermare o qualsiasi altro carattere per annullare: ");
                            char confermaEliminazione = Console.ReadKey().KeyChar;
                            if (confermaEliminazione == 's' || confermaEliminazione == 'S')
                            {
                                listaProdotti.Remove(prodottoDaEliminare);
                                ErroreEliminaProdotto($"Il prodotto {prodottoDaEliminare} è stato eliminato.");
                            }
                            else
                                ErroreEliminaProdotto("Eliminazione annullata. Riprova.");
                        }
                        else
                            ErroreEliminaProdotto("Il prodotto non è presente nella lista. Riprova.");
                    }
                    else
                    {
                        TestoConClear($"{new string('-', 16)}LISTA PRODOTTI{new string('-', 17)}");
                        Console.WriteLine("La lista prodotti è vuota");
                    }
                }
                else if (scelta3 == '2')
                {
                    Console.Clear();
                    break;
                }
                else
                    ErroreEliminaProdotto("Carattere non valido. Riprova.");
                continue;
            }
            break;

        case '4':
            TestoConClear($"{new string('-',15)} LISTA PRODOTTI {new string('-',16)}");
            StampaListaPerChiave(listaProdotti);
            break;

        case '5':
            TestoConClear($"{new string('-',13)}SCADENZE IMMINENTI{new string('-',14)}");
            StampaScadenzeImminenti(listaProdotti);
            break;

        case '6':
            Console.Clear();

            while (true)
            {
                TitoloProdottiScaduti();
                Console.WriteLine("1. Visualizza prodotti scaduti");
                Console.WriteLine("2. Pulisci la lista prodotti scaduti");
                Console.WriteLine("3. Esci");
                char scelta6 = Console.ReadKey().KeyChar;

                if (scelta6 == '1')
                {
                    if (listaProdottiScaduti.Any())
                    {
                        StampaListaPerValore(listaProdottiScaduti);
                        PremiTasto();
                        Console.Clear();
                    }
                    else
                        ErroreProdottiScaduti("La lista prodotti scaduti è vuota.");
                }
                else if (scelta6 == '2')
                {
                    if (listaProdottiScaduti.Any())
                    {
                        Console.Clear();
                        TitoloProdottiScaduti();
                        Console.WriteLine("Sei sicuro di voler pulire la lista prodotti scaduti?");
                        Console.Write("Premi 's' per confermare o qualsiasi altro carattere per tornare al menu. ");
                        char scelta6p1 = Console.ReadKey().KeyChar;
                        if (scelta6p1 == 's' || scelta6p1 == 'S')
                        {
                            listaProdottiScaduti.Clear();
                            ErroreProdottiScaduti("Pulizia lista prodotti scaduti avvenuta con successo.");
                        }
                        else
                            Console.Clear();
                    }
                    else
                        ErroreProdottiScaduti("La lista prodotti scaduti è già vuota.");
                }
                else if (scelta6 == '3')
                {
                    Console.Clear();
                    break;
                }
                else
                    ErroreProdottiScaduti("Carattere non valido. Riprova.");
                continue;
            }
            break;

        case '7':
            TestoConClear($"{new string('-', 20)} ESCI {new string('-', 21)}");
            Console.WriteLine("Sei sicuro di voler uscire?");
            Console.Write("Premi 's' per confermare o qualsiasi altro carattere per tornare al menu.");
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
            TitoloGestioneProdotti();
            Console.WriteLine("Scelta non valida. Riprova.");
            break;
    }
}

void TestoConClear(string messaggio)
{
    Console.Clear();
    Console.WriteLine(" ");
    Console.WriteLine(messaggio);
    Console.WriteLine(" ");
}

void ErroreProdottiScaduti(string messaggio)
{
    Console.Clear();
    TitoloProdottiScaduti();
    Console.WriteLine(messaggio);
}

void TitoloProdottiScaduti()
{
    Console.WriteLine(" ");
    Console.WriteLine($"{new string('-', 15)} PRODOTTI SCADUTI {new string('-', 14)}");
    Console.WriteLine(" ");
}

void ErroreEliminaProdotto(string messaggio)
{
    Console.Clear();
    TitoloEliminaProdotto();
    Console.WriteLine(messaggio);
}

void TitoloEliminaProdotto()
{
    Console.WriteLine(" ");
    Console.WriteLine($"{new string('-', 15)} ELIMINA PRODOTTI {new string('-', 14)}");
    Console.WriteLine(" ");
}

void ErroreModificaProdotto(string messaggio)
{
    Console.Clear();
    TitoloModificaProdotto();
    Console.WriteLine(messaggio);
}

void ModificaScadenza(Dictionary<string, DateTime> listaProdotti, string nomeProdottoDaModificare, string messaggio)
{
    Console.Write("Inserisci la nuova data di scadenza (aaaa,mm,gg): ");
    string? nuovaScadenza = Console.ReadLine().Trim().ToLower();
    if (DateTime.TryParse(nuovaScadenza, out DateTime nuovaScadenzaParse))
    {
        if (nuovaScadenzaParse < oggi.AddDays(-1))
        {
            ErroreModificaProdotto("Il prodotto è già scaduto. Riprova.");
        }
        else
        {
            listaProdotti[nomeProdottoDaModificare] = nuovaScadenzaParse;
            ErroreModificaProdotto(messaggio);
            Console.WriteLine(" ");
            Tabella();
            TimeSpan giorni = nuovaScadenzaParse - oggi;
            Console.WriteLine($"{nomeProdottoDaModificare,-20} {nuovaScadenzaParse.ToShortDateString(),-15}   {giorni.TotalDays,-3}");
        }
    }
    else
    {
        ErroreModificaProdotto("Data non valida. Riprova.");
    }
}

void TitoloModificaProdotto()
{
    Console.WriteLine(" ");
    Console.WriteLine($"{new string('-', 14)} MODIFICA PRODOTTO {new string('-', 14)}");
    Console.WriteLine(" ");
}

void ErroreAggiungiProdotto(string messaggio)
{
    Console.Clear();
    TitoloAggiungiProdotto();
    Console.WriteLine(messaggio);
}

void AggiuntaScadenza(Dictionary<string, DateTime> listaProdotti, string nomeProdotto, string messaggio)
{
    string? nuovaDataScadenza = Console.ReadLine().Trim().ToLower();
    if (DateTime.TryParse(nuovaDataScadenza, out DateTime nuovaDataScadenzaParse))
    {
        if (nuovaDataScadenzaParse < oggi)
        {
            ErroreAggiungiProdotto("Il prodotto è già scaduto. Riprova.");
        }
        else
        {
            listaProdotti.Add(nomeProdotto, nuovaDataScadenzaParse);
            ErroreAggiungiProdotto(messaggio);
            Console.WriteLine(" ");
            Tabella();
            TimeSpan giorni = nuovaDataScadenzaParse - oggi;
            Console.WriteLine($"{nomeProdotto,-20} {nuovaDataScadenzaParse.ToShortDateString(),-15}   {giorni.TotalDays,-3}");
        }
    }
    else
    {
        ErroreAggiungiProdotto("Data non valida. Riprova.");
    }
}

void TitoloAggiungiProdotto()
{
    Console.WriteLine(" ");
    Console.WriteLine($"{new string('-', 14)} AGGIUNGI PRODOTTI {new string('-', 14)}");
    Console.WriteLine(" ");
}

void TitoloGestioneProdotti()
{
    Console.WriteLine(" ");
    Console.WriteLine($"{new string('-', 14)} GESTIONE PRODOTTI {new string('-', 14)}");
    Console.WriteLine(" ");
}

void StampaListaPerChiave(Dictionary<string, DateTime> ListaProdotti)
{
    if (listaProdotti.Any())
    {
        var listaProdottiOrdinatiPerNome = listaProdotti
        .OrderBy(prodotto => prodotto.Key);
        Tabella();
        foreach (var prodotto in listaProdottiOrdinatiPerNome)
        {
            TimeSpan giorni = prodotto.Value - oggi;
            Console.WriteLine($"{prodotto.Key,-20} {prodotto.Value.ToShortDateString(),-15}   {giorni.TotalDays,-3}");
        }
        PremiTasto();
        Console.Clear();
    }
    else
    {
        Console.WriteLine("La lista prodotti è vuota");
    }
}

void StampaScadenzeImminenti(Dictionary<string, DateTime> ListaProdotti)
{
    var scadenzeImminenti = listaProdotti
    .Where(prodotto => prodotto.Value >= oggi && prodotto.Value <= oggi.AddDays(7))
    .OrderBy(prodotto => prodotto.Value);

    if (scadenzeImminenti.Any())
    {
        Tabella();
        foreach (var prodotto in scadenzeImminenti)
        {
            TimeSpan giorni = prodotto.Value - oggi;
            Console.WriteLine($"{prodotto.Key,-20} {prodotto.Value.ToShortDateString(),-15}   {giorni.TotalDays,-3}");
        }
        PremiTasto();
        Console.Clear();
    }
    else
    {
        Console.WriteLine("Non ci sono prodotti in scadenza nei prossimi 7 giorni.");
    }
}

void StampaListaPerValore(Dictionary<string, DateTime> ListaProdottiScaduti)
{
    Console.Clear();
    TitoloProdottiScaduti();
    var listaProdottiScadutiOrdinata = listaProdottiScaduti
    .OrderBy(prodotto => prodotto.Value);
    Tabella();
    foreach (var prodotto in listaProdottiScadutiOrdinata)
    {
        TimeSpan giorni = prodotto.Value - oggi;
        Console.WriteLine($"{prodotto.Key,-20} {prodotto.Value.ToShortDateString(),-15}   {giorni.TotalDays,-3}");
    }
}

void Tabella()
{
    Console.WriteLine($"{"Prodotto",-20} {"Scadenza",-15} {"Giorni",-10}");
    Console.WriteLine(new string('-', 47));
    Console.WriteLine(" ");
}

void PremiTasto()
{
    Console.WriteLine(" ");
    Console.WriteLine(new string('-', 47));
    Console.Write("Premi un tasto per tornare al menu...");
    Console.ReadKey();
}

void AggiornaListeScadenze()
{
    var scaduti = listaProdotti
        .Where(prodotto => prodotto.Value < oggi)
        .ToList();

    foreach (var prodotto in scaduti)
    {
        listaProdottiScaduti[prodotto.Key] = prodotto.Value;
        listaProdotti.Remove(prodotto.Key);
    }
}