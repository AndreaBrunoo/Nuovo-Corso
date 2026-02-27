
using Newtonsoft.Json;

string jsonId = File.ReadAllText(@"id.json");
string jsonContatto = File.ReadAllText(@"contatto.json");

Id id = JsonConvert.DeserializeObject<Id>(jsonId);
List<Contatto> contatti = JsonConvert.DeserializeObject <List<Contatto>>(jsonContatto);

foreach (var contatto in contatti)
{
    Console.WriteLine($"id: { contatto.id}");
    Console.WriteLine($"id: { contatto.nome}");
    Console.WriteLine($"id: { contatto.cognome}");
    Console.WriteLine($"id: { contatto.telefono}");
    Console.WriteLine($"id: { contatto.presente}");
    Console.WriteLine("Interesse");
    foreach (var interess in contatto.interessi)
    {
        Console.WriteLine(interess);
    }
}

public class Id
{
    public int id { get; set; }
}

public class Contatto
{
    public int id { get; set; }
    public string nome { get; set; }
    public string cognome { get; set; }
    public string telefono { get; set; }
    public bool presente { get; set; }
    public List<string> interessi { get; set; } =new();
}