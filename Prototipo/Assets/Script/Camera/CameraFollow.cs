using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Player player;
    public float StartTimer = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        StartTimer -= Time.deltaTime;

        if (StartTimer <= 0)
        {
            Vector3 pos = transform.position;
            Vector2 playerPos = player.transform.position;
            pos.x = playerPos.x;
            pos.y = playerPos.y;

            transform.position = pos;
        }
    }
}


