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
Dictionary<string, string> numNome = new Dictionary<string, string>()
{
    { "3496266961", "andrea" },
    { "3333333333", "mario" },
    { "3456789012", "carmelo" },
};
bool continua = true;

while (continua)
{
    Console.Clear();
    Console.WriteLine("1. Aggiungi numero di telefono");
    Console.WriteLine("2. Modifica numero di telefono");
    Console.WriteLine("3. Rimuovi numero di telefono");
    Console.WriteLine("4. Visualizza rubrica");
    Console.WriteLine("5. Esci");
    string scelta = Console.ReadLine().Trim();

    switch (scelta)
    {
        case "1":
            Console.Clear();
            Console.WriteLine("Aggiungi il tuo numero di telefono");
            string numero = Console.ReadLine().Trim();
            Console.WriteLine("Aggiungi il tuo nome:");
            string nome = Console.ReadLine().Trim();
            bool numEsistente = numNome.ContainsKey(numero);
            if (numEsistente == true)
            {
                Console.WriteLine($"Questo numero: {numero} è già presente nella lista");
                Console.WriteLine("Vuoi modificarlo? y");
                string scelta1 = Console.ReadLine().Trim();
                if (scelta1 == "y" || scelta1 == "Y")
                {
                    Console.WriteLine("Scrivi il nuovo numero: ");
                    string numMod2 = Console.ReadLine().Trim();
                    numNome[numero] = numMod2;
                    Console.WriteLine(numNome[numero]);
                }
            }
            else
            {
                numNome.Add($"{numero}", $"{nome}");
            }
            break;


        case "2":
            Console.Clear();
            foreach (var kvp in numNome)
            {
                Console.WriteLine($"Numero: {kvp.Key}, Nome: {kvp.Value}");
            }
            Console.WriteLine("Scrivi il numero che vuoi modificare: ");
            string numDaMod = Console.ReadLine().Trim();
            bool numEsistenteMod = numNome.ContainsKey(numDaMod);
            if (numEsistenteMod == true)
            {
                Console.WriteLine("Scrivi il nuovo numero: ");
                string numMod = Console.ReadLine().Trim();
                numNome[numDaMod] = numMod;
                Console.WriteLine(numNome[numDaMod]);
            }
            else
            {
                Console.WriteLine($"Il numero {numDaMod} non è presente nella lista");
            }
            break;
        case "3":
            Console.Clear();
            foreach (var kvp in numNome)
            {
                Console.WriteLine($"Numero: {kvp.Key}, Nome: {kvp.Value}");
            }
            Console.WriteLine("Scrivi il numero che vuoi cancellare:");
            string numDaRimuovere = Console.ReadLine().Trim();
            numNome.Remove(numDaRimuovere);
            break;

        case "4":
            Console.Clear();
            foreach (var kvp in numNome)
            {
                Console.WriteLine($"Numero: {kvp.Key}, Nome: {kvp.Value}");
            }
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
