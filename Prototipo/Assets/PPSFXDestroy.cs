using UnityEngine;
using UnityEngine.VFX;

public class PPSFXDestroy : MonoBehaviour
{
    
    public float sldTimer;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sldTimer += Time.deltaTime;
        if (sldTimer >= 1.2f)
        {
            GetComponent<VisualEffect>().Stop();
            sldTimer = 0;
        }
        
    }
}
