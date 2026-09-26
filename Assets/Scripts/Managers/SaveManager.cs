using System.IO;
using UnityEngine;
public static class SaveManager
{
    static string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    public static void SaveData(saveData save)
    {
        string json = JsonUtility.ToJson(save, true);
        File.WriteAllText(SavePath, json);
    }
    public static saveData LoadData(saveData def)
    {
        if (!File.Exists(SavePath))
        {
            if (def != null)
                return def;
            return new saveData();
        }
        string json = File.ReadAllText(SavePath);
        saveData newData = JsonUtility.FromJson<saveData>(json);
        return newData ?? new saveData();
    }
}