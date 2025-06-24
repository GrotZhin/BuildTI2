
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public Hat[] hats;
    public Body[] bodies;

    public GameObject shopItemPrefabHat;
    public GameObject shopItemPrefabBodie;
    public Transform hatContainer;
    public Transform bodyContainer;

    public Text trickPointsText;

    private ShopData shopData;

    private List<ShopItemUI> hatItems = new List<ShopItemUI>();
    private List<ShopItemUI> bodyItems = new List<ShopItemUI>();
    
    public GameObject wardrobePanel;
    public GameObject shopPanel;

    public Transform hatWardrobeContainer;
    public Transform bodyWardrobeContainer;
    public GameObject wardrobeItemPrefabHat;
    public GameObject wardrobeItemPrefabBodie;
    
    private List<WardrobeItemUI> hatWardrobeItems = new();
    private List<WardrobeItemUI> bodyWardrobeItems = new();

    
    void Start()
    {
        LoadShop();
        CreateShopItems();
        UpdateUI();
    }

    void LoadShop()
    {
        shopData = SaveShop.LoadData();
        if (shopData == null)
        {
            shopData = new ShopData();
            shopData.trickPoints = 1000;

            foreach (var hat in hats)
                shopData.ownedHats.Add(new HatData { hatName = hat.hatName, purchased = false });

            foreach (var body in bodies)
                shopData.ownedBodies.Add(new BodyData { bodyName = body.bodyName, purchased = false });

            SaveShop.SaveData(shopData);
        }
    }
    public void EquipHat(int index)
    {
        Hat hat = hats[index];
        var hatData = shopData.ownedHats.Find(h => h.hatName == hat.hatName);
        if (!hatData.purchased) return;

        shopData.equippedHatName = hat.hatName;
        SaveShop.SaveData(shopData);
        SkinManager.instance.Equiphat(hat);
    }

    public void EquipBody(int index)
    {
        Body body = bodies[index];
        var bodyData = shopData.ownedBodies.Find(b => b.bodyName == body.bodyName);
        if (!bodyData.purchased) return;

        shopData.equippedBodyName = body.bodyName;
        SaveShop.SaveData(shopData);
        SkinManager.instance.Equipbody(body);
    }

    void CreateShopItems()
    {
        for (int i = 0; i < hats.Length; i++)
        {
            var obj = Instantiate(shopItemPrefabHat, hatContainer);
            var item = obj.GetComponent<ShopItemUI>();
            item.Init(i, true, this);
            hatItems.Add(item);
        }

        for (int i = 0; i < bodies.Length; i++)
        {
            var obj = Instantiate(shopItemPrefabBodie, bodyContainer);
            var item = obj.GetComponent<ShopItemUI>();
            item.Init(i, false, this);
            bodyItems.Add(item);
        }
    }

    public void OnHatClick(int index)
    {
        var hat = hats[index];
        var data = shopData.ownedHats[index];

        if (!data.purchased && shopData.trickPoints >= hat.price)
        {
            shopData.trickPoints -= hat.price;
            data.purchased = true;
        }
        else if (data.purchased)
        {
            shopData.equippedHatName = hat.hatName;
            SkinManager.instance.Equiphat(hat);
        }

        SaveShop.SaveData(shopData);
        UpdateUI();
    }

    public void OnBodyClick(int index)
    {
        var body = bodies[index];
        var data = shopData.ownedBodies[index];

        if (!data.purchased && shopData.trickPoints >= body.price)
        {
            shopData.trickPoints -= body.price;
            data.purchased = true;
        }
        else if (data.purchased)
        {
            shopData.equippedBodyName = body.bodyName;
            SkinManager.instance.Equipbody(body);
        }

        SaveShop.SaveData(shopData);
        UpdateUI();
    }

    void UpdateUI()
    {
        trickPointsText.text = "TP: " + shopData.trickPoints;

        for (int i = 0; i < hatItems.Count; i++)
        {
            var ui = hatItems[i];
            var hat = hats[i];
            var data = shopData.ownedHats[i];

            ui.icon.sprite = hat.hatPreviewSprite;
            ui.priceText.text = hat.price + " TP";

            // Ajustar transparência com base na compra/TP
            Color color = ui.icon.color;

            if (data.purchased)
                color.a = 0.4f; // APAGADINHO mesmo se já estiver comprado
            else if (shopData.trickPoints < hat.price)
                color.a = 0.2f; // Mais apagado ainda se nem dá pra comprar
            else
                color.a = 1f; 

            ui.icon.color = color;
        }


        for (int i = 0; i < bodyItems.Count; i++)
        {
            var ui = bodyItems[i];
            var body = bodies[i];
            var data = shopData.ownedBodies[i];

            ui.icon.sprite = body.bodyPreviewSprite;
            ui.priceText.text = body.price + " TP";

            Color color = ui.icon.color;

            if (data.purchased)
                color.a = 0.4f; // APAGADINHO mesmo se já estiver comprado
            else if (shopData.trickPoints < body.price)
                color.a = 0.2f; // Mais apagado ainda se nem dá pra comprar
            else
                color.a = 1f; 

            ui.icon.color = color;
        }

    }

    public void ResetSave()
    {
        SaveShop.ResetSave();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    public void OpenWardrobe()
    {
        wardrobePanel.SetActive(true);
        shopPanel.SetActive(false);

        UpdateWardrobe();
    }
    void UpdateWardrobe()
    {
        foreach (Transform child in hatWardrobeContainer) Destroy(child.gameObject);
        foreach (Transform child in bodyWardrobeContainer) Destroy(child.gameObject);

        hatWardrobeItems.Clear();
        bodyWardrobeItems.Clear();
        

        for (int i = 0; i < hats.Length; i++)
        {
            var data = shopData.ownedHats[i];
            if (!data.purchased) continue;

            var obj = Instantiate(wardrobeItemPrefabHat, hatWardrobeContainer);
            var ui = obj.GetComponent<WardrobeItemUI>();

            ui.icon.sprite = hats[i].hatPreviewSprite;
            ui.nameText.text = hats[i].hatName;
            ui.Init(i, true, this);
        }
        for (int i = 0; i < bodies.Length; i++)
        {
            var data = shopData.ownedBodies[i];
            if (!data.purchased) continue;

            var obj = Instantiate(wardrobeItemPrefabBodie, bodyWardrobeContainer); 
            var ui = obj.GetComponent<WardrobeItemUI>();

            ui.icon.sprite = bodies[i].bodyPreviewSprite;
            ui.nameText.text = bodies[i].bodyName;
            ui.Init(i, false, this); 
        }

    }

}