/*DateTime today = DateTime.Today; // oggi 
Console.WriteLine($"oggi è {today}");
Console.WriteLine($"oggi è {today.ToShortDateString()}"); // in numeri
Console.WriteLine($"oggi è {today.ToLongDateString()}"); // a parole
Console.WriteLine($"oggi è {today.ToString("dd/MM/yyyy")}");

string dateString = "2024-12-31"; // Data in formato stringa
DateTime date = DateTime.Parse(dateString); // Converte la stringa in un oggetto DateTime
Console.WriteLine($"La data convertita è: {date}");

string dateString2 = "2024-12-31"; // Data in formato stringa
if (DateTime.TryParse(dateString2, out DateTime date2))
{
    Console.WriteLine($"La data convertita è: {date2}");
}
else
{
    Console.WriteLine("La conversione della data non è riuscita.");
}

DateTimeOffset now = DateTimeOffset.UtcNow;
long timestamp = now.ToUnixTimeSeconds();
Console.WriteLine($"Il timestamp attuale è: {timestamp}");

DateTime domani = today.AddDays(1);
Console.WriteLine($"Domani è: {domani}");
DateTime ieri = today.AddDays(-1);
Console.WriteLine($"Ieri era: {ieri:dddd}");

TimeSpan timeSpan = new TimeSpan(2, 0, 0, 0); // 2 giorni
DateTime traDueGiorni = today.Add(timeSpan);
Console.WriteLine($"Tra due giorni sarà: {traDueGiorni}");

DateTime domani2 = today.AddDays(1); // Domani
// è possibile usare AddMonths, AddYears, AddHours, AddMinutes, AddSeconds, AddMilliseconds, AddTicks
int result = DateTime.Compare(today, domani2);
if (result < 0)
{
    Console.WriteLine("La prima data è precedente alla seconda.");
}
else if (result == 0)
{
    Console.WriteLine("Le due date sono uguali.");
}
else
{
    Console.WriteLine("La prima data è successiva alla seconda.");
}

/*Gestione della scadenza di prodotti deperibili.
Chiede all'utente di inserire il nome di un prodotto e la sua data di scadenza.
La data deve essere convertita in oggetto DateTime e devono essere gestiti eventuali errori di conversione.
Aggiunge il prodotto a un dizionario Prodotti, dove la chiave è il nome del prodotto e il valore è la data di scadenza.
Calcola quanti giorni mancano alla scadenza del prodotto.
Stampa un indicazione a fianco al prodotto che indica se:
il prodotto è scaduto,
sta per scadere (entro 3 giorni) o
è ancora utilizzabile.
Stampa la data di scadenza in un formato leggibile (es. "31 dicembre 2024") scritta tra parentesi.*/

/*Dictionary<string, DateTime> Prodotti = new Dictionary<string, DateTime>();

while (true)
{
    Console.WriteLine("Inserisci il nome del prodotto (o 'exit' per uscire):");
    string nomeProdotto = Console.ReadLine();
    if (nomeProdotto.ToLower() == "exit")
        break;

    Console.WriteLine("Inserisci la data di scadenza (formato: dd/MM/yyyy):");
    string dataScadenzaString = Console.ReadLine();

    if (DateTime.TryParse(dataScadenzaString, out DateTime dataScadenza))
    {
        Prodotti[nomeProdotto] = dataScadenza;
        TimeSpan giorniMancanti = dataScadenza - DateTime.Today;

        string stato;
        if (giorniMancanti.TotalDays < 0)
            stato = "scaduto";
        else if (giorniMancanti.TotalDays <= 3)
            stato = "sta per scadere";
        else
            stato = "ancora utilizzabile";

        Console.WriteLine($"{nomeProdotto} ({dataScadenza:dd MMMM yyyy}) è {stato}.");
    }
    else
    {
        Console.WriteLine("Errore nella conversione della data. Riprova.");
    }
}

Console.WriteLine("Prodotti e le loro scadenze:"); 
foreach (var prodotto in Prodotti)
{
    Console.WriteLine($"{prodotto.Key} - scadenza: ({prodotto.Value:dd MMMM yyyy})");
}
*/
Dictionary<string, string> listaProdotti = new Dictionary<string, string>
{
    {"yogurt", "18-02-2026"},
    {"latte" , "24-02-2026"},
    {"biscotti" , "16-02-2026"}
};
bool uscita = false;
while(!uscita)
{
    Console.WriteLine("1. Aggiungi prodotti");
    Console.WriteLine("2. Elenco prodotti");
    Console.WriteLine("3. Elenco prodotti in scadenza");
    Console.WriteLine("4. esci");
    string? risposta = Console.ReadLine();
        switch(risposta)
        {
            case "1":
                bool uscita2 = true;
                while(!uscita2)
                {
                    Console.WriteLine("1. Aggiungi il nome del prodotto");
                    Console.WriteLine("2. Indietro");
                    string? scelta1 = Console.ReadLine();
                    if(scelta1 == "1")
                    {
                        Console.WriteLine("Aggiungi il nome del prodotto: ");
                        string? nomeProdotto = Console.ReadLine();
                        Console.WriteLine("Aggiungi la sua data di scadenza: dd/mm/aaaa");
                        string? dataScadenza = Console.ReadLine();
                        DateTime.TryParse( dataScadenza, out DateTime scadenza);
                        TimeSpan giorniMancanti = scadenza - DateTime.Today;
                        if (giorniMancanti.TotalDays >= 0)
                        {
                            Console.WriteLine($"Il prodotto è stato aggiunto alla lista");
                            listaProdotti[nomeProdotto] = dataScadenza;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine($"Il prodotto non è stato aggiunto perchè scaduto");
                            continue;
                        }
                    }
                    else if (scelta1 == "2")
                    {
                        uscita2 = false;
                    }

                }
                break;

            default:
                break;

        }
}