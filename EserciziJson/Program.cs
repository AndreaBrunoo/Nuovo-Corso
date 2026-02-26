
using System.Net;
using Newtonsoft.Json;

//esercizio 1

string contenutoJson = File.ReadAllText(@"lastId.json");

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
File.WriteAllText(@"lastId.json", idAggiornato);

//esercizio 2
while (true)
{
    Console.Write("Nome: ");
    string nome = Console.ReadLine().Trim();
    Console.Write("Età: ");
    if (int.TryParse(Console.ReadLine(), out int età))
    {
        Console.Write("Presente (true/false)");
        if (bool.TryParse(Console.ReadLine().ToLower(), out bool presenza))
        {
            var partecipante = new
            {
                nome = 
            }

        }
        else
        {
            Console.WriteLine("Non è stato possibile aggiungere la presenza.");
            Console.WriteLine("Assicurati che sia composta da true o false.");
            continue;
        }
    }
    else
    {
        Console.WriteLine("Non è stato possibile aggiungere l'eta.");
        Console.WriteLine("Assicurati che sia composta da soli numeri.");
        continue;
    }
}