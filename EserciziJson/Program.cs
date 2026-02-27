
using Newtonsoft.Json;

//esercizio 1

/*string contenutoJson = File.ReadAllText(@"lastId.json");

var id = JsonConvert.DeserializeObject<dynamic>(contenutoJson);

int ultimoId = id.lastId;

var nuovoPartecipante = new
{
    id = ultimoId + 1,
    nome = $"Partecipante {ultimoId + 1}"
};

string nuovoPartecipanteJson = JsonConvert.SerializeObject(nuovoPartecipante, Formatting.Indented);

File.WriteAllText(@"partecipante.json", nuovoPartecipanteJson);

id.lastId = ultimoId + 1;

string idAggiornato = JsonConvert.SerializeObject(id, Formatting.Indented);
File.WriteAllText(@"lastId.json", idAggiornato);*/

//esercizio 2

// leggo il file lastId.json
string lastIdJson = File.ReadAllText(@"lastId.json");

// chiedo all'utente di inserire i dati del partecipante
Console.Write("Nome: ");
string nome = Console.ReadLine();
Console.Write("Età: ");
int eta = int.Parse(Console.ReadLine());
Console.Write("Presente (true/false): ");
bool presente = bool.Parse(Console.ReadLine());

// costruisco l'oggetto partecipante con i dati inseriti dall'utente
var nuovoPartecipante2 = new
{
    id = JsonConvert.DeserializeObject<dynamic>(lastIdJson).lastId + 1,
    nome = nome,
    eta = eta,
    presente = presente
};

// serializzo l'oggetto partecipante in una stringa json
string nuovoPartecipanteJson2 = JsonConvert.SerializeObject(nuovoPartecipante2, Formatting.Indented);
// scrivo la stringa json su un file
File.WriteAllText(@"partecipante.json", nuovoPartecipanteJson2);

// aggiorno il valore di lastId
var lastIdObj = JsonConvert.DeserializeObject<dynamic>(lastIdJson);
lastIdObj.lastId = (int)lastIdObj.lastId + 1;
// serializzo l'oggetto aggiornato in una stringa json
string updatedLastIdJson = JsonConvert.SerializeObject(lastIdObj, Formatting.Indented);
// scrivo la stringa json aggiornata su file
File.WriteAllText(@"lastId.json", updatedLastIdJson);

// stampo i dati del nuovo partecipante
Console.WriteLine($"Nuovo partecipante creato:");
Console.WriteLine($"ID: {nuovoPartecipante2.id}");
Console.WriteLine($"Nome: {nuovoPartecipante2.nome}");
Console.WriteLine($"Età: {nuovoPartecipante2.eta}");
Console.WriteLine($"Presente: {nuovoPartecipante2.presente}"); 