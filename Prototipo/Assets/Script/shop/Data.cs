using UnityEngine;
using System.IO;
using Unity.VisualScripting;
public class Data : MonoBehaviour
{
    public class shop;
    public class Skin;
    public class SkinManager;


    [System.Serializable]
    public float score;
    public string nome;
    public Material hatMaterial;
    public Material bodyMaterial;
    public Mesh hatMesh;
    public Mesh bodyMesh;
    public int preco;
    public bool comprada;
    public Text precoTexto;
    public List<Skin> skins;
    public Skin skinEquipada;

    Data() 
    {
        //score = ;
        nome = Skin.nome;
        hatMaterial = Skin.hatMaterial;
        bodyMaterial = Skin.bodyMaterial;
        hatMesh = Skin.hatMesh;
        bodyMesh = Skin.bodyMesh;
        preco = Skin.preco;
        comprada = Skin.comprada;
        precoTexto = shop.precoTexto;
        skins = shop.skins;
        skinEquipada = SkinManager.skinEquipada;
    }
    public void SetData()
    {
    }    
}