using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        Vector2 playerPos = player.transform.position;
        pos.x = playerPos.x ;
        pos.y = playerPos.y ;

        transform.position = pos;
    }
}
