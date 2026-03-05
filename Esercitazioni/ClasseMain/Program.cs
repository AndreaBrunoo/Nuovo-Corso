using Newtonsoft.Json;
class Program
{
    //La firma del metodo Main è sempre questa con un arrey di stringhe che raccoglie i metodi della console (es dotnet run ...)
    static void Main(string[] args)
    {
        //codice del main
        // devo creare un'istanza della classe LastIdController per poter utilizzare il metodo GetNextId, che è un metodo di istanza
        LastId lastid = new LastId();
        LastIdController lastIdController = new LastIdController();
        // devo creare un'istanza della classe LastIdController per poter utilizzare il metodo GetNextId, che è un metodo di istanza
        int nextId = lastIdController.GetNextId();
        Console.WriteLine($"Il prossimo ID è: {nextId}");
    }
}