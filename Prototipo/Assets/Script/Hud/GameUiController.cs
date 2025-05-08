using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using System.Threading.Tasks;
using UnityEngine.UI;

public class GameUiController : MonoBehaviour
{
    Player player;
    public TextMeshProUGUI distanceTxt;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI finalDistanceTxt;
    public TextMeshProUGUI finalScoreTxt;
    public GameObject resultPanel;
    public GameObject pausePanel;
    public GameObject settingsPanel;

    //DoTween Animations
    [SerializeField] RectTransform MenuAni;
    [SerializeField] RectTransform UpBar;
    [SerializeField] RectTransform DownBar;
    [SerializeField] RectTransform UpHud;
    [SerializeField] RectTransform DownHud;
    [SerializeField] Image MenuBackground;
    [SerializeField] RectTransform SettingsMenu;
    [SerializeField] CanvasGroup PauseFade;
    [SerializeField] Image CamSnap;
    [SerializeField] RectTransform resoultsAni;

    //DOtween positions
    [SerializeField] float MenuSizein,MenuSizeout;
    [SerializeField] float UpTopPosY,UpmiddlePosY;
    [SerializeField] float DownTopPosY,DownmiddlePosY;
    [SerializeField] float UpHudTopPosY,UpHudmiddlePosY;
    [SerializeField] float DownHudTopPosY,DownHudmiddlePosY;
    [SerializeField] float TweenDur;
    [SerializeField] float ReTweenDur;
  

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        resultPanel.SetActive(false);
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
       


    }


    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        distanceTxt.text = distance + "m";
        scoreTxt.text ="Score: " + player.score;

        if (player.isDead)
        {
            ResoultsAni();
            resultPanel.SetActive(true);
            finalDistanceTxt.text = distance + "m";
            finalScoreTxt.text ="Score: " + player.score;
        }

    }
    public void Exit()
    {
        Application.Quit();
    }
    public async void Retry()
    {
        await RetryAni();
        SceneManager.LoadScene("GameScene");
        
    }
    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        InGameMenuAniIntro();
    }
    public async void Resume()
    {
        await InGameMenuAniOutro();
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void Settings()
    {
       
        settingsPanel.SetActive(true);
        SettingsAni();
        
    }
    public async void Apply()
    {
        settingsPanel.SetActive(false);
         await SettingsAniOutro();
    }
       //Dotween Stuff

    public void InGameMenuAniIntro(){
        MenuBackground.DOFade(0.90f,TweenDur).SetUpdate(true);
        PauseFade.DOFade(1,0).SetUpdate(true);
        MenuAni.DOScale(MenuSizein,TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true);
        UpBar.DOAnchorPosY(UpmiddlePosY,0.1f).SetEase(Ease.InOutCubic).SetUpdate(true);
        DownBar.DOAnchorPosY(DownmiddlePosY,0.1f).SetEase(Ease.InOutCubic).SetUpdate(true);
        UpHud.DOAnchorPosY(UpHudmiddlePosY,0.1f).SetEase(Ease.InCubic).SetUpdate(true);
        DownHud.DOAnchorPosY(DownHudmiddlePosY,0.1f).SetEase(Ease.InCubic).SetUpdate(true);

    }
    async Task InGameMenuAniOutro(){
         MenuBackground.DOFade(0,TweenDur).SetUpdate(true);
         PauseFade.DOFade(0f,0.5f).SetEase(Ease.InOutCubic).SetUpdate(true);
       await MenuAni.DOScale(MenuSizeout,TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true).AsyncWaitForCompletion();
        await UpBar.DOAnchorPosY(UpTopPosY,TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true).AsyncWaitForCompletion();
         await DownBar.DOAnchorPosY(DownTopPosY,TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true).AsyncWaitForCompletion();
          await UpHud.DOAnchorPosY(UpHudTopPosY,TweenDur).SetEase(Ease.InCubic).SetUpdate(true).AsyncWaitForCompletion();
         await DownHud.DOAnchorPosY(DownHudTopPosY,TweenDur).SetEase(Ease.InCubic).SetUpdate(true).AsyncWaitForCompletion();
    }

    public void SettingsAni(){

        SettingsMenu.DOScale(0.81f,TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true);
        
    }
    async Task SettingsAniOutro(){

       await SettingsMenu.DOScale(0.7f,TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true).AsyncWaitForCompletion();
       
    }

    public void  ResoultsAni()
    {
        
        resoultsAni.DOScale(1,ReTweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
    }
    async Task RetryAni()
    {

        await resoultsAni.DOScale(1.6f,0.5f).SetEase(Ease.OutCubic).SetUpdate(true).AsyncWaitForCompletion();

    }
    
}
