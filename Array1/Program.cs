Random random = new Random();
int[] array =new int[5];
 int[] arrayCopia = new int[5];
Console.WriteLine ("se schiacci 1 generi numeri casuali da un dado");
string arrayImput0 = Console.ReadLine();

    if (arrayImput0 == "1")
    {
        int I = 0;
        while ( I < 5 )
        {
            int dado6 = random.Next(1,7);
            Console.WriteLine ($"è uscito il numero: {dado6}, premi 1 per tenere o 0 per buttare");
            string scelta = Console.ReadLine();
                if( scelta == "1")  
                {
                    array[I] = dado6;
                    I++;
                }
                else if (scelta == "0")
                {
                    Console.WriteLine("numero buttato");
                }
                else
                {
                    Console.WriteLine("scelta non valida, riprova");
                }  
        }  
    }
     else
    {
        Console.WriteLine ("scelta non valida riprova");
        Environment.Exit(0);
    }
    
if (array[4] != 0 && array[0] != 0 && array[1] != 0 && array[2] != 0 && array[3] != 0)
{
    Array.Sort(array);
    Array.Reverse(array);

    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] >= 5)
        {
            arrayCopia[i] = array[i];
        }
    }
    Console.WriteLine(string.Join(",", arrayCopia));
}



int[] numeri = {1, 2, 3, 4, 5}; //arrey
Console.WriteLine(numeri.Length); //Lenght restituisce il numero di elementi dell'arrey (output 5)

int[] numeriCopia = new int[numeri.Length]; // dichiarazione array
Array.Copy(numeri, numeriCopia, numeri.Length);
Console.WriteLine(string.Join(",", numeriCopia)); // output 1,2,3,4,5

Array.Clear(numeriCopia, 0, numeriCopia.Length); //resetta l'arrey per tutta la sua lunghezza
Console.WriteLine(string.Join(",", numeriCopia)); //output 0,0,0,0,0

Array.Reverse(numeri); //inverte l'arrey
Console.WriteLine(string.Join("," , numeri)); //output 5,4,3,2,1

Array.Sort(numeri); // ordina i numeri in ordine crescente
Console.WriteLine(string.Join(",", numeri)); // output 1,2,3,4,5

int[] numero = {9, 8, 7, 6, 5}; // array
int indice = Array.IndexOf(numero, 7);
Console.WriteLine(indice); // output 2 perchè restituisce il numero corrrispondente all'indice richiesto
