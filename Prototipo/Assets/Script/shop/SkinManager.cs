using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] Transform[] hatParents = new Transform[2];   // pode ser o osso Head ou um vazio
    List<Dictionary<string, GameObject>> hatMaps = new();



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

    void BuildHatMaps()
    {
        hatMaps.Clear();

        foreach (var parent in hatParents)
        {
            var map = new Dictionary<string, GameObject>();

            if (parent)
            {
                foreach (Transform child in parent)            // chapéus-filhos
                    if (child.name.StartsWith("Hat"))      // filtragem
                        map[child.name] = child.gameObject;
            }
            else
                Debug.LogWarning("Hat parent ausente em hatParents[]");

            hatMaps.Add(map);   // mantém a ordem: 0  player1, 1  player2
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        for (int i = 0; i < hatParents.Length; i++)
            hatParents[i] = null;

        RefreshHatParents();

        BuildHatMaps();

        bodyRenderer = GameObject.Find("Body")?.GetComponent<SkinnedMeshRenderer>();
        bodyRenderer2 = GameObject.Find("Body2")?.GetComponent<SkinnedMeshRenderer>();
        cameraRenderer = GameObject.Find("CamBody")?.GetComponent<SkinnedMeshRenderer>();
        
       // LoadEquippedSkins();
        StartCoroutine(ApplyBodyNextFrame());
    }
    void RefreshHatParents()
    {
        for (int i = 0; i < hatParents.Length; i++)
        {
           
            string hatName = i == 0 ? "Hat_Default" : "Hat_Default2";
            var hatDefault = GameObject.Find(hatName);
            if (hatDefault)
            {
                hatParents[i] = hatDefault.transform.parent;
                continue;
            }

          
            string rootName = i == 0 ? "RannaModel" : "RannaModel2";
            var root = GameObject.Find(rootName);
            if (root)
            {
              
                var hats = root.transform.Find("Hats");
                hatParents[i] = hats ? hats : root.transform;
            }
        }
    }
    void ReapplySavedBody()
    {
        ShopData data = SaveShop.LoadData();
        if (data == null || string.IsNullOrEmpty(data.equippedBodyName))
            return;

        Body body = FindBodyByName(data.equippedBodyName);
        if (body == null)
        {
            Debug.LogWarning($"Corpo '{data.equippedBodyName}' não existe na lista allBodies.");
            return;
        }

        ApplyBodyToRenderers(body);   // helper que faz mesh + material
    }
    void ApplyBodyToRenderers(Body body)
    {
        if (bodyRenderer)
        {
            bodyRenderer.sharedMesh = body.bodyMesh;
            bodyRenderer.sharedMaterial = body.bodyMaterial;
        }
        if (bodyRenderer2)
        {
            bodyRenderer2.sharedMesh = body.bodyMesh;
            bodyRenderer.sharedMaterial = body.bodyMaterial;
        }
    }
    IEnumerator ApplyBodyNextFrame()
    {
        yield return null;    
        ReapplySavedBody();     
        LoadEquippedSkins();     
    }

    void EnsureHatParents()
    {
        for (int i = 0; i < hatParents.Length; i++)
        {
            if (hatParents[i] == null)
            {
               
                var defaults = GameObject.FindObjectsOfType<SkinnedMeshRenderer>()
                              .FirstOrDefault(r => r.name == (i == 0 ? "Hat_Default" : "Hat_Default2"));
                if (defaults) hatParents[i] = defaults.transform.parent;
            }
        }
    }

    public void EquipHat(Hat hat)
    {
        if (hat == null) return;

        foreach (var map in hatMaps)    
        {
            // Desliga todos os chapéus desse player
            foreach (var go in map.Values)
                go.SetActive(false);

            // Liga o chapéu escolhido (se existir no mapa)
            if (map.TryGetValue(hat.hatName, out var chosen))
                chosen.SetActive(true);
            var rend = chosen.GetComponent<SkinnedMeshRenderer>();
            if (hat.overrideMaterial)
                rend.sharedMaterial = hat.overrideMaterial;
        }
    }

    public void EquipBody(Body body)
    {
        if (body == null) return;

        if (bodyRenderer)
        {
            bodyRenderer.sharedMesh = body.bodyMesh;
            bodyRenderer.material = body.bodyMaterial;
        }

        if (bodyRenderer2)
        {
            bodyRenderer2.sharedMesh = body.bodyMesh;
            bodyRenderer2.material = body.bodyMaterial;
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
                EquipHat(hat);
            if (body != null)
                EquipBody(body);
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