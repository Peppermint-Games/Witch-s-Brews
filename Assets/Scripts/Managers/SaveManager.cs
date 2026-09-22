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
        saveData newData = new saveData();
        if (!File.Exists(SavePath))
            newData = def;
        else
        {
            string json = File.ReadAllText(SavePath);
            newData = JsonUtility.FromJson<saveData>(json);
        }
        return newData;
    }
}