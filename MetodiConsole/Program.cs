
// esempio di percorso assoluto
string absolutePath = @"C:\Users\Username\Documents\file.txt";
// esempio di percorso relativo
string relativePath = @"..\..\file.txt"; //salire di due directory e accedere al file

// ottenere il nome del file da un percorso
string fileName = Path.GetFileName(relativePath);
// ottenere la directory da un percorso
string directory = Path.GetDirectoryName(relativePath);
// ottenere l'estensione del fileda un percorso
string extension = Path.GetExtension(relativePath);
// ottenere il nome del file senza l'estensione
string fileNameWhitoutExtension = Path.GetFileNameWithoutExtension(relativePath);

// combinare percorsi 
string combinedPath = Path.Combine("C:\\Users", "Username", "Documents", "file.txt");
Console.WriteLine(combinedPath); //Stampa "C:\Users\Username\Documents\file.txt"

//FILES

// creare un file
string path = @"text.txt";
File.WriteAllText(path, "Hello World");

// leggere un file
if (File.Exists(path))
{
    string content = File.ReadAllText(path);
    Console.WriteLine(content);
}
else
    Console.WriteLine("il file non esiste");

// copiare un file
string sourcePath = @"source.txt";
string destinationPath = @"destination.txt";
if (File.Exists(sourcePath))
    File.Copy(sourcePath, destinationPath); //copia il file
else
    Console.WriteLine("il file non esiste");

//rinomina file
string oldFileName = @"oldName.txt";
string 

// ottenere informaizoni su un file
FileInfo info = new FileInfo(path);
Console.WriteLine(Information.Length);
Console.WriteLine(Information.CreationTime);
Console.WriteLine(Information.LastWriteTime);
Console.WriteLine(Information.Extension);
Console.WriteLine(Information.Name);
Console.WriteLine(Information.DirectoryName);
 
// scrivere su un file
string path = @"text.txt";
string content = "Hello Word";
File.WriteAllText(path, content);
File.AppendAllText(path, content);

// scrivere un elenco di stringhe su un file
List<string> lines = new List<string>{"line 1", "line 2", "line 3"};
File.WriteAllLines(path, lines);
File.AppendAllLines(path, lines);

// leggere un file riga per riga
string[] lines = File.ReadAllLines(path);
foreach (string line in lines)
{
    Console.WriteLine(line); // stampa ogni riga del file
}

// FOLDERS

// Eliminare una directory
if (Directory.Exists(dir))
{
    Directory.Delete(dir); // elimina directory
}
else
{
    Console.WriteLine("la directory non esiste");
}

// ottenere informazioni su un folder
DirectoryInfo info = new DirectoryInfo(dir);
Console.WriteLine(info.CreationTime); // data di creazione della directory
Console.WriteLine(info.LastWriteTime); // data dell'ultima modifica della directory
Console.WriteLine(info.Name); // nome della directory
Console.WriteLine(info.FullName); // percorso completo della directory

// Elencare i file in una directory
if (Directory.Exists(dir))
{
    string[] files = Directory.GetFiles(dir); // ottenere un array di stringhe con i percorsi del file nella directory
    foreach(string file in files)
    {
        Console.WriteLine(file);
    }
}
else
    Console.WriteLine("La directory no esiste");

// elencare le sottodirectory in una directory
if (Directory.Exists(dir))
{
    string[] subdirs = Directory.GetDirectories(dir);
    foreach(string file in subdirs)
    {
        Console.WriteLine(file);
    }
}
else
    Console.WriteLine("La directory no esiste");

//using
string path = @"text.txt";
if (File.Exists(path))
{
    using (StreamReader reader = new StreamReader(path))
    {
        string content = reader.ReadToEnd();
        Console.WriteLine(content);
    }
    //il file viene chiuso automaticamente al termine del blocco using
}
else
    Console.WriteLine("La directory no esiste");

//con il try catch
string path = @"text.txt";
try
{
    using(StreamReader reader = new StreamReader(path))
    {
        string content = reader.ReadToEnd();
        Console.WriteLine(content);
    }
}
catch (IOException ex)
{
    Console.WriteLine("Il file è in uso da un altro processo. Dettagli: " + ex.Message);
}