/*L'utente deve inserire una stringa contenente un operazione matematica (+, - , *, /)
leggere la stringa e risolvere l'operazione matematica e uscire con il risultato*/

while (true)
{
    Console.WriteLine("--------------------calcolatrice--------------------");
    Console.WriteLine(" ");
    Console.WriteLine("1. Inserisci un operazione: ");
    Console.WriteLine("2. Esci");
    char scelta = Console.ReadKey().KeyChar;
    Console.Clear();
    if (scelta == '1')
    {
        Console.Write("Inserisci un operazione: ");
        string operazione = Console.ReadLine();

        string segno = InputCalcolatrice(operazione);

        string[] stringaNumeri = operazione.Split(segno);
        double numero1, numero2;

        if (!double.TryParse(stringaNumeri[0], out numero1))
        {
            Console.WriteLine("Operazione non valida");
            continue;
        }

        if (!double.TryParse(stringaNumeri[1], out numero2))
        {
            Console.WriteLine("Operazione non valida");
            continue;
        }

        double risultato = Calcolatrice(segno, numero1, numero2);

    }
    else if (scelta == '2')
    {
        Console.WriteLine("Arrivederci!");
        break;
    }
    else
    {
        Console.WriteLine("Scelta non valida. Riprova.");
        continue;
    }


}



double Calcolatrice(string segno, double numero1, double numero2)
{
    double risultato = 0;
    switch (segno)
    {
        case "+":
            risultato = numero1 + numero2;
            Console.WriteLine($"Il risultato è: {risultato}");
            break;

        case "-":
            risultato = numero1 - numero2;
            Console.WriteLine($"Il risultato è: {risultato}");
            break;

        case "*":
            risultato = numero1 * numero2;
            Console.WriteLine($"Il risultato è: {risultato}");
            break;

        case "/":
            if (numero2 != 0)
            {
                risultato = numero1 / numero2;
                Console.WriteLine($"Il risultato è: {risultato}");
                break;
            }
            Console.WriteLine("Divisione per 0 non possibile. Riprova.");
            break;
    }
    return risultato;
}
string InputCalcolatrice(string operazione)
{
    string segno = "";
    if (operazione.Contains("+"))
    {
        segno = "+";
    }
    else if (operazione.Contains("-"))
    {
        segno = "-";
    }
    else if (operazione.Contains("*"))
    {
        segno = "*";
    }
    else if (operazione.Contains("/"))
    {
        segno = "/";
    }
    else
    {
        Console.WriteLine("Non è presente nessun segno");
    }
    return segno;
}