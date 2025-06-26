
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public Hat[] hats;
    public Body[] bodies;
    public CameraSkin[] cameraSkins;

    public GameObject shopItemPrefabHat;
    public GameObject shopItemPrefabBodie;
    public GameObject shopItemPrefabCamera;
    public Transform hatContainer;
    public Transform bodyContainer;
    public Transform cameraContainer;

    public Text trickPointsText;

    private ShopData shopData;

    private List<ShopItemUI> hatItems = new List<ShopItemUI>();
    private List<ShopItemUI> bodyItems = new List<ShopItemUI>();
    private List<ShopItemUI> cameraItems = new List<ShopItemUI>();

    public GameObject wardrobePanel;
    public GameObject shopPanel;

    public Transform hatWardrobeContainer;
    public Transform bodyWardrobeContainer;
    public Transform cameraWardrobeContainer;
    public GameObject wardrobeItemPrefabHat;
    public GameObject wardrobeItemPrefabBodie;
    public GameObject wardrobeItemPrefabCamera;

    private List<WardrobeItemUI> hatWardrobeItems = new();
    private List<WardrobeItemUI> bodyWardrobeItems = new();
    private List<WardrobeItemUI> cameraWardrobeItems = new();

    [SerializeField] LoadSystem loadSystem;
    [SerializeField] SaveSystem saveSystem;



    void Start()
    {
        LoadShop();
        CreateShopItems();
        UpdateUI();
       
    }

    public void Init(PlayerData playerData)
    {
        shopData.trickPoints = playerData.trickpoints;
    }
    
    void LoadShop()
    {     
        shopData = SaveShop.LoadData();
        if (shopData != null)
        {
            Debug.Log("entrei");
            Init(loadSystem.LoadPlayerData());
            Debug.Log("passei");
        }
       
        if (shopData == null)
        {
            shopData = new ShopData();
           
            

            foreach (var hat in hats)
                shopData.ownedHats.Add(new HatData { hatName = hat.hatName, purchased = false });

            foreach (var body in bodies)
                shopData.ownedBodies.Add(new BodyData { bodyName = body.bodyName, purchased = false });

            foreach (var cam in cameraSkins)
                shopData.ownedCameraSkins.Add(new CameraSkinData { cameraSkinName = cam.cameraSkinName, purchased = false });
              Init(loadSystem.LoadPlayerData());
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
    public void EquipCamera(int index)
    {
        CameraSkin skin = cameraSkins[index];
        var data = shopData.ownedCameraSkins[index];
        if (!data.purchased) return;

        shopData.equippedCameraSkinName = skin.cameraSkinName;
        SaveShop.SaveData(shopData);
        SkinManager.instance.EquipCameraSkin(skin);
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
        for (int i = 0; i < cameraSkins.Length; i++)
        {
            var obj = Instantiate(shopItemPrefabCamera, cameraContainer);
            var item = obj.GetComponent<ShopItemUI>();
            item.Init(i, false, this, isCamera: true);
            cameraItems.Add(item);
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
        saveSystem.SavePlayerData("", shopData.trickPoints);
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
        saveSystem.SavePlayerData("", shopData.trickPoints);
        SaveShop.SaveData(shopData);
        UpdateUI();
    }

    public void OnCameraClick(int index)
    {
        var skin = cameraSkins[index];
        var data = shopData.ownedCameraSkins[index];

        if (!data.purchased && shopData.trickPoints >= skin.price)
        {
            shopData.trickPoints -= skin.price;
            data.purchased = true;
        }
        else if (data.purchased)
        {
            shopData.equippedCameraSkinName = skin.cameraSkinName;
            SkinManager.instance.EquipCameraSkin(skin);
        }
        saveSystem.SavePlayerData("", shopData.trickPoints);
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
        for (int i = 0; i < cameraItems.Count; i++)
        {
            var ui = cameraItems[i];
            var cam = cameraSkins[i];
            var data = shopData.ownedCameraSkins[i];

            ui.icon.sprite = cam.previewSprite;
            ui.priceText.text = cam.price + " TP";

            Color color = ui.icon.color;

            if (data.purchased)
                color.a = 0.4f; // Apagadinho se já comprado
            else if (shopData.trickPoints < cam.price)
                color.a = 0.2f; // Mais apagado se não dá pra comprar
            else
                color.a = 1f; // Aceso se comprável

            ui.icon.color = color;
        }
    }

    [ContextMenu("ResetSave")]
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
        foreach (Transform child in cameraWardrobeContainer) Destroy(child.gameObject);

        hatWardrobeItems.Clear();
        bodyWardrobeItems.Clear();
        cameraWardrobeItems.Clear();

        // Chapéus
        for (int i = 0; i < hats.Length; i++)
        {
            var data = shopData.ownedHats[i];
            if (!data.purchased) continue;

            var obj = Instantiate(wardrobeItemPrefabHat, hatWardrobeContainer);
            var ui = obj.GetComponent<WardrobeItemUI>();

            ui.icon.sprite = hats[i].hatPreviewSprite;
            ui.nameText.text = hats[i].hatName;
            ui.Init(i, true, this);
            // Apagar o ícone se estiver equipado
            if (shopData.equippedHatName == hats[i].hatName)
            {
                Color c = ui.icon.color;
                c.a = 0.4f;
                ui.icon.color = c;
            }
            hatWardrobeItems.Add(ui);
        }

        // Corpos
        for (int i = 0; i < bodies.Length; i++)
        {
            var data = shopData.ownedBodies[i];
            if (!data.purchased) continue;

            var obj = Instantiate(wardrobeItemPrefabBodie, bodyWardrobeContainer);
            var ui = obj.GetComponent<WardrobeItemUI>();

            ui.icon.sprite = bodies[i].bodyPreviewSprite;
            ui.nameText.text = bodies[i].bodyName;
            ui.Init(i, false, this);

            if (shopData.equippedBodyName == bodies[i].bodyName)
            {
                Color c = ui.icon.color;
                c.a = 0.4f;
                ui.icon.color = c;
            }

            bodyWardrobeItems.Add(ui);
        }

        // Câmeras
        for (int i = 0; i < cameraSkins.Length; i++)
        {
            var data = shopData.ownedCameraSkins[i];
            if (!data.purchased) continue;

            var obj = Instantiate(wardrobeItemPrefabCamera, cameraWardrobeContainer);
            var ui = obj.GetComponent<WardrobeItemUI>();

            ui.icon.sprite = cameraSkins[i].previewSprite;
            ui.nameText.text = cameraSkins[i].cameraSkinName;
            ui.Init(i, false, this, isCamera: true);

            // Apagar o ícone se for o equipado
            if (shopData.equippedCameraSkinName == cameraSkins[i].cameraSkinName)
            {
                Color c = ui.icon.color;
                c.a = 0.4f;
                ui.icon.color = c;
            }

            cameraWardrobeItems.Add(ui);
        }
    }
}