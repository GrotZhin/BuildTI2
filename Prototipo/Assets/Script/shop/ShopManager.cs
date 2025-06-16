using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public Hat[] hats;
    public Body[] bodies;

    private int currentHatIndex = 0;
    private int currentBodyIndex = 0;

    public Image hatPreview;
    public Image bodyPreview;

    public Text hatPriceText;
    public Text bodyPriceText;
    public Text trickPointsText;

    public Button buyHatButton;
    public Button buyBodyButton;
    public Button equipButton;
    public Button resetButton;

    private ShopData shopData;

    void Start()
    {
        LoadShop();
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

    public void NextHat()
    {
        currentHatIndex = (currentHatIndex + 1) % hats.Length;
        UpdateUI();
    }

    public void PreviousHat()
    {
        currentHatIndex = (currentHatIndex - 1 + hats.Length) % hats.Length;
        UpdateUI();
    }

    public void NextBody()
    {
        currentBodyIndex = (currentBodyIndex + 1) % bodies.Length;
        UpdateUI();
    }

    public void PreviousBody()
    {
        currentBodyIndex = (currentBodyIndex - 1 + bodies.Length) % bodies.Length;
        UpdateUI();
    }

    public void BuyHat()
    {
        Hat hat = hats[currentHatIndex];
        var hatData = shopData.ownedHats.Find(h => h.hatName == hat.hatName);
        if (hatData.purchased) return;
        if (shopData.trickPoints < hat.price) return;

        shopData.trickPoints -= hat.price;
        hatData.purchased = true;
        SaveShop.SaveData(shopData);
        UpdateUI();
    }

    public void BuyBody()
    {
        Body body = bodies[currentBodyIndex];
        var bodyData = shopData.ownedBodies.Find(b => b.bodyName == body.bodyName);
        if (bodyData.purchased) return;
        if (shopData.trickPoints < body.price) return;

        shopData.trickPoints -= body.price;
        bodyData.purchased = true;
        SaveShop.SaveData(shopData);
        UpdateUI();
    }

    public void EquipHat()
    {
        Hat hat = hats[currentHatIndex];
        var hatData = shopData.ownedHats.Find(h => h.hatName == hat.hatName);
        if (!hatData.purchased) return;
        shopData.equippedHatName = hat.hatName;
        SaveShop.SaveData(shopData);
        SkinManager.instance.Equiphat(hat);

    }

    public void EquipBody()
    {
        Body body = bodies[currentBodyIndex];
        var bodyData = shopData.ownedBodies.Find(b => b.bodyName == body.bodyName);
        if (!bodyData.purchased) return;
        shopData.equippedBodyName = body.bodyName;
        SaveShop.SaveData(shopData);
        SkinManager.instance.Equipbody(body);
    }

    public void ResetSave()
    {
        SaveShop.ResetSave();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateUI()
    {
        Hat hat = hats[currentHatIndex];
        Body body = bodies[currentBodyIndex];

        hatPreview.sprite = hat.hatPreviewSprite;
        bodyPreview.sprite = body.bodyPreviewSprite;

        hatPriceText.text = hat.price + " TP";
        bodyPriceText.text = body.price + " TP";
        trickPointsText.text = "TP: " + shopData.trickPoints;

        var hatData = shopData.ownedHats.Find(h => h.hatName == hat.hatName);
        var bodyData = shopData.ownedBodies.Find(b => b.bodyName == body.bodyName);

        buyHatButton.interactable = !hatData.purchased;
        buyBodyButton.interactable = !bodyData.purchased;
    }
}
