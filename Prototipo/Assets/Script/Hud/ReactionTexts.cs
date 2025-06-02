using UnityEngine;
using DG.Tweening;

public class ReactionTexts : MonoBehaviour
{
    public GameObject com;
    public Transform Daddy;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            Instantiate(com, transform.position, Quaternion.identity, Daddy);

        }
        
        
    }
    
    
        
    
}
