using System.IO;
using UnityEngine;

public partial class SaveSystem : MonoBehaviour
{
    PlayerData playerData;
   
    public void SavePlayerData(string playerName, int tp)
    {
        
        playerData = new PlayerData(playerName,0,tp,0);
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
     
    }
}