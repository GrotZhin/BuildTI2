using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    List<PlayerData> playerDataList = new List<PlayerData>();
    [SerializeField] int maxCount = 5;
    [SerializeField] string filename;
    public delegate void OnHighScoreListChanged(List<PlayerData> list);
    public static event OnHighScoreListChanged onHighScoreListChanged;
    private void Start()
    {
        LoadHighScore();
    }

    private void LoadHighScore()
    {
        playerDataList = FileHandler.ReadListFromJSON<PlayerData>(filename);

        while (playerDataList.Count > maxCount)
        {
            playerDataList.RemoveAt(maxCount);
        }
        if (onHighScoreListChanged != null)
        {
            onHighScoreListChanged.Invoke(playerDataList);
        }
    }
    private void SaveHighScore()
    {
        FileHandler.SaveToJSON<PlayerData>(playerDataList, filename);
    }
    public void AddHighScoreIfPossible(PlayerData playerData)
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (i >= playerDataList.Count || playerData.score > playerDataList[i].score)
            {
                playerDataList.Insert(i, playerData);
                while (playerDataList.Count > maxCount)
                {
                    playerDataList.RemoveAt(maxCount);
                }

                SaveHighScore();

                if (onHighScoreListChanged != null)
                {
                    onHighScoreListChanged.Invoke(playerDataList);
                }


                break;
            }
        }
    }
}
