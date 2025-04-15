using UnityEngine;

public class Ground : MonoBehaviour
{
    public float groundHeight;
    BoxCollider2D collider2D;
    private void Awake()
    {
        collider2D = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (collider2D.size.y / 2);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
