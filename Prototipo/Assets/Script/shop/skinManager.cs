using UnityEngine;
using static skin;

public class skinManager : MonoBehaviour
{
    public static skinManager instance;

    public Skin skinEquipada;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //aplica a skin
    public void EquiparSkin(Skin novaSkin)
    {
        skinEquipada = novaSkin;
    }
}

