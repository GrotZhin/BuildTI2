using System.IO;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]
public class ConquistasData
{

    public bool Distance500M = false;
    public bool Distance1000M = false;
    public bool Distance5000M = false;
    public bool firstHit = false;
    public bool firstDeath = false;
    public bool tenDeaths = false;
    public bool hundredDeaths = false;
    public int deathCount = 0;

    public ConquistasData(bool distance500M, bool distance1000M, bool distance5000M, bool firstHit,bool firstDeath, bool tenDeaths, bool hundredDeaths, int deathCount)
    {
        Distance500M = distance500M;
        Distance1000M = distance1000M;
        Distance5000M = distance5000M;
        this.firstHit = firstHit;
        this.firstDeath = firstDeath;
        this.tenDeaths = tenDeaths;
        this.hundredDeaths = hundredDeaths;
        this.deathCount = deathCount;
    }

    public int DeathCount
    {
        get { return deathCount; }
        set { deathCount = value; }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    // Update is called once per frame


}
