using RWM;
using UnityEngine;

public class Paperspawn : MonoBehaviour
{
    
    public GameObject pp;
  

    public Camera CAM;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            Instantiate(pp, transform.position, Quaternion.identity);
          
            soundManager.PlaySound(SoundType.Paper);
        }
    }
}
