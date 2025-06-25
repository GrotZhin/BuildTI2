using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinManager : MonoBehaviour
{
    public static SkinManager instance;

    public SkinnedMeshRenderer hatRenderer;
    public SkinnedMeshRenderer bodyRenderer;
    public SkinnedMeshRenderer cameraRenderer;
    

    public Hat[] allHats;
    public Body[] allBodies;
    public CameraSkin[] allCameraSkins;
    
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;  
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        hatRenderer = GameObject.Find("HatDefault")?.GetComponent<SkinnedMeshRenderer>();
        bodyRenderer = GameObject.Find("Body")?.GetComponent<SkinnedMeshRenderer>();
        cameraRenderer = GameObject.Find("CamBody")?.GetComponent<SkinnedMeshRenderer>();
        LoadEquippedSkins();
    }

    public void Equiphat(Hat hat)
    {
        if (hatRenderer != null)
        {
            hatRenderer.material = hat.hatMaterial;
            hatRenderer.sharedMesh = hat.hatMesh;
        }
    }

    public void Equipbody(Body body)
    {
        if (bodyRenderer != null)
        {
            bodyRenderer.material = body.bodyMaterial;
            bodyRenderer.sharedMesh = body.bodyMesh;
        }
    }
    
    public void EquipCameraSkin(CameraSkin skin)
    {
        if (cameraRenderer != null)
        {
            cameraRenderer.material = skin.cameraMaterial;
        }
    }

    public void LoadEquippedSkins()
    {
        ShopData data = SaveShop.LoadData();
        if (data != null)
        {
            Hat hat = FindHatByName(data.equippedHatName);
            Body body = FindBodyByName(data.equippedBodyName);
            CameraSkin cameraSkin = FindCameraSkinByName(data.equippedCameraSkinName);
            
            if (cameraSkin != null)
                EquipCameraSkin(cameraSkin);
            if (hat != null)
                Equiphat(hat);
            if (body != null)
                Equipbody(body);
        }
        
    }

    private Hat FindHatByName(string name)
    {
        foreach (var hat in allHats)
            if (hat.hatName == name) return hat;
        return null;
    }

    private Body FindBodyByName(string name)
    {
        foreach (var body in allBodies)
            if (body.bodyName == name) return body;
        return null;
    }

    private CameraSkin FindCameraSkinByName(string name)
    {
        foreach (var skin in allCameraSkins)
            if (skin.cameraSkinName == name) return skin;
        return null;
    }
}
