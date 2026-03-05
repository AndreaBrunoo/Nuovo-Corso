/*
    1. Effettuare il backup di un Cartella chiamata Data all'interno del progetto
    2. Elencare i file e le sottodirectory presenti 
    3. Stampare le informazioni sui file e sulle directory elencati
    4. Creare una cartella di backup con il timestamp all'interno della folder selezionata
    5. Copiare tutti i file presenti nella folder selezionata mantenendo la struttura delle sottodirectory
    6. Spostare i files copiati dentro cartelle divisi per estensione (es. tutti i .txt in una cartella "txt", tutti i "jpg", ecc.)
    7. Eliminare i file originali dopo averli copiati
    8. Gestire gestire eventuali errori(es. percorso non valido o file in uso)e stampare messaggi di errore
    9. Il file di backup dovrà essere messo insisme a data all'interno del progetto
*/
string root = Path.Combine(".", "Data");
try
{
    if (!Directory.Exists(root))
    {
        Console.WriteLine("La cartella 'Data' non esiste.");
        Environment.Exit(0);
    }
    Console.Clear();
    Console.WriteLine(" ");
    foreach (var dir in Directory.GetDirectories(root))
        Console.WriteLine("[DIR] " + Path.GetFileName(dir));
    foreach (var file in Directory.GetFiles(root))
        Console.WriteLine("[FILE] " + Path.GetFileName(file));
    Console.WriteLine(" ");

    StampaInformazioni(root);
    Console.WriteLine(" ");
    string backupFolder = CreaCartellaBackup();

    Copia(root, backupFolder);
    Console.WriteLine("Copia completata.");
    Console.WriteLine(" ");

    OrganizzaPerEstensione(backupFolder);
    Console.WriteLine("File organizzati per estensione.");
    Console.WriteLine(" ");

    EliminaOriginali(root);
    Console.WriteLine("File originali eliminati.");
    Console.WriteLine(" ");
}
catch (Exception ex)
{
    Console.WriteLine("ERRORE: " + ex.Message);
}

// ------------------------ FUNZIONI ------------------------

void StampaInformazioni(string percorso)
{
    Console.WriteLine("--- Informazioni contenuto ---");
    Console.WriteLine(" ");

    foreach (var dir in Directory.GetDirectories(percorso))
    {
        var info = new DirectoryInfo(dir);
        Console.WriteLine($"[DIR]  {info.Name} - Creata il {info.CreationTime}");
    }

    foreach (var file in Directory.GetFiles(percorso))
    {
        var info = new FileInfo(file);
        Console.WriteLine($"[FILE] {info.Name} - {info.Length} bytes - Creato il {info.CreationTime}");
    }
    Console.WriteLine(" ");
}

string CreaCartellaBackup()
{
    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

    string backupFolder = Path.Combine(".", "Backup_" + timestamp);

    Directory.CreateDirectory(backupFolder);
    Console.WriteLine("Cartella di backup creata: " + backupFolder);
    Console.WriteLine(" ");

    return backupFolder;
}

void Copia(string partenza, string destinazione)
{
    foreach (var dir in Directory.GetDirectories(partenza))
    {
        string newDir = Path.Combine(destinazione, Path.GetFileName(dir));
        Directory.CreateDirectory(newDir);
        Copia(dir, newDir);
    }

    foreach (var file in Directory.GetFiles(partenza))
    {
        string destFile = Path.Combine(destinazione, Path.GetFileName(file));
        File.Copy(file, destFile, true);
    }
}

void OrganizzaPerEstensione(string percorso)
{
    var files = Directory.GetFiles(percorso, "*.*", SearchOption.AllDirectories);

    foreach (var file in files)
    {
        string extension = Path.GetExtension(file).Trim('.').ToLower();
        if (extension == "") extension = "senza_estensione";

        string extensionFolder = Path.Combine(percorso, extension);
        Directory.CreateDirectory(extensionFolder);

        string destFile = Path.Combine(extensionFolder, Path.GetFileName(file));

        if (Path.GetDirectoryName(file) != extensionFolder)
            File.Move(file, destFile, true);
    }
}

void EliminaOriginali(string percorso)
{
    foreach (var file in Directory.GetFiles(percorso, "*.*", SearchOption.AllDirectories))
    {
        File.Delete(file);
    }
}

/*
void CreazioneData()
{
    string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
    Directory.CreateDirectory(dataPath);

    string folder1Path = Path.Combine(dataPath, "Folder1");
    Directory.CreateDirectory(folder1Path);
    File.WriteAllText(Path.Combine(folder1Path, "file1.txt"), "contenuto del file1.txt");
    File.WriteAllText(Path.Combine(folder1Path, "file2.jpg"), "contenuto del file2.jpg");

    string subFolder1Path = Path.Combine(folder1Path, "Subfolder1");
    Directory.CreateDirectory(subFolder1Path);
    File.WriteAllText(Path.Combine(subFolder1Path, "file3.txt"), "contenuto del file3.txt");

    string folder2Path = Path.Combine(dataPath, "Folder2");
    Directory.CreateDirectory(folder2Path);
    File.WriteAllText(Path.Combine(folder2Path, "file4.txt"), "contenuto del file4.txt");
    File.WriteAllText(Path.Combine(folder2Path, "file5.jpg"), "contenuto del file5.jpg");
}
*/