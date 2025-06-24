
using UnityEngine;

[CreateAssetMenu(fileName = "NewCam", menuName = "Shop/Cam")]
public class Cam : ScriptableObject
{
    public string camName;
    public Material camMaterial;
    public Mesh camMesh;
    public int price;
    public Sprite camPreviewSprite;
    private bool pruchased = false;
}