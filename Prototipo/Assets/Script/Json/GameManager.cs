using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SaveSystem saveSystem;
    [SerializeField] GameUiController uiController;
    [SerializeField] HighScore highScore;
    [SerializeField] Conquistas conquistas;
    [SerializeField] ConquistasManager Manager;
    public LoadSystem loadSystem;
    public PlayerData[] lista;
    Player player;
    [SerializeField] string playerName;
    [SerializeField] GameObject panelName;
    
    [SerializeField] TMP_InputField inputField;

    public void Init(PlayerData playerData)
    { 
        playerName = playerData.playerName;
    }
    void Start()

    {
        Init(loadSystem.LoadPlayerData());
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
       

    }
    public void EndGame()
    {
        Debug.Log("EndGame");
        highScore.AddHighScoreIfPossible(new PlayerData(playerName, player.score,player.trickPoint,player.distance));
        conquistas.DeathCount += 1;
        conquistas.SaveConquistas();
        
    }

    public void Name()
    {
        playerName = inputField.text;
        saveSystem.SavePlayerData(playerName);
        inputField.text = "";
        panelName.SetActive(false);

    }
}
