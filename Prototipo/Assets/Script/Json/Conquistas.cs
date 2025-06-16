using System.IO;
using UnityEngine;

public class Conquistas : MonoBehaviour
{
    [SerializeField] GameObject[] icons;
    [SerializeField] Transform refPosition;
    [SerializeField] public bool Distance500M = false;
    [SerializeField] public bool Distance1000M = false;
    [SerializeField] public bool Distance5000M = false;
    [SerializeField] public bool firstHit = false;
    [SerializeField] public bool firstDeath = false;
    [SerializeField] public bool tenDeaths = false;
    [SerializeField] public bool hundredDeaths = false;
    [SerializeField] public int deathCount = 0;
    public int DeathCount
    {
        get { return deathCount; }
        set { deathCount = value; }
    }

    public void Init(ConquistasData conquistas)
    {
        Distance500M = conquistas.Distance500M;
        Distance1000M = conquistas.Distance1000M;
        Distance5000M = conquistas.Distance5000M;
        this.firstHit = conquistas.firstHit;
        this.firstDeath = conquistas.firstDeath;
        this.tenDeaths = conquistas.tenDeaths;
        this.hundredDeaths = conquistas.hundredDeaths;
        this.deathCount = conquistas.deathCount;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Init(LoadConquistas());
    }

    // Update is called once per frame
    public void Distance500MUnlock()
    {
        Distance500M = true;

        Instantiate(icons[0]);
        Destroy(icons[0], 5);
    }

    public void Distance1000MUnlock()
    {
        Distance1000M = true;
        Instantiate(icons[1]);
        Destroy(icons[1], 5);
    }

    public void Distance5000MUnlock()
    {
        Distance5000M = true;
        Instantiate(icons[2]);
        Destroy(icons[2], 5);
    }
    public void FirstHit()
    {
        firstHit = true;
        Instantiate(icons[3]);
        Destroy(icons[3], 5);

    }

    public void FirstDeath()
    {
        firstDeath = true;
        Instantiate(icons[4]);
        Destroy(icons[4], 5);

    }
    public void TenDeaths()
    {
        tenDeaths = true;
        Instantiate(icons[5]);
        Destroy(icons[5], 5);
    }
    public void HundredDeaths()
    {
        hundredDeaths = true;
        Instantiate(icons[6]);
        Destroy(icons[6], 5);
    }

    public void SaveConquistas()
    {
        ConquistasData data = new ConquistasData(Distance500M, Distance1000M, Distance5000M, firstHit, firstDeath, tenDeaths, hundredDeaths, deathCount);

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/conquistas.json", json);
        Debug.Log(Application.persistentDataPath + "/conquistas.json");

    }
    public ConquistasData LoadConquistas()
    {
        string path = Application.persistentDataPath + "/conquistas.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            ConquistasData data = JsonUtility.FromJson<ConquistasData>(json);

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
