using UnityEngine;

public class Sekker : MonoBehaviour
{
    Player player;
    public float gravity;
    public Vector2 speed;
    public int score = 0;
    public float maxXSpeed = 100;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerPos = player.transform.position;
        
        Vector3 pos = new Vector3(playerPos.x - 5, playerPos.y, playerPos.z);

        
        speed.x += acceleration * Time.fixedDeltaTime;
        float speedRatio = speed.x / maxXSpeed;
        acceleration = maxAcceleration * (1 - speedRatio);

        if (speed.x >= maxXSpeed)
        {
            speed.x = maxXSpeed;
        }
        if (player.speed.x <= 0)
        {
        }

       
        transform.position = pos;
    }
}
