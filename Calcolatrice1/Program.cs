Console.WriteLine("Premi y per continuare o n per uscire");

string risposta = Console.ReadLine();
if (risposta == "y" || risposta == "Y")
{
    Console.WriteLine("Hai scelto di continuare!");
}
else if (risposta == "n" || risposta == "N")
{
    Console.WriteLine("Hai scelto di uscire. Arrivederci!");
    Environment.Exit(0); // Opzionale: termina il programma
}
else
{
    Console.WriteLine("Risposta non valida.");
    Environment.Exit(0); 
}

//__________________________________________________________________________

Console.WriteLine("Premi 1 per un Ciao o 2 per un Buongiorno");

char risposta2 = Console.ReadKey().KeyChar;
if (risposta2 == '1')
{
    Console.WriteLine("\nCiao!");
}
else if (risposta2 == '2')
{
    Console.WriteLine("\nBuongiorno!");
}
else
{
    Console.WriteLine("\nRisposta non valida.");
}

//__________________________________________________________________________

Console.WriteLine("Inserisci un numero da 1 a 5:");
string inputNumero = Console.ReadLine();
int numero;
if (int.TryParse(inputNumero, out numero))
{
    if (numero >= 1 && numero <= 5)
    {
        Console.WriteLine($"Hai inserito il numero {numero}");
    }
    else
    {
        Console.WriteLine("Numero non valido. Inserisci un numero tra 1 e 5.");
    }
}
else
{
    Console.WriteLine("Input non valido. Inserisci un numero intero.");
}

//__________________________________________________________________________

// Esercizio Bonus: Calcolatrice Semplice
Console.WriteLine("Calcolatrice Semplice");
Console.Write("Inserisci il primo numero: ");   
string input1 = Console.ReadLine();
Console.Write("Inserisci il secondo numero: "); 
string input2 = Console.ReadLine();
double num1, num2;

if (double.TryParse(input1, out num1) && double.TryParse(input2, out num2))
{
    Console.WriteLine("Scegli l'operazione (+, -, *, /): ");
    char operazione = Console.ReadKey().KeyChar;
    Console.WriteLine(); 

    double risultato;
    switch (operazione)
    {
        case '+':
            risultato = num1 + num2;
            Console.WriteLine($"Risultato: {risultato}");
            break;
        case '-':
            risultato = num1 - num2;  
            Console.WriteLine($"Risultato: {risultato}");
            break;
        case '*':
            risultato = num1 * num2;
            Console.WriteLine($"Risultato: {risultato}");
            break;
        case '/':
            if (num2 != 0)
            {
                risultato = num1 / num2;
                Console.WriteLine($"Risultato: {risultato}");
            }
            else
            {
                Console.WriteLine("Errore: Divisione per zero non consentita.");
            }
            break;
        default:
            Console.WriteLine("Operazione non valida.");
            break;
    }
}
else
{
    Console.WriteLine("Input non valido. Assicurati di inserire numeri validi.");
}
