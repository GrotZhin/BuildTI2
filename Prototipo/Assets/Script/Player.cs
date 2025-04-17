using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity;
    public Vector2 speed;
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 pos = transform.position;
        float groundDistance = Math.Abs(pos.y - groundHeight);
        if (isGrounded || groundDistance <= jumpGroundTreshhold)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                speed.y = jumpSpeed;
                isHoldingJump = true;
                jumpTimer = 0f;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }
    }
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

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

            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if (hit2D.collider != null)
            {
                Ground ground = hit2D.collider.GetComponent<Ground>();
                if (ground != null)
                {
                    groundHeight = ground.groundHeight;
                    pos.y = groundHeight;
                    speed.y = 0;
                    isGrounded = true;

                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);



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

        }


            Vector2 obstOrigin = new Vector2(pos.x,pos.y); 
            RaycastHit2D obstHitX = Physics2D.Raycast(obstOrigin,Vector2.right, speed.x * Time.fixedDeltaTime); 
            if (obstHitX.collider !=null)
            {
                Obstacle obstacle = obstHitX.collider.GetComponent<Obstacle>();
                if (obstacle != null)
                {
                    HitObstacle(obstacle);
                }
            }

             RaycastHit2D obstHitY = Physics2D.Raycast(obstOrigin,Vector2.up, speed.y * Time.fixedDeltaTime); 
            if (obstHitY.collider !=null)
            {
                Obstacle obstacle = obstHitY.collider.GetComponent<Obstacle>();
                if (obstacle != null)
                {
                    HitObstacle(obstacle);
                }
            }

        transform.position = pos;
    }

    void HitObstacle(Obstacle obstacle)
    {
        obstacle.boxCollider2D.enabled = false;
        speed.x *= 0.5f;
    }
}
