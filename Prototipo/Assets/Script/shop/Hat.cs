
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Hat")]
public class Hat : ScriptableObject
{
    public string hatName;    // deve bater com o nome do GameObject
    public int price;
    public Sprite hatPreviewSprite;
    public Material overrideMaterial;
}
