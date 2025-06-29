using UnityEngine;

public class Delete : MonoBehaviour
{
    public GameObject filho;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (filho == null)
        { 
            Destroy(gameObject);
        }
    }
}
