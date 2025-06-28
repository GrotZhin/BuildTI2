using UnityEngine;
using System.IO;

public static class SaveShop
{
    private static string path = Application.persistentDataPath + "/shopdata.json";

    public static void SaveData(ShopData data)
    {
       
       
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    public static ShopData LoadData()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<ShopData>(json);
        }
        return null;
    }

    public static void ResetSave()
    {
        if (File.Exists(path))
            File.Delete(path);
    }
}