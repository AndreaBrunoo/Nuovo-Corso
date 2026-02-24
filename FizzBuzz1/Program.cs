Console.Write ("inserisci un numero: ");
string imput = Console.ReadLine();
int numero = int.Parse(imput);
int risultato1 = numero % 3;
int risultato2 = numero % 5;

if ( (risultato1 == 0) && (risultato2 == 0) )
{
    Console.WriteLine ("fizzbuzz");
}
else if (risultato1 == 0)
{
    Console.WriteLine ("fizz");
}
else if (risultato2 == 0)
{
    Console.WriteLine ("buzz");
}
else
{
    Console.WriteLine (numero);
}



//fizzbuzz con swich
Console.Write ("inserisci un numero: ");
string imput1 = Console.ReadLine();
int numero1 = int.Parse(imput1);
int risultato3 = numero1 % 3;
int risultato4 = numero1 % 5;
switch ( (risultato3 == 0, risultato4 == 0) )
{
    case (true, true):
        Console.WriteLine ("fizzbuzz");
        break;
    case (true, false):
        Console.WriteLine ("fizz");
        break;
    case (false, true):
        Console.WriteLine ("buzz");
        break;
    default:
        Console.WriteLine (numero1);
        break;
}



// utilizzare le eccezzioni
const int NUM_PRED = 10;
Console.Write ("inserisci un numero: ");
string num = Console.ReadLine();
if ( int.TryParse(num , out int numUtente) )
// TryParce permette di conventire l'imput, se ha successo crea una variabile tramite out
{
    int risultato = NUM_PRED + numUtente;
    Console.WriteLine ($"il risultato è {risultato}");
}
else
{
    Console.WriteLine ("numero non valido");
}
