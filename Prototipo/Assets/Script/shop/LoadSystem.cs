using static shopData;
using UnityEngine;
using System.IO;

public class LoadSystem : MonoBehaviour
{
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/shopData.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Data data = JsonUtility.FromJson<ShopData>(json);
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}