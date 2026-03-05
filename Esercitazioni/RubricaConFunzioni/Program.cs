/*
    Crea un dizionario che associa un numero di telefono ad un nome.
    Permette all'utente di inserire un numero di telefono e li aggiunge al dizionario
    Permette all'utente di modificare un numero di telefono esistente
    Permette all'utente di rimuovere un numero di telefono esistente
    Nel caso di inserimento di un nome già esistente, chiedi se si vuole aggiornare il numero di telefono associato a quel nome
    Nel caso di modifica e di cancellazione deve prima stampare la rubrica in modo da poter visualizzare i nomi e i numeri di telefono associati
    Nel caso di eliminazione di un nome che non esiste, deve stampare un messaggio di errore
*/
Console.Clear();
Dictionary<string, string> rubrica = new Dictionary<string, string>()
{
    { "3496266961", "andrea" },
    { "3333333333", "mario" },
    { "3456789012", "carmelo" },
};

bool continua = true;
while (continua)
{
    Console.Clear();
    Console.WriteLine("1. Aggiungi nuovo contatto");
    Console.WriteLine("2. Modifica contatto esistente");
    Console.WriteLine("3. Rimuovi contatto");
    Console.WriteLine("4. Visualizza rubrica");
    Console.WriteLine("5. Esci");
    string scelta = LeggiInput("Inserisci la tua scelta");

    switch (scelta)
    {
        case "1":
            AggiuntaContatto(rubrica);
            break;
        case "2":
            ModificaContatto(rubrica);
            break;
        case "3":
            RimuoviContatto(rubrica);
            break;
        case "4":
            ListaContatti(rubrica);
            break;

        case "5":
            Console.Clear();
            continua = false;
            break;

        default:
            Console.Clear();
            Console.WriteLine("Operazione non valida.");
            break;
    }
}

void AggiuntaContatto(Dictionary<string, string> rubrica)
{
    Console.Clear();
    
    string numero = LeggiInput("Aggiungi il tuo numero telefonico: ");
    string nome = LeggiInput("Aggiungi il tuo nome: ");
    bool numEsistente = rubrica.ContainsKey(numero);
    if (numEsistente == true)
    {
        Console.WriteLine($"Questo numero: {numero} è già presente nella lista");
        string scelta1 = LeggiInput("Vuoi modificarlo? y ");
        if (scelta1 == "y" || scelta1 == "Y")
        {
            string numeroModificato = LeggiInput("Scrivi il nuovo numero: ");
            rubrica.Remove(numero);
            rubrica.Add($"{numeroModificato}", $"{nome}");
            Console.WriteLine(rubrica[numeroModificato]);
        }
    }
    else
    {
        rubrica.Add($"{numero}", $"{nome}");
    }
}

string LeggiInput(string messaggio)
{
    Console.Write(messaggio);
    string input = Console.ReadLine().Trim();
    Console.Clear();
    return input;
}

void ModificaContatto(Dictionary<string, string> rubrica)
{
    Console.Clear();
    foreach (var kvp in rubrica)
    {
        Console.WriteLine($"Numero: {kvp.Key}, Nome: {kvp.Value}");
    }
    string numeroDaModificare = LeggiInput("Scrivi il numero che vuoi modificare: ");
    bool numEsistenteMod = rubrica.ContainsKey(numeroDaModificare);
    if (numEsistenteMod == true)
    {
        string nuovoNumero = LeggiInput("Scrivi il nuovo numero: ");
        rubrica[numeroDaModificare] = nuovoNumero;
        Console.WriteLine(rubrica[numeroDaModificare]);
    }
    else
    {
        Console.WriteLine($"Il numero {numeroDaModificare} non è presente nella lista");
    }
}

void RimuoviContatto(Dictionary<string, string> numNome)
{
    Console.Clear();
    foreach (var kvp in numNome)
    {
        Console.WriteLine($"Numero: {kvp.Key}, Nome: {kvp.Value}");
    }
    string numDaRimuovere = LeggiInput("Scrivi il numero che vuoi cancellare:");
    numNome.Remove(numDaRimuovere);

}

void ListaContatti(Dictionary<string, string> rubrica)
{
    Console.Clear();
    foreach (var kvp in rubrica)
    {
        Console.WriteLine($"Numero: {kvp.Key}, Nome: {kvp.Value}");
    }
}