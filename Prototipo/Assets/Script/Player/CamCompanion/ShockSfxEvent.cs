using UnityEngine;
using RWM;

public class ShockSfxEvent : MonoBehaviour
{
    public Transform Cam;
    public GameObject Shockpp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void SoundPlay()
    {
        soundManager.PlaySound(SoundType.Shock);
    }
    
     void PPSpawn()
    {
        
        Instantiate(Shockpp, transform.position, Quaternion.identity, Cam);
    }
}
