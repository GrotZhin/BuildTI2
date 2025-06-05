using UnityEngine;

public class Grind : MonoBehaviour
{
    public float groundHeight =0;
    public Quaternion rotation;
    BoxCollider collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider.GetComponent<BoxCollider>();
        groundHeight = transform.position.y + (collider.size.y / 2);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
