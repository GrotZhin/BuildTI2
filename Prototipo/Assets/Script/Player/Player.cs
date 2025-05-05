using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    
    public float gravity;
    public Vector2 speed;
    public int score = 0;
    public float maxXSpeed = 100;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    public float distance = 0;

    public float jumpSpeed = 20;
    public float groundHeight = 10;
    public bool isGrounded = false;
    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.4f;
    public float maxHoldJump = 0.4f;
    public float jumpTimer = 0.0f;
    public float jumpGroundTreshhold = 1;

    public LayerMask groundLayerMask;
    public LayerMask obstacleLayerMask;
    public LayerMask scoreLayerMask;
    public LayerMask powerUpLayerMask;


    public bool isDead = false;
    // Start is called before the first frame update
    

    // Update is called once per frame
    private void Update()
    {
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);
        

        if (isGrounded || groundDistance <= jumpGroundTreshhold)
        {
      
            // Teclado
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
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
        isGrounded = false;
        speed.y = jumpSpeed;

        jumpTimer = 0f;
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

        if (pos.y <= -20)

        {
            isDead = true;
            speed.x = 0;
        }
        if (isHoldingJump)
        {
            jumpTimer += Time.fixedDeltaTime;
            if (jumpTimer >= maxHoldJumpTime)
            {
                isHoldingJump = false;

            }
        }
        if (!isGrounded)
        {
            pos.y += speed.y * Time.fixedDeltaTime;
            if (!isHoldingJump)
            {
                speed.y += gravity * Time.fixedDeltaTime;
            }
            Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = speed.y * Time.fixedDeltaTime;

            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, groundLayerMask);
            if (hit2D.collider != null)
            {
                Debug.Log("colidiu");
                Ground ground = hit2D.collider.GetComponent<Ground>();
                if (ground != null)
                {
                    if (pos.y >= ground.groundHeight)
                    {
                        groundHeight = ground.groundHeight;
                        pos.y = groundHeight;
                        speed.y = 0;
                        isGrounded = true;
                    }
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);


            Vector2 wallOrigin = new Vector2(pos.x, pos.y);
            RaycastHit2D wallHit = Physics2D.Raycast(wallOrigin, Vector2.right, speed.x * Time.fixedDeltaTime);
            if (wallHit.collider != null)
            {
                Ground ground = wallHit.collider.GetComponent<Ground>();
                if (ground != null)
                {
                    if (pos.y < ground.groundHeight)
                    {
                        speed.x = 0;

                    }
                }
            }


        }

        distance += speed.x * Time.fixedDeltaTime;

        if (isGrounded)
        {
            speed.x += acceleration * Time.fixedDeltaTime;
            float speedRatio = speed.x / maxXSpeed;
            acceleration = maxAcceleration * (1 - speedRatio);
            maxHoldJumpTime = maxHoldJump * speedRatio;
            if (speed.x >= maxXSpeed)
            {
                speed.x = maxXSpeed;
            }

            Vector2 rayOrigin = new Vector2(pos.x - 0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = speed.y * Time.fixedDeltaTime;

            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if (hit2D.collider == null)
            {
                isGrounded = false;
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);


            Vector2 obstOrigin = new Vector2(pos.x, pos.y);
            RaycastHit2D obstHitX = Physics2D.Raycast(obstOrigin, Vector2.right, speed.x * Time.fixedDeltaTime, obstacleLayerMask);
            if (obstHitX.collider != null)
            {
                Obstacle obstacle = obstHitX.collider.GetComponent<Obstacle>();
                if (obstacle != null)
                {
                    HitObstacle(obstacle);
                }
            }

            RaycastHit2D obstHitY = Physics2D.Raycast(obstOrigin, Vector2.up, speed.y * Time.fixedDeltaTime, obstacleLayerMask);
            if (obstHitY.collider != null)
            {
                Obstacle obstacle = obstHitY.collider.GetComponent<Obstacle>();
                if (obstacle != null)
                {
                    HitObstacle(obstacle);
                }
            }

        }


        Vector2 powerUp = new Vector2(pos.x, pos.y);
        RaycastHit2D powerUpX = Physics2D.Raycast(powerUp, Vector2.right, speed.x * Time.fixedDeltaTime, powerUpLayerMask);
        if (powerUpX.collider != null)
        {
            PowerUp scores = powerUpX.collider.gameObject.GetComponent<PowerUp>();
            if (scores != null)
            {
                HitPowerUp(scores);
            }
        }

        RaycastHit2D powerUpY = Physics2D.Raycast(powerUp, Vector2.up, speed.y * Time.fixedDeltaTime, powerUpLayerMask);
        if (powerUpY.collider != null)
        {
            PowerUp scores = powerUpY.collider.gameObject.GetComponent<PowerUp>();

            if (scores != null)
            {
                HitPowerUp(scores);
            }
        }

        Vector2 obstScore = new Vector2(pos.x, pos.y);
        RaycastHit2D obstScoreX = Physics2D.Raycast(obstScore, Vector2.right, speed.x * Time.fixedDeltaTime, scoreLayerMask);
        if (obstScoreX.collider != null)
        {
            Score scores = obstScoreX.collider.gameObject.GetComponent<Score>();
            if (scores != null)
            {
                HitScore(scores);
            }
        }

        RaycastHit2D obstScoreY = Physics2D.Raycast(obstScore, Vector2.up, speed.y * Time.fixedDeltaTime, scoreLayerMask);
        if (obstScoreY.collider != null)
        {
            Score scores = obstScoreY.collider.gameObject.GetComponent<Score>();

            if (scores != null)
            {
                HitScore(scores);
            }
        }
        


        pos.x += speed.x * Time.fixedDeltaTime;
       

        transform.position = pos;
    }

    void HitObstacle(Obstacle obstacle)
    {
        obstacle.boxCollider2D.enabled = false;
        speed.x *= 0.8f;
    }
    void HitScore(Score obstacle)
    {
        obstacle.boxCollider2D.enabled = false;
        score += 10;

    }
    void HitPowerUp(PowerUp powerUp)
    {
        Destroy(powerUp.gameObject);
        speed.x += speed.x * 0.10f;
    }
}
