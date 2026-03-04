using Newtonsoft.Json;
public static class JsonHelper
{
    public static void Salva(string path, object obj)
    {
        string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public static T Leggi<T>(string path)
    {
        if (!File.Exists(path))
        {
            return default(T);
        }

        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<T>(json);
    }
}