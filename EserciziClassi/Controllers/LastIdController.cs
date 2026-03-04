public class LastIdController
{
    private readonly string path = "Data/lastId.json";
    private LastId lastIdObj;
    public LastIdController()
    {
        lastIdObj = JsonHelper.Leggi<LastId>(path)?? new LastId {Id = 0};
    }
    public int GetNextId()
    {
        lastIdObj.Id++;
        JsonHelper.Salva(path, lastIdObj);
        return lastIdObj.Id;
    }
}