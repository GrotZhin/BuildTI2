using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SaveSystem saveSystem;
    [SerializeField] HighScore highScore;
    [SerializeField] Conquistas conquistas;
    [SerializeField] ConquistasManager Manager;
    public LoadSystem loadSystem;
    public PlayerData[] lista;
    Player player;
    [SerializeField] string playerName;
    [SerializeField] TMP_InputField inputField;

    void Start()

    {
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        

    }
    public void EndGame()
    {
        highScore.AddHighScoreIfPossible(new PlayerData(playerName, player.score));
        conquistas.DeathCount += 1;
        conquistas.SaveConquistas();
        
    }

    public void Name()
    {
        playerName = inputField.text;
        inputField.text = "";
    }
}
