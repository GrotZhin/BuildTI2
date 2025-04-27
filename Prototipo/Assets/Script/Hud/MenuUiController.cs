using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MenuUiController : MonoBehaviour
{
  
   
    public GameObject settingsPanel;
    public GameObject shopPanel;

    // Start is called before the first frame update
    private void Awake()
    {
        
        settingsPanel.SetActive(false);
        shopPanel.SetActive(false);

    }


    // Update is called once per frame
   

    public void Exit()
    {
        Application.Quit();
    }
 
    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void Settings()
    {
        settingsPanel.SetActive(true);
    }
    public void Apply()
    {
        settingsPanel.SetActive(false);
    }
    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Shop()
    {
        shopPanel.SetActive(true);
    }
    public void Back()
    {
        shopPanel.SetActive(false) ;
    }
}
