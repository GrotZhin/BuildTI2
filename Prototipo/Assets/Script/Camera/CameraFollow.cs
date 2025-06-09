using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Player player;
    public Transform Ran;
    public float StartTimer = 0.5f;

    public float Stp = 10;
    bool follow = false;
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



            if (follow == true)
            {
                Vector3 pos = transform.position;
                Vector2 playerPos = player.transform.position;
                pos.x = playerPos.x;
                pos.y = playerPos.y;

                transform.position = pos;
            }

            if (transform.position.y <= Stp && player.cheat == false)
            {
                follow = false;
                Vector3 currentPosition = transform.position;
                transform.position = currentPosition;

            }
            else
            {
                follow = true;
            }
        }
          
        
    }
}


