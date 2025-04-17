using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using TMPro;
using UnityEditor.SearchService;
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

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        resultPanel.SetActive(false);

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        distanceTxt.text = distance + "m";
        scoreTxt.text = player.score + "";

        if (player.isDead)
        {
            resultPanel.SetActive(true);
            finalDistanceTxt.text = distance + "m";
            finalScoreTxt.text = player.score + "";
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
}
