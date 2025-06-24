using UnityEngine;
using UnityEngine.UI;

public class WardrobeItemUI : MonoBehaviour
{
    public Image icon;
    public Button button;
    public Text nameText;

    [HideInInspector] public int index;
    [HideInInspector] public bool isHat;
    [HideInInspector] public ShopManager shopManager;

    public void Init(int i, bool isHatItem, ShopManager manager)
    {
        index = i;
        isHat = isHatItem;
        shopManager = manager;

        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (isHat)
            shopManager.EquipHat(index);
        else
            shopManager.EquipBody(index);
    }
}