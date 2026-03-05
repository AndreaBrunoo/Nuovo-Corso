int[] numeri = { 1, 2, 3, 4, 5 }; // dichiarazione e inizializzazione dell'array
Console.WriteLine(numeri[0]); // accesso al primo elemento dell'array, indice 0 = primo elemento


List<int> numeriList = new List<int> { 1, 2, 3, 4, 5 }; // dichiarazione e inizializzazione della lista
Console.WriteLine(numeriList[0]); // accesso al primo elemento della lista, indice 0 = primo elemento


List<string> spesa = new List<string>{ "pane", "latte", "uova" }; // dichiarazione e inizializzazione della lista di stringhe 
Console.WriteLine(spesa[0]); // accesso al primo elemento della lista, indice 0 = primo elemento
spesa.Add ("formaggio"); // aggiunta di un elemento alla lista
Console.WriteLine(spesa[3]); // accesso al quarto elemento della lista, indice 3 = quarto elemento


Dictionary<int, string> eta = new Dictionary<int, string>
{
    { 25, "Mario" },
    { 30, "Luigi" },
    { 35, "Peach" }
};
Console.WriteLine(eta[30]); // accesso al valore associato alla chiave 30
eta.Add (40, "Toad"); // aggiunta di una nuova coppia chiave-valore


Console.Write ("ilserisci il tuo nome utente: ");
List<string> utenti = new List<string>();
string nome1 = Console.ReadLine();
utenti.Add(nome1); 
Console.Write ("ilserisci il tuo nome utente: ");
string nome2 = Console.ReadLine();
utenti.Add(nome2); 
Console.Write ("ilserisci il tuo nome utente: ");
string nome3 = Console.ReadLine();
utenti.Add(nome3); 
Console.WriteLine("Utenti registrati:");
Console.WriteLine("scrivi il numero del nome vuoi utilizzare?");
char scelta = Console.ReadKey().KeyChar;
Console.WriteLine (utenti[scelta -1 - '0']);
