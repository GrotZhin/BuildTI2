using UnityEngine;

[CreateAssetMenu(fileName = "NewCameraSkin", menuName = "Shop/Camera Skin")]
public class CameraSkin : ScriptableObject
{
    public string cameraSkinName;
    public Material cameraMaterial;
    public Mesh CameraMesh;
    public Sprite previewSprite;
    public int price;
    private bool pruchased = false;
}