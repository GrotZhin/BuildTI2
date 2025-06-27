using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinManager : MonoBehaviour
{
    public static SkinManager instance;

    public SkinnedMeshRenderer hatRenderer;
    public SkinnedMeshRenderer bodyRenderer;
    public SkinnedMeshRenderer cameraRenderer;
    public SkinnedMeshRenderer hatRenderer2;
    public SkinnedMeshRenderer bodyRenderer2;


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
        hatRenderer2 = GameObject.Find("HatDefault2")?.GetComponent<SkinnedMeshRenderer>();
        bodyRenderer2 = GameObject.Find("Body2")?.GetComponent<SkinnedMeshRenderer>();
        LoadEquippedSkins();
    }

    public void Equiphat(Hat hat)
    {
        ApplyHatToRenderer(hatRenderer, bodyRenderer, hat);
        ApplyHatToRenderer(hatRenderer2, bodyRenderer2, hat);
    }

    // --------- helper privado ---------
    void ApplyHatToRenderer(SkinnedMeshRenderer target, SkinnedMeshRenderer reference, Hat hat)
    {
        if (target == null || reference == null || hat == null) return;

        // troca mesh e material
        target.sharedMesh = hat.hatMesh;
        target.sharedMaterial = hat.hatMaterial;

        // copia esqueleto do corpo
        target.bones = reference.bones;
        target.rootBone = reference.rootBone;

        // bounding box para culling correto
        target.localBounds = hat.hatMesh.bounds;
    }
    public void Equipbody(Body body)
    {
        if (bodyRenderer != null)
        {
            bodyRenderer.material = body.bodyMaterial;
            bodyRenderer.sharedMesh = body.bodyMesh;
        }
        if (bodyRenderer2 != null)
        {
            bodyRenderer2.material = body.bodyMaterial;
            bodyRenderer2.sharedMesh = body.bodyMesh;
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
