using UnityEngine;
using DG.Tweening;
using RWM;

public class glassbreakPP : MonoBehaviour
{
    public GameObject pp;
    public GameObject glass;

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
            Destroy(glass);
            soundManager.PlaySound(SoundType.Glass);
        }
    }
}
