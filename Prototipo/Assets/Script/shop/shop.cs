using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static skin;

public class shop : MonoBehaviour
{
    public List<Skin> skins;
    public int indexAtual;
    public GameObject botaoComprar;
    public GameObject botaoEquipar;
    public Text precoTexto;

    void Start()
    {
        AtualizarUI();
    }

    public void ProximaSkin()
    {
        indexAtual = (indexAtual + 1) % skins.Count;
        AtualizarUI();
    }

    public void SkinAnterior()
    {
        indexAtual = (indexAtual - 1 + skins.Count) % skins.Count;
        AtualizarUI();
    }

    public void ComprarSkin()
    {
        Skin skin = skins[indexAtual];
        if (!skin.comprada)
        {
            // Aqui você verificaria se o jogador tem dinheiro
            skin.comprada = true;
        }
        AtualizarUI();
    }

    public void EquiparSkin()
    {
        Skin skin = skins[indexAtual];
        if (skin.comprada)
        {
            skinManager.instance.EquiparSkin(skin);
        }
    }

    void AtualizarUI()
    {
        Skin skin = skins[indexAtual];
        precoTexto.text = "Preço: " + skin.preco;
        botaoComprar.SetActive(!skin.comprada);
        botaoEquipar.SetActive(skin.comprada);
    }
}