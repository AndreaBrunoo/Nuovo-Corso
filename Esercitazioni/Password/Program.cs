// Genera una password casuale di lunghezza compresa tra 5 e 8 caratteri, che deve contenere almeno una lettera maiuscola, una minuscola un numero e un carattere speciale(@, #, !, ecc...) non deve contenere spazi
Random random = new Random();
Console.WriteLine("Vuoi generare una password casuale? y ");
string risposta = Console.ReadLine();
List<string> minuscole = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
List<string> maiuscole = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
List<string> numeri = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
List<string> caratteri = new List<string>() { "@", "#", "!", "$", "%", "&", "*", "?" };
List<string> passwordList = new List<string>();
int [] lunghezza = [random.Next(5, 9)];

if (risposta == "y" || risposta == "Y")
{
    bool contieneMaiuscola = false;
    bool contieneMinuscola = false;
    bool contieneNumero = false;
    bool contieneCarattere = false;
    while (!contieneMaiuscola && !contieneMinuscola && !contieneNumero && !contieneCarattere)
    {
        while (passwordList.Count < lunghezza[0])
        {
            contieneMaiuscola = false;
            contieneMinuscola = false;
            contieneNumero  = false;
            contieneCarattere = false;
            int casuale = random.Next(4);
            if (casuale == 0)
            {
                passwordList.Add(minuscole[random.Next(minuscole.Count)]);
                contieneMinuscola = true;

            }
            else if (casuale == 1)
            {
                passwordList.Add(maiuscole[random.Next(maiuscole.Count)]);
                contieneMaiuscola = true;
            }
            else if (casuale == 2)
            {
                passwordList.Add(numeri[random.Next(numeri.Count)]);
                contieneNumero = true;
            }
            else
            {
                passwordList.Add(caratteri[random.Next(caratteri.Count)]);
                contieneCarattere = true;
            }

        }
   
       
    }
    Console.WriteLine(string.Join("", passwordList));
}
else
{
    Console.WriteLine("Ok, magari la prossima volta!");
}


//legge un elenco di email da una lista, per ognuna maschera la parte dell'utente prima della @ con asterischi lasciando visibile solo la prima e l'ultima lettera, mantenendo visibile la parte del dominio dopo la @. il numero di asterischi deve essere uguale al numero di lettere mascherate. 
List<string> emailList = new List<string>() { "giannigianni@gmail.com", "lupoalberto@gmail.com", "paolapina@gmail.com" };
foreach (string email in emailList)
{
    int chiocciola = email.IndexOf("@");
    string utente = email.Substring(0, chiocciola);
    string dominio = email.Substring(chiocciola);
    string mascherata = utente[0] + new string('*', utente.Length - 2) + utente[utente.Length - 1];
    Console.WriteLine(mascherata + dominio);
}

/*
legge un elenco di frasi da una lista
per ognuna genera uno slug
1. deve essere tutto minuscolo
2. deve sostituire gli spazi con trattini
3. deve rimuovere la punteggiatura
4. deve rimuovere eventuali trattini doppi
*/
List<string> frasi = new List<string> { "ho chieSto, a Gianni, uno sqUalo.", "...pino!! canecella LA FOto, ciao", "Fantozzi-e il CAVAllo matto!!" };
List<char> punteggiatura = new List<char> {',','.','-','_', '!', '?', '+'};
foreach (string frase in frasi)
{
    string slug = frase.ToLower(); // 1. deve essere tutto minuscolo
    //ciclare slug = slug.Replace(".", "").Replace("!", "").Replace(",", "").Replace("-", ""); // 3. deve rimuovere la punteggiatura
    slug = slug.Replace(" ", "-"); // 2. deve sostituire gli spazi con trattini
    while (slug.Contains("--"))
    {
        slug = slug.Replace("--", "-"); // 4. deve rimuovere eventuali trattini doppi
    }
    Console.WriteLine(slug);
}
