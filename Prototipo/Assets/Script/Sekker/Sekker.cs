using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Sekker : MonoBehaviour
{
    Player player;
    Ground ground;
    public GameObject Stalker;
    public Animator seekani;
    public float gravity;
    public Vector2 speed;
    public int score = 0;
    public float time;
    float timerStop;
    bool stopMove;
    public float maxXSpeed = 100;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    public float jumpSpeed = 20;
    public float groundHeight;
    public float fbkTimer;
    private CharacterController characterController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        seekani = Stalker.GetComponent<Animator>();

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
       
        if (pos.y <= 3 ||Vector3.Distance(player.transform.position, transform.position) >= 20 )
        {
            player.sekkerInstantiate = false;
            player.isSekkerInstantiate = false;
            Destroy(gameObject);
        }

        if (!characterController.isGrounded)
        {
            speed.y += gravity * Time.fixedDeltaTime;
            seekani.SetBool("jump", true);
            seekani.SetBool("fallback", false);
        }
        if (stopMove)
        {
            timerStop += Time.deltaTime;
            if (timerStop >= 10)
            {

                stopMove = false;
                timerStop = 0;
           }
        }

        if (characterController.isGrounded && stopMove == false)
        {
            speed.x += acceleration * Time.fixedDeltaTime;
            float speedRatio = speed.x / maxXSpeed;
            acceleration = maxAcceleration * (1 - speedRatio);

            seekani.SetBool("jump", false);
            seekani.SetBool("fallback", true);


        }
        
       

        characterController.Move(new Vector2(speed.x, speed.y) * Time.deltaTime);
    }
    public void Jump()
    {

        speed.y = Mathf.Sqrt(jumpSpeed * -2.0f * gravity);
        
    }
    public void StopMove()
    {
        Debug.Log("chamou");
        speed.x = 0;
        speed.y = 0;
        stopMove = true;
        
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Vector3 pos = transform.position;
        Ground ground = hit.collider.GetComponent<Ground>();

    }

    void OnTriggerEnter(Collider other)
    {
     

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            seekani.SetTrigger("Capture");
        }
    }
}