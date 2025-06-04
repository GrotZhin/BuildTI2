using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SaveSystem saveSystem;
    public LoadSystem loadSystem;
    public List<PlayerData>lista;

    void Start()
    {
        loadSystem = GetComponent<LoadSystem>();
        // Exemplo de salvar dados  
        if (loadSystem.LoadPlayerData() != null)
        {
            loadSystem.LoadPlayerData();
        }
        else
        {
            saveSystem.SavePlayerData();
        }

    }

    void Update()
    {
        PlayerData loadedData = loadSystem.LoadPlayerData();

        if (loadedData != null)
        {
            Debug.Log("Nome do Jogador: " + loadedData.playerName);
            Debug.Log("Pontua��o: " + loadedData.score);
        }
    }
}
