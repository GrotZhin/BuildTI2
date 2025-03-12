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
            if (jumpTimer >= maxHoldJump)
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

            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;

            }
        }

        distance += speed.x * Time.fixedDeltaTime;

        if (isGrounded)
        {
           speed.x += acceleration * Time.fixedDeltaTime; 
           float speedRatio = speed.x / maxXSpeed;
           acceleration = maxAcceleration * ( 1 - speedRatio);
           if (speed.x >= maxXSpeed)
           {
                speed.x = maxXSpeed;
           }
        }

        transform.position = pos;
    }
}
