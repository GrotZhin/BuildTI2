using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SaveSystem saveSystem;
    [SerializeField] GameUiController uiController;
    [SerializeField] HighScore highScore;
    [SerializeField] Conquistas conquistas;
    [SerializeField] ConquistasManager Manager;
    public LoadSystem loadSystem;
    public PlayerData[] lista;
    [SerializeField]Player player;
    [SerializeField] string playerName;
    [SerializeField] GameObject panelName;
    int trickpoints;
    bool endGame = true;
    bool tutorial;
    Scene scene;    
    

    [SerializeField] TMP_InputField inputField;

    public void Init(PlayerData playerData)
    {
        playerName = playerData.playerName;
        trickpoints = playerData.trickpoints;
    }
    void Start()

    {
       
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            
            tutorial = true;
        }
        Init(loadSystem.LoadPlayerData());
       
        


    }
    public void EndGame()
    {

        if (endGame)
        {
           
            player.isDead = true;
            if (tutorial == false)
            {
                loadSystem.LoadPlayerData();
                trickpoints += player.score;
               
                saveSystem.SavePlayerData(playerName, trickpoints);
                highScore.AddHighScoreIfPossible(new PlayerData(playerName, player.score, 0, player.distance));
                endGame = false;
                conquistas.DeathCount += 1;
                conquistas.SaveConquistas();
            }



        }


    }

    public void Name()
    {
        playerName = inputField.text;
        saveSystem.SavePlayerData(playerName, trickpoints);
        inputField.text = "";
        panelName.SetActive(false);

    }
}
