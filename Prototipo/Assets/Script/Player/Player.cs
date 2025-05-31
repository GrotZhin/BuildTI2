using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private CharacterController characterController;
    public float gravity;
    public Vector2 speed;
    public int score = 0;
    public float maxXSpeed = 100;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    public float distance = 0;
    public int trickPoint;

    public float jumpSpeed = 20;
    public float groundHeight = 10;

    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.4f;
    public float maxHoldJump = 0.4f;
    public float jumpTimer = 0.0f;
    public float jumpGroundTreshhold = 1;

    public LayerMask groundLayerMask;
    public LayerMask obstacleLayerMask;
    public LayerMask scoreLayerMask;
    public LayerMask powerUpLayerMask;
    float groundDistance;

    bool slider = false;
    float sliderTimer;
    public GameObject sekker;


    public bool isDead = false;
    public bool cheat = false;
    // Start is called before the first frame update
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        Vector2 pos = transform.position;
        Vector2 sekkerPos = new Vector2(pos.x - 5, pos.y);

        Instantiate(sekker, sekkerPos, Quaternion.identity);

    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 pos = transform.position;

        groundDistance = Mathf.Abs(pos.y - groundHeight);

        if (slider)
        {
            sliderTimer += Time.deltaTime;
            if (sliderTimer >= 0.8f)
            {
                characterController.height = 2.0f;
                slider = false;
                sliderTimer = 0;

            }

        }

        if (characterController.isGrounded || groundDistance <= jumpGroundTreshhold)
        {


            // Teclado
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();

            }
        }
        if (!slider)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Slide();
            }
        }
        // Teclado
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ReleaseJump();
        }
    }
    public void Jump()
    {

        speed.y = Mathf.Sqrt(jumpSpeed * -2.0f * gravity);
    }
    public void Slide()
    {
        slider = true;
        characterController.height = 0.7f;

    }


    public void ReleaseJump()
    {
        isHoldingJump = false;
    }

    private void FixedUpdate()
    {

        Vector2 pos = transform.position;

        if (isDead)
        {
            return;
        }

        if (pos.y <= 3 && !cheat)

        {
            isDead = true;
            speed.x = 0;
        }
        else if (pos.y <= 3 && cheat)
        {
            speed.y = groundHeight + 20;
            speed.x = 5;
          
            


        }
        if (isHoldingJump)
        {
            jumpTimer += Time.fixedDeltaTime;
            if (jumpTimer >= maxHoldJumpTime)
            {
                isHoldingJump = false;

            }
        }
        if (!characterController.isGrounded)
        {
            pos.y += speed.y * Time.fixedDeltaTime;
            if (!isHoldingJump)
            {
                speed.y += gravity * Time.fixedDeltaTime;
            }


        }

        distance += speed.x * Time.fixedDeltaTime;

        if (characterController.isGrounded)
        {
            Debug.Log(characterController.isGrounded);
            speed.x += acceleration * Time.fixedDeltaTime;
            float speedRatio = speed.x / maxXSpeed;
            acceleration = maxAcceleration * (1 - speedRatio);
            maxHoldJumpTime = maxHoldJump * speedRatio;
            if (speed.x >= maxXSpeed)
            {
                speed.x = maxXSpeed;
            }

        }

        characterController.Move(new Vector2(speed.x, speed.y) * Time.deltaTime);
        
    }

    public void HitObstacle(Obstacle obstacle)
    {
        Destroy(obstacle.gameObject);
        speed.x *= 0.8f;
    }
    void HitPowerUp(PowerUp powerUp)
    {
        Destroy(powerUp.gameObject);
        speed.x += speed.x * 0.10f;
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
            Debug.Log("atingiuX");
            speed.x = 5;
        }


    }

    void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            HitObstacle(obstacle);
        }

        if (other.gameObject.CompareTag("Score"))
        {
            score += 10;
        }
        PowerUp powerUp = other.gameObject.GetComponent<PowerUp>();
        if (powerUp != null)
        {
            HitPowerUp(powerUp);
        }
    }
    public void Cheat()
    {
        cheat = true;

    }


}
