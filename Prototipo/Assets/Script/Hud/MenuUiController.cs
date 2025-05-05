using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using DG.Tweening.Core;
using UnityEngine.UI;
using System.Threading.Tasks;

public class MenuUiController : MonoBehaviour
{
  
   
    public GameObject settingsPanel;
    public GameObject shopPanel;

    //Dotween animations
    [SerializeField] RawImage Fade;
    [SerializeField] RectTransform MenuAni;
    [SerializeField] RectTransform SettingsMenu;
    [SerializeField] RectTransform ShopTrans;

    //DOtween positions
    [SerializeField] float TweenDur;
    [SerializeField] float TweenShopDur;
    [SerializeField] float ShopTopPosx,ShopmiddlePosx;
    [SerializeField] float MenuSizein;


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
        SettingsAni();
    }
    public async void Apply()
    {
        settingsPanel.SetActive(false);
        await SettingsAniOutro();
    }
    public async void Play()
    {
        await Fadeani();
        SceneManager.LoadScene("GameScene");
       

    }
    public async void Shop()
    {
        NosincFadein();
        await ShopAniintro();
        NosincFade0ut();
        ShopAnioutro();
        shopPanel.SetActive(true);
        
    }
    public async void Back()
    {   
        NosincFadein();
        await ShopAniintro();
        NosincFade0ut();
        ShopAnioutro();
        shopPanel.SetActive(false) ;
    }

    async Task Fadeani(){

        MenuAni.DOScale(MenuSizein,TweenDur);
         await Fade.DOFade(1,TweenDur).AsyncWaitForCompletion();
         

    }

    public void NosincFadein()
    {
        Fade.DOFade(1,TweenShopDur);
    }
    public void NosincFade0ut()
    {
        Fade.DOFade(0,TweenShopDur);
    }

    public void SettingsAni(){

        SettingsMenu.DOScale(0.81f,0.08f).SetEase(Ease.InOutCubic);
        
    }
    async Task SettingsAniOutro(){

       await SettingsMenu.DOScale(0.7f,0.08f).SetEase(Ease.InOutCubic).SetUpdate(true).AsyncWaitForCompletion();
       
    }
    async Task ShopAniintro(){

       await ShopTrans.DOAnchorPosX(ShopmiddlePosx,TweenShopDur).SetEase(Ease.InOutFlash).SetUpdate(true).AsyncWaitForCompletion();
       await ShopTrans.DOShakeAnchorPos(TweenShopDur,10,10,0,false,true).SetEase(Ease.InOutFlash).AsyncWaitForCompletion();
       
    }

    public void ShopAnioutro(){
        ShopTrans.DOShakeAnchorPos(TweenShopDur,10,5).SetEase(Ease.InOutFlash);
        ShopTrans.DOAnchorPosX(ShopTopPosx,TweenShopDur).SetUpdate(true);
       
    }
}
