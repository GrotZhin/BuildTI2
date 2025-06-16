using UnityEngine;

[CreateAssetMenu(fileName = "NewBody", menuName = "Shop/Body")]
public class Body : ScriptableObject
{
    public string bodyName;
    public Material bodyMaterial;
    public Mesh bodyMesh;
    public int price;
    public Sprite bodyPreviewSprite;
}