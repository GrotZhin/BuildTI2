using System.Linq;
using UnityEngine;

public class GroundCount : MonoBehaviour
{
    public static GroundCount gc;
    
    public static bool create = true;
    [SerializeField]GameObject[] gos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
    }


    // Update is called once per frame
    void Update()
    {
        gos = GameObject.FindGameObjectsWithTag("Ground");
        if (gos.Length > 5)
        {
            create = false;
        }
        else
        {
            create = true;
        }
        
    }
}
