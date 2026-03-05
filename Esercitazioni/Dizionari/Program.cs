//metodi di dizionari 
Dictionary<int,string> dizionario = new Dictionary<int, string>()
{
    {1, "uno"},
    {2, "due"},
    {3, "tre"}
};

//aggiunta al dizionario
dizionario.Add(4, "quattro");

//eccezzione la chiave esiste già deve essere gestita
dizionario.Add(1, "uno aggiornato");

//verifica se sia già presente la chiave nel dizionario
bool esisteChiave1 = dizionario.ContainsKey(1); //true
bool esisteChiave5 = dizionario.ContainsKey(5); //false

//verifica se sia già presente il valore nel dizionario
bool esisteValore1  = dizionario.ContainsValue("uno aggiornato"); //true
bool esisteValore5  = dizionario.ContainsValue("cinque"); //false

//verifica se un valore associato esiste già
if(dizionario.TryGetValue(1, out string valore))
{
    Console.WriteLine($" il valore associato alla chiave 1 è {valore}");
}
else
{
    Console.WriteLine("la chiave 1 esiste nel dizionario");
}

//modifica un valore già esistente
dizionario[1] = "uno modificato";
Console.WriteLine(dizionario[1]); // "uno modificato"

//rimuovere un valore dal dizionario
dizionario.Remove(2); // rimuove l'elemento con la chiave 2
dizionario.Remove(6); // non fa nulla

//rimuovere tutti gli elementi
dizionario.Clear(); //il dizionario è vuoto

//accedere ad una chiave ed ad un valore in un dizionario
foreach(KeyValuePair<int, string> kvp in dizionario)
{
    Console.WriteLine($"chiave: {kvp.Key}, Valore: {kvp.Value}");
}

//più semplicemente e migliore
foreach(var kvp in dizionario)
{
    Console.WriteLine($"chiave: {kvp.Key}, Valore: {kvp.Value}");
}

//diozionario di liste
Dictionary<int, List<string>> dizionarioListe = new Dictionary<int, List<string>>()
{
    {1, new List<string> { "nome" , "prezzo"} },
    {2, new List<string> { "nome"} },
    {3, new List<string> { "nome" , "prezzo" , "quantita"} }
};

//aggiunta di un elemento alla lista associata alla chiave 1
dizionarioListe[1].Add("quantita");

//nuvo elemento
dizionarioListe.Add(4, new List<string> {"nome" , "prezzo" , "quantita"} );

//stampare il dizionario
foreach ( var kvp in dizionarioListe )
{
    Console.WriteLine($"chiave: {kvp.Key}, valore: {string.Join(",", kvp.Value)}");
}

//dizionario di dizionari
Dictionary<int, Dictionary<string, string>> dizionarioDizionari = new Dictionary<int, Dictionary<string, string>>()
{
    { 1, new Dictionary<string, string>{ { "nome", "prodotto1"}, {"prezzo", "10" }}},
    { 2, new Dictionary<string, string>{ { "nome", "prodotto2"}, {"prezzo", "20" }}},
    { 3, new Dictionary<string, string>{ { "nome", "prodotto3"}, {"prezzo", "30" }}}
};

// aggiungo un elemento al dizionario alla chiave 1
dizionarioDizionari[1].Add("quantita", "100");

//aggiungere un nuovo elemento
dizionarioDizionari.Add(4, new Dictionary<string, string> {{"nome", "prodotto4" }, { "prezzo", "40" }, {"quantita", "100"}});

// stampare il dizionario di dizionari
foreach ( var kvp in dizionarioDizionari )
{
    Console.WriteLine($"chiave: {kvp.Key}");
    foreach (var inkvp in dizionarioDizionari )
    {
        Console.WriteLine($"chiave interna: {inkvp.Key}, valore interno: {inkvp.Value}");
    }
}
