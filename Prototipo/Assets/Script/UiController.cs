using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class UiController : MonoBehaviour
{
    Player player;
    public TextMeshProUGUI distanceTxt;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI finalDistanceTxt;
    public TextMeshProUGUI finalScoreTxt;
    public GameObject resultPanel;
    public GameObject pausePanel;
    public GameObject settingsPanel;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        resultPanel.SetActive(false);
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);


    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        distanceTxt.text = distance + "m";
        scoreTxt.text ="Score: " + player.score;

        if (player.isDead)
        {
            resultPanel.SetActive(true);
            finalDistanceTxt.text = distance + "m";
            finalScoreTxt.text ="Score: " + player.score;
        }

    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Retry()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("menu");
    }
    public void Settings()
    {
        settingsPanel.SetActive(true);
    }
    public void Apply()
    {
        settingsPanel.SetActive(false);
    }
}
