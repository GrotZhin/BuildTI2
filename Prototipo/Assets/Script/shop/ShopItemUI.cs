using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopItemUI : MonoBehaviour
{
    public Image icon;
    public Button button;
    public Text priceText;
    [SerializeField] RectTransform Ani;


    [HideInInspector] public int index;
    [HideInInspector] public bool isHat;
    [HideInInspector] public ShopManager shopManager;
    public bool isCamera;

    public void Init(int i, bool isHatItem, ShopManager manager, bool isCamera = false)
    {
        index = i;
        isHat = isHatItem;
        this.isCamera = isCamera;
        shopManager = manager;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Anim();
        if (isCamera)
            shopManager.OnCameraClick(index);
        else if (isHat)
            shopManager.OnHatClick(index);
        else
            shopManager.OnBodyClick(index);
    }

    public async void Anim()
    {
        Ani.DOShakeAnchorPos(0.2f, 20,10,90,false,true).SetEase(Ease.OutFlash);
        await Ani.DOScale(1.2f, 0.1f).SetEase(Ease.OutFlash).AsyncWaitForCompletion();
        await Ani.DOScale(0.8f, 0.1f).SetEase(Ease.OutFlash).AsyncWaitForCompletion();
        await Ani.DOScale(1, 0.1f).SetEase(Ease.OutFlash).AsyncWaitForCompletion();
        
    }
}