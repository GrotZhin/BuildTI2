using UnityEngine;
using RWM;

public class ShockSfxEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void SoundPlay()
    {
        soundManager.PlaySound(SoundType.Shock);
    }
}
