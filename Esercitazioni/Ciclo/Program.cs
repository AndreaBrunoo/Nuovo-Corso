// ciclo for

for (int i = 1; i <= 10; i++)
{
    Console.WriteLine(i);
}

// ciclo foreach con lista
List<string> nomi = new List<string> {"nome1, nome2, nome3"};
foreach (var nome in nomi)
{
    Console.WriteLine(nome);
}

// ciclo foreach con dizionario
Dictionary<int, string> dizionario = new Dictionary<int, string>
{
    { 1, "uno" },
    { 2, "due" },
    { 3, "tre" }
};
foreach (var kvp in dizionario)
{
    Console.WriteLine ($"chiave: {kvp.Key}, valore: {kvp.Value}");
}

// ciclo while
int index = 1;
while (index <= 10)
{
    Console.WriteLine(index);
    index++;
}

// ciclo do while 
int indice =1;
do
{
    Console.WriteLine(indice);
    indice++;
}
while (indice <= 10);

// fizzbuzz con ciclo for 
for (int f = 1; f <= 100; f++)
{
int risultato1 = f % 3;
int risultato2 = f % 5;
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
    Console.WriteLine (f);
}
}

// fizzbuzz con foreach
List<int> numStrani = new List<int> {346578, 672687248, 5652784, 12784825, 67217};
foreach (var num in numStrani)
{
int risultato3 = num % 3;
int risultato4 = num % 5;
if ( (risultato3 == 0) && (risultato4 == 0) )
{
    Console.WriteLine ("fizzbuzz");
}
else if (risultato3 == 0)
{
    Console.WriteLine ("fizz");
}
else if (risultato4 == 0)
{
    Console.WriteLine ("buzz");
}
else
{
    Console.WriteLine (num);
}
}

// parce e casting con ciclo for
for (int t = 1; t <= 5; t++)
{
    Console.WriteLine ("inserisci un numero: ");
    string imput = Console.ReadLine();
    int numero = int.Parse(imput);
    Console.WriteLine ($"{numero}");
}


// parce e casting con ciclo while e foreach stampando lo 0
List<int> utenti = new List<int>();
int numImput = 1;
while (numImput != 0)
{
    Console.WriteLine("inserisci un numero o premi 0 per uscire");
    string numUtente = Console.ReadLine();
    numImput = int.Parse(numUtente);
    utenti.Add(numImput);

}
 Console.Write ($"la tua lista è composta da: ");
foreach (var u in utenti)
{
    Console.WriteLine ($"{u}");
}

// parce e casting con ciclo while e foreach senza stampare lo 0
List<int> utenza = new List<int>();
while (true)
{
    Console.WriteLine("inserisci un numero o premi 0 per uscire");
    string numUtente1 = Console.ReadLine();
    int numImput1 = int.Parse(numUtente1);
    if (numImput1 == 0)
    {
        break;
    }
    utenza.Add(numImput1);

}
 Console.Write ($"la tua lista è composta da: ");
foreach (var r in utenza)
{
    Console.WriteLine ($"{r}");
}

// random
Random random = new Random();
int numeroCasuale = random.Next(0,100);  // il primo incluso, il secondo escluso
int numeroCasualeSenzaMinimo = random.Next(100); // omettere il primo parametro non cambia nulla
double numeroDecimaleCasuale = random.NextDouble();
bool valoreBooleanoCasuale = random.Next(2) == 0;
