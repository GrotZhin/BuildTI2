using UnityEngine;
using UnityEngine.VFX;

public class Grindpp : MonoBehaviour
{
    public Player Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Player.isGrind)
        {
            GetComponent<VisualEffect>().Play();
        }
        if (Player.isGrind == false)
        {
            GetComponent<VisualEffect>().Play();
        }
        
        
    }
}
