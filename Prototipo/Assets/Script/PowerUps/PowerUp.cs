using UnityEngine;

public class PowerUp : MonoBehaviour
{
    Player player;
   public BoxCollider boxCollider;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();  
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
