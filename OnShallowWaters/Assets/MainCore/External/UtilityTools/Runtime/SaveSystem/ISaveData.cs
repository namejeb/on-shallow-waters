using NamejebTools.SaveSystem;

public interface ISaveData
{
    static void SaveData(string saveName, object data)
    {
        SerializationManager.Save(saveName, data);
    }

    static object LoadData(string saveName)
    {
        return SerializationManager.Load(saveName);
    }
}
