using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    PlayerData playerData;
   
    public void SavePlayerData(string playerName)
    {
        
        playerData = new PlayerData(playerName,0,0,0);
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
        Debug.Log(playerData.playerName + playerData.score);
    }
}