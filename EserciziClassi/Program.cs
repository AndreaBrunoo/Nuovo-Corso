
class Program
{
    static void Main(string[] args)
    {
        
        Menu();
        char scelta = Console.ReadKey().KeyChar;
        StudentiController studentiController = new StudentiController();

        switch (scelta)
        {
            case '1':
                Studente nuovoStudente = new Studente();
                Console.Write("Inserisci il tuo nome: ");
                nuovoStudente.Nome = Console.ReadLine();
                Console.Write("Inserisci il tuo cognome: ");
                nuovoStudente.Cognome = Console.ReadLine();
                Console.Write("Inserisci la tua età: ");
                nuovoStudente.Eta = int.Parse(Console.ReadLine());

                studentiController.AggiungiStudente(nuovoStudente);
                break;

            case '2':
                List<Studente> studenti = studentiController.GetStudenti();
                foreach (var studente in studenti)
                {
                    Console.WriteLine($"ID: {studente.Id}, Nome: {studente.Nome}, Cognome: {studente.Cognome}, Età: {studente.Eta}");
                }

                Console.Write("Inserisci l'Id da modificare: ");
                int id = int.Parse(Console.ReadLine());
                bool continua = false;
                foreach (var studente in studenti)
                {
                    if (studente.Id == id)
                    {
                        continua = true;
                    }

                }
                if (continua)
                {
                    Console.Write("Inserisci il tuo nome: ");
                    string nome = Console.ReadLine();
                    Console.Write("Inserisci il tuo cognome: ");
                    string cognome = Console.ReadLine();
                    Console.Write("Inserisci la tua età: ");
                    int eta = int.Parse(Console.ReadLine());

                    studentiController.ModificaStudente(id, nome, cognome, eta);
                    Console.WriteLine("Modifica eseguita");
                }
                else
                    Console.WriteLine("Id non esistente");

                break;

            case '3' :

                List<Studente> studentiDaEliminare = studentiController.GetStudenti();
                foreach (var studente in studentiDaEliminare)
                {
                    Console.WriteLine($"ID: {studente.Id}, Nome: {studente.Nome}, Cognome: {studente.Cognome}, Età: {studente.Eta}");
                }

                Console.Write("Inserisci l'Id da Eliminare: ");
                int idEliminare = int.Parse(Console.ReadLine());
                bool continuaElimina = false;

                foreach (var studente in studentiDaEliminare)
                {
                    if (studente.Id == idEliminare)
                    {
                        continuaElimina = true;
                    }
                }

                if (continuaElimina)
                {
                    studentiController.EliminaStudente(idEliminare);
                    Console.WriteLine("Eliminazione eseguita");
                }
                else
                    Console.WriteLine("Id non esistente");
                break;

            case '4':
                List<Studente> listaStudenti = studentiController.GetStudenti();
                foreach (var studente in listaStudenti)
                {
                    Console.WriteLine($"ID: {studente.Id}, Nome: {studente.Nome}, Cognome: {studente.Cognome}, Età: {studente.Eta}");
                }
                break;         
        }
    }

    static void Menu()
    {
        Console.WriteLine("1. Aggiungi studente");
        Console.WriteLine("2. Modifica studente");
        Console.WriteLine("3. Elimina studente");
        Console.WriteLine("4. Visualizza lista studenti");
        Console.WriteLine("5. Esci");
    }
}