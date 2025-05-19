
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
using Unity.VisualScripting;

public class MenuUiController : MonoBehaviour
{
    public GameObject MainCam;
    public GameObject WardrobeCam;
    public GameObject MirrorCam;
    public GameObject MainMenuPanel;
    public GameObject WarPanel;
    public GameObject settingsPanel;
    public GameObject shopPanel;
    public GameObject shopIntro;
    public GameObject shopOutro;
    public GameObject RannaShopGO;
    public GameObject ShopkShopGO;

    public GameObject ShopchgRan;
    public GameObject ShopchgCam;

    public GameObject WdbchgRan;
    public GameObject WdbchgCam;

    public GameObject Ranna;
    public Animator Ranani;
    public GameObject CanvasB;

   

    //Dotween animations
    [SerializeField] RawImage Fade;
    [SerializeField] RectTransform MenuAni;
    [SerializeField] RectTransform SettingsMenu;
    [SerializeField] RectTransform ShopTrans;
    [SerializeField] RectTransform WdbPanel;
    [SerializeField] RectTransform WdbPanel2;
    [SerializeField] RectTransform ShopPanel;
    [SerializeField] RectTransform ShopPanel2;
    [SerializeField] RectTransform RannaIcon;
    [SerializeField] RectTransform CamIcon;
    [SerializeField] RectTransform WdbRannaIcon;
    [SerializeField] RectTransform WdbCamIcon;
    [SerializeField] RawImage Backbtn;
    [SerializeField] Sprite[] RannaShop;
    [SerializeField] Image RannaShopRend;
    [SerializeField] Sprite[] ShopkShop;
    [SerializeField] Image ShopkShopRend;


    //DOtween positions
    [SerializeField] float TweenDur;
    [SerializeField] float TweenShopDur;
    [SerializeField] float TweenShopiconDur;
    [SerializeField] float ShopTopPosx, ShopmiddlePosx;
    [SerializeField] float ShopPanelTopPosx, ShopPanelmiddlePosx;
    [SerializeField] float MenuSizein;


    // Start is called before the first frame update
    public void Start()
    {
        Ranani = Ranna.GetComponent<Animator>();
        CanvasB.SetActive(false);
    }
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
        CanvasB.SetActive(true);
        await Fadeani();
        SceneManager.LoadScene("GameScene");


    }
    public async void Wardrobe()
    {
        shopIntro.SetActive(true);
        shopOutro.SetActive(false);
        CanvasB.SetActive(true);
        NosincFadein();
        RannaAniWdb();
        await ShopAniintro();
        settingsPanel.SetActive(false);
        CanvasB.SetActive(false);
        NosincFade0ut();
        ShopAnioutro();
        WdbchgCam.SetActive(false);
        WdbchgRan.SetActive(true);
        MainCam.SetActive(false);
        MirrorCam.SetActive(true);
        WarPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        WardrobeCam.SetActive(true);
        await WdbPanelAniintro();
    }
    public async void WdbBack()
    {
        ShopBackAnioutro();
        shopOutro.SetActive(true);
        shopIntro.SetActive(false);
        NosincFadein();
        WdbPanelAnioutro();
        WdbPanel2Anioutro();
        await ShopAniintro();
        RannaAniMenu();
        NosincFade0ut();
        ShopAnioutro();
        MainCam.SetActive(true);
        MainMenuPanel.SetActive(true);
        WardrobeCam.SetActive(false);
        MirrorCam.SetActive(false);
        WarPanel.SetActive(false);

    }
    public async void WdbChange()
    {
        WdbIconShake();
        WdbPanelAnioutro();
        WdbchgRan.SetActive(false);
        WdbchgCam.SetActive(true);
        await WdbPanel2Aniintro();

    }
    public async void WdbChange2()
    {
        WdbIconShake();
        WdbPanel2Anioutro();
        WdbchgCam.SetActive(false);
        WdbchgRan.SetActive(true);
        await WdbPanelAniintro();
    }
    public async void Shop()
    {
        shopIntro.SetActive(true);
        shopOutro.SetActive(false);
        CanvasB.SetActive(true);
        NosincFadein();
        await ShopAniintro();
        RannaShopGO.SetActive(true);
        ShopkShopGO.SetActive(true);
        CanvasB.SetActive(false);
        RannaShopRand();
        NosincFade0ut();
        ShopAnioutro();
        ShopBackAniintro();
        ShopchgCam.SetActive(false);
        ShopchgRan.SetActive(true);
        shopPanel.SetActive(true);
        settingsPanel.SetActive(false);
        await ShopPanelAniintro();
        
    }

    public async void ShopChange()
    {
        ShopIconShake();
        ShopPanelAnioutro();
        ShopchgRan.SetActive(false);
        ShopchgCam.SetActive(true);
        await ShopPanel2Aniintro();

    }
    public async void ShopChange2()
    {
        ShopIconShake();
        ShopPanel2Anioutro();
        ShopchgCam.SetActive(false);
        ShopchgRan.SetActive(true);
        await ShopPanelAniintro();
    }
    public async void Back()
    {
        ShopBackAnioutro();
        shopOutro.SetActive(true);
        shopIntro.SetActive(false);
        NosincFadein();
        ShopPanelAnioutro();
        ShopPanel2Anioutro();
        await ShopAniintro();
        RannaShopGO.SetActive(false);
        ShopkShopGO.SetActive(false);
        NosincFade0ut();
        ShopAnioutro();
        shopPanel.SetActive(false);
    }

    async Task Fadeani()
    {

        MenuAni.DOScale(MenuSizein, TweenDur);
        await Fade.DOFade(1, TweenDur).AsyncWaitForCompletion();


    }
    #region TweenAni
    public void NosincFadein()
    {
        Fade.DOFade(1, TweenShopDur);
    }
    public void NosincFade0ut()
    {
        Fade.DOFade(0, TweenShopDur);
    }

    public void SettingsAni()
    {

        SettingsMenu.DOScale(0.81f, 0.08f).SetEase(Ease.InOutCubic);

    }
    async Task SettingsAniOutro()
    {

        await SettingsMenu.DOScale(0.7f, 0.08f).SetEase(Ease.InOutCubic).SetUpdate(true).AsyncWaitForCompletion();

    }
    public void ShopIconShake()
    {

        RannaIcon.DOShakeAnchorPos(TweenShopiconDur, 10, 10, 0, false, true).SetEase(Ease.InCubic);
        CamIcon.DOShakeAnchorPos(TweenShopiconDur, 10, 10, 0, false, true).SetEase(Ease.InCubic);

    }

    async Task ShopAniintro()
    {

        await ShopTrans.DOAnchorPosX(ShopmiddlePosx, TweenShopDur).SetEase(Ease.InOutFlash).SetUpdate(true).AsyncWaitForCompletion();
        await ShopTrans.DOShakeAnchorPos(TweenShopDur, 10, 10, 0, false, true).SetEase(Ease.InOutFlash).AsyncWaitForCompletion();

    }
    public void ShopAnioutro()
    {
        ShopTrans.DOShakeAnchorPos(TweenShopDur, 10, 5).SetEase(Ease.InOutFlash);
        ShopTrans.DOAnchorPosX(ShopTopPosx, TweenShopDur).SetUpdate(true);

    }
    async Task ShopPanelAniintro()
    {

        await ShopPanel.DOAnchorPosX(ShopmiddlePosx, TweenShopDur).SetEase(Ease.InOutFlash).SetUpdate(true).AsyncWaitForCompletion();

    }

    public void ShopPanelAnioutro()
    {
        ShopPanel.DOAnchorPosX(ShopTopPosx, TweenDur).SetUpdate(true);


    }

    async Task ShopPanel2Aniintro()
    {

        await ShopPanel2.DOAnchorPosX(ShopmiddlePosx, TweenShopDur).SetEase(Ease.InOutFlash).SetUpdate(true).AsyncWaitForCompletion();

    }

    public void ShopPanel2Anioutro()
    {
        ShopPanel2.DOAnchorPosX(ShopTopPosx, TweenDur).SetUpdate(true);


    }

    public void RannaShopRand()
    {
        float rand = Random.Range(0, RannaShop.Length);

        RannaShopRend.sprite = RannaShop[(int)rand];

        float rand2 = Random.Range(0, ShopkShop.Length);

        ShopkShopRend.sprite = ShopkShop[(int)rand2];
    }

    public void ShopBackAniintro()
    {

        Backbtn.DOFade(1, 2f).SetEase(Ease.OutCubic);

    }
    public void ShopBackAnioutro()
    {
        Backbtn.DOFade(0, 3);

    }
    #endregion

    #region WardrobeTweenAni

    async Task WdbPanelAniintro()
    {

        await WdbPanel.DOAnchorPosX(ShopmiddlePosx, TweenShopDur).SetEase(Ease.InOutFlash).SetUpdate(true).AsyncWaitForCompletion();

    }

    public void WdbPanelAnioutro()
    {
        WdbPanel.DOAnchorPosX(ShopTopPosx, TweenDur).SetUpdate(true);


    }

    async Task WdbPanel2Aniintro()
    {

        await WdbPanel2.DOAnchorPosX(ShopmiddlePosx, TweenShopDur).SetEase(Ease.InOutFlash).SetUpdate(true).AsyncWaitForCompletion();

    }

    public void WdbPanel2Anioutro()
    {
        WdbPanel2.DOAnchorPosX(ShopTopPosx, TweenDur).SetUpdate(true);


    }
    public void WdbIconShake()
    {

        WdbRannaIcon.DOShakeAnchorPos(TweenShopiconDur, 10, 10, 0, false, true).SetEase(Ease.InCubic);
        WdbCamIcon.DOShakeAnchorPos(TweenShopiconDur, 10, 10, 0, false, true).SetEase(Ease.InCubic);

    }
    #endregion

    public void RannaAniWdb()
    {
        if (Ranna != null){
            
            Ranani.SetBool("IsCustom", true);
        }
    }
    public void RannaAniMenu()
    {
        if (Ranna != null)
        {
            Ranani.SetBool("IsCustom", false); 
        }
    }
}


