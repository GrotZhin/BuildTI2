using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Player player;
    public float groundHeight;
    public float groundRight;
    public float screenRight;
    public float screenLeft;
    BoxCollider2D collider2D;
    public float cameraHalfSize;
    


    bool didGenerateGround = false;

    public  GameObject[] boxPrefab;
    public GameObject scoreCollider;
    public GameObject powerUp;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        collider2D = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (collider2D.size.y / 2);
     
       
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    private void FixedUpdate()
    { 
        cameraHalfSize = Camera.main.orthographicSize * Camera.main.aspect;

        screenRight = Camera.main.transform.position.x * 2;
        screenLeft =  Camera.main.transform.position.x - cameraHalfSize;

        Vector2 pos = transform.position;
       
        if (screenLeft >= groundRight)
        {
            Destroy(gameObject);
            return;
        }

        groundRight = transform.position.x + (collider2D.size.x / 2);

        if (!didGenerateGround)
        {
            if (groundRight < screenRight)
            {
                didGenerateGround = true;
                GenerateGround();
            }
        }
        transform.position = pos;
    }


    void GenerateGround()
    {
        GameObject go = Instantiate(gameObject);
        BoxCollider2D goCollider = GetComponent<BoxCollider2D>();
        Vector2 pos;

        float h1 = player.jumpSpeed * player.maxHoldJumpTime;
        float t = player.jumpSpeed / -player.gravity;
        float h2 = player.jumpSpeed * t + (0.5f * (player.gravity * (t * t)));
        float maxJumpHeight = h1 + h2;
        float maxY = maxJumpHeight * 0.7f;
        maxY += groundHeight;

        float minY = 1;
        float actualY = UnityEngine.Random.Range(minY, maxY);


        pos.y = actualY - goCollider.size.y / 2;
        if (pos.y > 3)
        {
            pos.y = 3;
        }
        if (pos.y < -15)
        {
            pos.y = -15;
        }



        float t1 = t + player.maxHoldJumpTime;
        float t2 = MathF.Sqrt(2.0f * (maxY - actualY) / -player.gravity);
        float totalTime = t1 + t2;
        float maxX = totalTime * player.speed.x;
        maxX *= 0.7f;
        maxX += groundRight;
        float minX = screenRight;
        float actualX = UnityEngine.Random.Range(minX, maxX);


        pos.x = actualX + goCollider.size.x / 2;

        go.transform.position = pos;

        Ground goGround = go.GetComponent<Ground>();
        goGround.groundHeight = go.transform.position.y + (goCollider.size.y / 2);


        int obstacleNum = UnityEngine.Random.Range(0, 3);
        
        
        for (int i = 0; i < obstacleNum; i++)
        {
            var random = UnityEngine.Random.Range(0, boxPrefab.Length);
            GameObject box = Instantiate(boxPrefab[random].gameObject);
            GameObject scoreBox = Instantiate(scoreCollider.gameObject);
            GameObject powerUps = Instantiate(powerUp.gameObject);

            float y = goGround.groundHeight + 1;
            float halfWidth = goCollider.size.x /2 - 1;
            float left = go.transform.position.x - halfWidth;
            float right = go.transform.position.x + halfWidth;
            float x = UnityEngine.Random.Range(left,right); 
            
            Vector2 boxPos = new Vector2(x,y);
            Vector2 boxPos2 = new Vector2(x-5,y - 0.5f);
            box.transform.position = boxPos;
            scoreBox.transform.position = boxPos;
            powerUps.transform.position = boxPos2;
            
        }
       
    }
}
