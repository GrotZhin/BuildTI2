using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Player player;
   public BoxCollider2D boxCollider2D;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();  
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x  -= player.speed.x * Time.fixedDeltaTime;
        if (pos.x < -10)
        {
            Destroy(gameObject);
        }


        transform.position = pos;
    }
}
