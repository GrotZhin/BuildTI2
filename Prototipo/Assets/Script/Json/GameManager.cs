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
    int trickpoints;
    bool endGame = true;

    [SerializeField] TMP_InputField inputField;

    public void Init(PlayerData playerData)
    {
        playerName = playerData.playerName;
        trickpoints = playerData.trickpoints;
    }
    void Start()

    {
        Init(loadSystem.LoadPlayerData());
        Debug.Log("start" + trickpoints);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


    }
    public void EndGame()
    {

        if (endGame)
        {
            Debug.Log("EndGame");
             player.isDead = true;
            loadSystem.LoadPlayerData();
            trickpoints += player.score;
            Debug.Log("change" + trickpoints);
            saveSystem.SavePlayerData(playerName, trickpoints);
            highScore.AddHighScoreIfPossible(new PlayerData(playerName, player.score, 0, player.distance));
            endGame = false;
            conquistas.DeathCount += 1;
            conquistas.SaveConquistas();
           

        }


    }

    public void Name()
    {
        playerName = inputField.text;
        saveSystem.SavePlayerData(playerName,trickpoints);
        inputField.text = "";
        panelName.SetActive(false);

    }
}
