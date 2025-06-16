using System.Collections.Generic;

using UnityEngine;

public class ConquistasManager : MonoBehaviour
{
    Player player;
    Conquistas conquistas;
    public int DeathCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        conquistas = GameObject.Find("ConquistasManager").GetComponent<Conquistas>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        DeathCount = conquistas.DeathCount;
        Debug.Log(DeathCount);
    }

    // Update is called once per frame
    void Update()
    {

        if (player.distance >= 500 && conquistas.Distance500M == false)
        {
            conquistas.Distance500MUnlock();
            
        }
        if (player.distance >= 1000 && conquistas.Distance1000M == false)
        {
            conquistas.Distance1000MUnlock();
            
        }
        if (player.distance >= 5000 && conquistas.Distance5000M == false)
        {
            conquistas.Distance5000MUnlock();
            
        }
        if (player.ouch && conquistas.firstHit)
        {
            conquistas.FirstHit();
            
        }
        if (DeathCount == 1 && conquistas.firstDeath == false)
        {
            conquistas.FirstDeath();
            
        }
        if (DeathCount == 10 && conquistas.tenDeaths == false)
        {
            conquistas.TenDeaths();
            
        }
        if (DeathCount == 100 && conquistas.hundredDeaths == false)
        {
            conquistas.HundredDeaths();
           
        }
    }
    

}
