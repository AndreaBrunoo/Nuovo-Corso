//Dichiarazione di una funzione void
void Stampa()
{
    //corpo della funzione
    Console.WriteLine("Ciao, sono una funzione void!");
}

//Dichiarazione di una funzione void con parametri
void NomeFunzione(string parametro1, int parametro2) //parametro1 è di tipo string, parametro2 è di tipo int
{
    //corpo della funzione
}

/*i parametri sono variabili che vengono passate alla funzione al momento della chiamata,
e possono essere utilizzate all'interno del corpo della funzione
per chiamare la funzione con dei parametri, basta scrivere il nome della funzione seguito da parentesi tonde
e all'interno delle parentesi tonde scrivere i valori dei parametri separati da virgole.*/

//Esempio di chiamata di una funzione con parametri
NomeFunzione("parametro1", 123); //argomenti: "parametro1" e 123
/* ci sono parametri ed argomenti, 
i parametri sono quelli dichiarati nella funzione, mentre 
gli argomenti sono quelli passati alla funzione al momento della chiamata.*/

//funzione con un valore di ritorno
int FunzioneInt()
{
    //corpo della funzione
    return 0; //valore di ritorno
}

string FunzioneString()
{
    //corpo della funzione
    return "Ciao"; //valore di ritorno
}

bool FunzioneBool()
{
    //corpo della funzione
    return true; //valore di ritorno
}   
/*il valore di ritorno è il risultato che la funzione restituisce al termine della sua esecuzione,
e può essere utilizzato per assegnare il risultato a una variabile o per eseguire altre operazioni.*/

//funzione con parametri e valore di ritorno
int Somma(int a, int b)
{
    return a + b; //ritorna la somma di a e b
}

int risultato = Somma(5, 10); //risultato sarà 15
Console.WriteLine($"La somma di 5 e 10 è: {risultato}");

/*le funzioni possono elaborare dati semplici come numeri o stringhe,
ma possono anche elaborare dati più complessi come oggetti o array.
possono anche richiamare altre funzioni al loro interno, 
creando così una struttura gerarchica di funzioni.*/

//esempio di funzione che manipola un array
int SommaArray(int[] numeri)
{
    int somma = 0;
    foreach (int numero in numeri)
    {
        somma += numero; //somma è uguale a somma più numero
    }
    return somma; //ritorna la somma di tutti i numeri nell'array
}

int[] numeri = { 1, 2, 3, 4, 5 }; //array di numeri da sommare
int risultato1 = SommaArray(numeri); //sommaArray sarà 15
Console.WriteLine($"La somma dell'array è: {risultato1}");

//esempio di funzione che richiama un'altra funzione
void StampaMessaggio(string messaggio)
{
    Console.WriteLine(messaggio); //stampa il messaggio passato come parametro
}
//funzione che richiama StampaMessaggio
void StampaMessaggioConPrefisso(string messaggio)
{
    string prefisso = "Prefisso: " + messaggio; //definisce un perfisso da aggiungere al messaggio
    StampaMessaggio($"{prefisso}{messaggio}"); //richiama la funzione StampaMessaggio con il nuovo messaggio
}
StampaMessaggioConPrefisso("Ciao, questo è un messaggio con prefisso!"); //stampa il prefisso

//funzione che calcola il doppio di un numero
int Doppio(int numero)
{
    return numero * 2; //ritorna il doppio del numero passato come parametro
}
//funzione che raddoppia il risultato di Doppio
int RaddoppiaDoppio(int numero)
{
    int doppio = Doppio(numero); //calcola il doppio del numero
    return doppio * 2; //ritorna il raddoppio del doppio
}
//funzione che raddoppia il raddoppio del doppio
int RaddoppiaRaddoppiaDoppio(int numero)    
{
    int raddoppiaDoppio = RaddoppiaDoppio(numero); //calcola il raddoppio del doppio del numero
    return raddoppiaDoppio * 2; //ritorna il raddoppio del raddoppio del doppio
}   
//funzione che stampa il risultato di RaddoppiaRaddoppiaDoppio
void StampaRaddoppiaRaddoppiaDoppio(int numero)  
{
    int raddoppiaRaddoppiaDoppio = RaddoppiaRaddoppiaDoppio(numero); //calcola il raddoppio del raddoppio del doppio del numero
    Console.WriteLine($"Il raddoppio del raddoppio del doppio di {numero} è: {raddoppiaRaddoppiaDoppio}"); //stampa il risultato
}

//Esercitazione 1
/*una funzione che unisce due liste di stringhe e restituisce una nuova lista contenente
tutti gli elementi di entrambe le liste ed una funzione che stampa la nuova lista.*/

List<string> UnisciListe(List<string> lista1, List<string> lista2)
{
    List<string> listaUnita = new List<string>(); //crea una nuova lista vuota
    listaUnita.AddRange(lista1); //aggiunge tutti gli elementi di lista1 alla nuova lista
    listaUnita.AddRange(lista2); //aggiunge tutti gli elementi di lista2 alla nuova lista
    return listaUnita; //ritorna la nuova lista unita
}
void StampaLista(List<string> lista)
{
    foreach (var item in lista)
    {
        Console.WriteLine(item);
    }
}

List<string> elenco1 = new List<string>
{
    "Nome1", 
    "Nome2"
};
List<string> elenco2 = new List<string>
{
    "Nome1", 
    "Nome2"
};

List<string>listaUnita = UnisciListe(elenco1, elenco2);
StampaLista(listaUnita);

/*una funzione tipo streang che si comporta come ReadLine avanzato 
che implementa la validazione dell'imput:
1. Trim: rimuove gli spazi all'inizio e alla fine della stringa
2. isNullOrEmpty: verifica se la stringa è null o vuota
3. ToLower: converte la stringa in minuscolo*/

Console.Clear();
string ReadLineAvanzato()
{
    while (true)
    {   
        Console.Write("Scrivi una frase: ");
        string? input = Console.ReadLine();
        input = input.Trim();
        if (!string.IsNullOrEmpty(input))
        {
            return input.ToLower();
        }
        Console.Clear();
        Console.WriteLine("Input non valido. Riprova.");
    }
}
string output = ReadLineAvanzato();
Console.Clear();
Console.WriteLine(output);

/*Una funzione che prende in imput un numero intero e restituisce il numero
 solo se è pari sennò un messaggio di errore*/

Console.Clear();
int InputNumeri()
{
    while (true)
    {   
        Console.Write("Scrivi un numero: ");
        string stringaNumero = Console.ReadLine();
        if (int.TryParse(stringaNumero, out int numeroParse))
        {
            if ( numeroParse % 2 == 0)
                return numeroParse;
            else
                Console.WriteLine($"Il numero {stringaNumero} non è pari");
        }
        Console.Clear();
        Console.WriteLine("Input non valido. Riprova.");
    }
}
int numeroParse = InputNumeri();
Console.Clear();
Console.WriteLine($"il numero {numeroParse} è pari");