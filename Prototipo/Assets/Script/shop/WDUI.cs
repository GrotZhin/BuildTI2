using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WardrobeItemUI : MonoBehaviour
{
    public Image icon;
    public Button button;
    

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
            shopManager.EquipCamera(index);
        else if (isHat)
            shopManager.EquipHat(index);
        else
            shopManager.EquipBody(index);
    }
    
    public async void Anim()
    {
        Ani.DOShakeAnchorPos(0.5f,10, 10, 0, false, true).SetEase(Ease.InCubic);
        await Ani.DOScale(1.05f, 0.1f).SetEase(Ease.OutFlash).AsyncWaitForCompletion();
        await Ani.DOScale(0.9f, 0.1f).SetEase(Ease.OutFlash).AsyncWaitForCompletion();
        await Ani.DOScale(1, 0.1f).SetEase(Ease.OutFlash).AsyncWaitForCompletion();
        
    }

}