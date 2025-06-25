
using UnityEngine;

[CreateAssetMenu(fileName = "NewHat", menuName = "Shop/Hat")]
public class Hat : ScriptableObject
{
    public string hatName;
    public Material hatMaterial;
    public Mesh hatMesh;
    public int price;
    public Sprite hatPreviewSprite;
    private bool pruchased = false;
}
