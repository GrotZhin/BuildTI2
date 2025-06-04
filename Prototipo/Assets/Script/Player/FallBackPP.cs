using RWM;
using UnityEngine;

public class FallBackPP : MonoBehaviour
{
    public GameObject fbPP;
    Vector3 PP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void PARTICLE()
    {
        PP = new Vector3(transform.position.x, transform.position.y - 0.95f, transform.position.z);
        Instantiate(fbPP, PP, Quaternion.identity);
        soundManager.PlaySound(SoundType.FallBack);
    }
}
