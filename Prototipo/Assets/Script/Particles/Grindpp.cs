using UnityEngine;
using UnityEngine.VFX;

public class Grindpp : MonoBehaviour
{
    public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        }

    // Update is called once per frame
    void Update()
    {
        
        if (player.isGrind)
        {
            GetComponent<VisualEffect>().Play();
            
        }
        if (player.isGrind == false)
        {
            GetComponent<VisualEffect>().Play();
        }
        
        
    }
}
