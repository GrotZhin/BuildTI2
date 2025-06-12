using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public void SaveData()
    {
        string json = JsonUtility.ToJson(new Data);
        File.WriteAllText(Application.persistentDataPath + "/Data.json", json);
    }
}
