using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Player player;
    Ground ground;

    public BoxCollider boxCollider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        ground = GameObject.Find("Ground").GetComponent<Ground>();
        boxCollider = GetComponent<BoxCollider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

  
    
        transform.position = pos;
    }
}
