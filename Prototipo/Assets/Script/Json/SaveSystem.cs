using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    PlayerData playerData;
   
    public void SavePlayerData( )
    {
        
        playerData = new PlayerData("Masco", 228);
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
        Debug.Log(playerData.playerName + playerData.score);
    }
}