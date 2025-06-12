using System.Threading;
using UnityEngine;

public class Sekker : MonoBehaviour
{
    Player player;
    Ground ground;
    public float gravity;
    public Vector2 speed;
    public int score = 0;
    public float time;
    public float maxXSpeed = 100;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    public float jumpSpeed = 20;
    public float groundHeight;
    private CharacterController characterController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }
    void Update()
    {
        ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<Ground>();
         if (player.assist)
        {
            time += Time.fixedDeltaTime;
            if (time >= 0.5)
            {
                Jump();
                player.assist = false;
                time = 0;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector2 pos = transform.position;

        if (pos.y <= 3)
        {
            player.sekkerInstantiate = false;
            Destroy(gameObject);
        }    

        if (!characterController.isGrounded)
        {
            speed.y += gravity * Time.fixedDeltaTime;
        }

        if (characterController.isGrounded)
        {
            speed.x += acceleration * Time.fixedDeltaTime;
            float speedRatio = speed.x / maxXSpeed;
            acceleration = maxAcceleration * (1 - speedRatio);


        }
        
       

        characterController.Move(new Vector2(speed.x, speed.y) * Time.deltaTime);
    }
    public void Jump()
    {

        speed.y = Mathf.Sqrt(jumpSpeed * -2.0f * gravity);
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Vector3 pos = transform.position;
        Ground ground = hit.collider.GetComponent<Ground>();


        if (ground == null && hit.moveDirection == Vector3.up)
        {
            groundHeight = ground.groundHeight + 0.35f;
            pos.y = groundHeight;
            speed.y = 0;
        }

        if (ground == null && hit.moveDirection == Vector3.right)
        {
           
            speed.x = 5;
        }


    }

    void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            //HitObstacle(obstacle);
        }
    }
}