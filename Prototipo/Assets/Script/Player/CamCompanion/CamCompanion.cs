using UnityEngine;

public class CamCompanion : MonoBehaviour
{
    public Transform Player;
    public float time = 0.3f;
    public float RanSpd = 10f; 
    public float SpdDamp = 0.1f; 
    
    private Vector3 velocity;
    private Vector3 playerVelocity;
    Vector3 PlayerPos;

    void Start()
    {
        Player = GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        transform.LookAt(Player);
        playerVelocity = (Player.position - transform.position) / Time.fixedDeltaTime;
        PlayerPos =new Vector3 (Player.position.x - 1, Player.position.y + 0.3f, Player.position.z);
;
        if (playerVelocity.magnitude > RanSpd)
        {
            transform.position = Vector3.SmoothDamp(transform.position, PlayerPos, ref velocity, SpdDamp);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, PlayerPos, ref velocity, time);
        }
    }
}