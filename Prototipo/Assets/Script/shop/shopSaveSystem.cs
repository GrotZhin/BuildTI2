using System.IO;
using UnityEngine;

public class shopSaveSystem : MonoBehaviour
{
    public void SaveShopData(shopData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/shopData.json", json);
    }
}
