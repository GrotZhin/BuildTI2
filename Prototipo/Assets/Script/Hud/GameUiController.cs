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
using RWM;

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
    [SerializeField] RectTransform ShockBtn;
    [SerializeField] RectTransform UpSrtBar;
    [SerializeField] RectTransform DownSrtBar;
    [SerializeField] RectTransform UpLHud;
    [SerializeField] RectTransform UpRHud;
    [SerializeField] RectTransform DownLHud;
    [SerializeField] RectTransform DownRHud;
    [SerializeField] Image MenuBackground;
    [SerializeField] RectTransform SettingsMenu;
    [SerializeField] CanvasGroup PauseFade;
    [SerializeField] CanvasGroup ResultFade;
    [SerializeField] Image CamSnap;
    [SerializeField] RectTransform resoultsAni;
    public AudioSource Music;

    //DOtween positions
    [SerializeField] float MenuSizein, MenuSizeout;
    [SerializeField] float UpTopPosY, UpmiddlePosY;
    [SerializeField] float DownTopPosY, DownmiddlePosY;
    [SerializeField] float UpHudTopPosY, UpHudmiddlePosY;
    [SerializeField] float DownHudTopPosY, DownHudmiddlePosY;
    [SerializeField] float TweenDur;
    [SerializeField] float ReTweenDur;

    public float Intimer;


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
        Intimer += Time.deltaTime;

        if (Intimer >= 0.6)
        {
            
            DownSrtBar.DOAnchorPosY(-66, 0.3f).SetUpdate(true);
            UpRHud.DOAnchorPosY(UpHudTopPosY, 0.2f).SetUpdate(true);
            DownLHud.DOAnchorPosY(DownHudTopPosY, 0.2f).SetUpdate(true);
            DownRHud.DOAnchorPosY(DownHudTopPosY, 0.2f).SetUpdate(true);

            
        }

        
        int distance = Mathf.FloorToInt(player.distance);
        distanceTxt.text = distance + "m";
        scoreTxt.text = "TP " + player.score;

        if (player.isDead)
        {
            Music.volume = 0.2f;
            ResoultsAni();
            resultPanel.SetActive(true);
            finalDistanceTxt.text = distance + "m";
            finalScoreTxt.text = "TP: " + player.score;
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
        Music.volume = 0.2f;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        PauseFade.DOFade(1, 0.2f).SetUpdate(true);
        InGameMenuAniIntro();
    }
    public async void Resume()
    {
        
        Music.volume = 1f;
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
        soundManager.PlaySound(SoundType.SettingsOp);
        SettingsAni();

    }
    public async void Apply()
    {
        settingsPanel.SetActive(false);
        soundManager.PlaySound(SoundType.SettingsClos);
        await SettingsAniOutro();
    }
    //Dotween Stuff

    public void InGameMenuAniIntro()
    {
        MenuBackground.DOFade(0.90f, TweenDur).SetUpdate(true);
        MenuAni.DOScale(MenuSizein, TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true);
        UpBar.DOAnchorPosY(UpmiddlePosY, 0.1f).SetEase(Ease.InOutCubic).SetUpdate(true);
        DownBar.DOAnchorPosY(DownmiddlePosY, 0.1f).SetEase(Ease.InOutCubic).SetUpdate(true);
        UpLHud.DOAnchorPosY(UpHudmiddlePosY, 0.1f).SetEase(Ease.InCubic).SetUpdate(true);
        UpRHud.DOAnchorPosY(UpHudmiddlePosY, 0.1f).SetEase(Ease.InCubic).SetUpdate(true);
        DownLHud.DOAnchorPosY(DownHudmiddlePosY, 0.1f).SetEase(Ease.InCubic).SetUpdate(true);
        DownRHud.DOAnchorPosY(DownHudmiddlePosY, 0.1f).SetEase(Ease.InCubic).SetUpdate(true);

    }
    public async Task InGameMenuAniOutro()
    {
        PauseFade.DOFade(0f, 0.4f).SetUpdate(true);
        MenuBackground.DOFade(0, 0.5f).SetUpdate(true);
        await MenuAni.DOScale(MenuSizeout, TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true).AsyncWaitForCompletion();
        await UpBar.DOAnchorPosY(UpTopPosY, TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true).AsyncWaitForCompletion();
        await DownBar.DOAnchorPosY(DownTopPosY, TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true).AsyncWaitForCompletion();
        await UpLHud.DOAnchorPosY(UpHudTopPosY, TweenDur).SetEase(Ease.InCubic).SetUpdate(true).AsyncWaitForCompletion();
        await UpRHud.DOAnchorPosY(UpHudTopPosY, TweenDur).SetEase(Ease.InCubic).SetUpdate(true).AsyncWaitForCompletion();
        await DownLHud.DOAnchorPosY(DownHudTopPosY, TweenDur).SetEase(Ease.InCubic).SetUpdate(true).AsyncWaitForCompletion();
        await DownRHud.DOAnchorPosY(DownHudTopPosY, TweenDur).SetEase(Ease.InCubic).SetUpdate(true).AsyncWaitForCompletion();
    }

    public void SettingsAni()
    {

        SettingsMenu.DOScale(0.81f, TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true);
        

    }
    async Task SettingsAniOutro()
    {

        await SettingsMenu.DOScale(0.7f, TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true).AsyncWaitForCompletion();

    }

    public void ResoultsAni()
    {

        resoultsAni.DOScale(1, ReTweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        ResultFade.DOFade(1, 0.2f).SetEase(Ease.OutFlash);
        
    }
    async Task RetryAni()
    {

        await resoultsAni.DOScale(1.6f, 0.5f).SetEase(Ease.OutCubic).SetUpdate(true).AsyncWaitForCompletion();
        ResultFade.DOFade(0, 0.2f).SetEase(Ease.OutFlash);

    }
    public void ShockBtnAni()
    {

        ShockBtn.DOScale(1.8f, ReTweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        ShockBtn.DOScale(0.7f,ReTweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        ShockBtn.DOScale(1,ReTweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        
    }
    
}
