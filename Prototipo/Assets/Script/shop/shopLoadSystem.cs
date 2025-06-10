using static shopData;
using UnityEngine;
using System.IO;

public class LoadSystem : MonoBehaviour
{
    public ShopData LoadShopData()
    {
        string path = Application.persistentDataPath + "/shopData.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ShopData data = JsonUtility.FromJson<ShopData>(json);
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}