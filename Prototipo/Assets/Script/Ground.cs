using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Player player;
    public float groundHeight;
    public float groundRight;
    public float screenRight;
    BoxCollider2D collider2D;

    bool didGenerateGround = false;

    public  GameObject[] boxPrefab;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        collider2D = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (collider2D.size.y / 2);
        screenRight = Camera.main.transform.position.x * 2;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= player.speed.x * Time.fixedDeltaTime;
        if (groundRight < 0)
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
            

            float y = goGround.groundHeight + 1;
            float halfWidth = goCollider.size.x /2 - 1;
            float left = go.transform.position.x - halfWidth;
            float right = go.transform.position.x + halfWidth;
            float x = UnityEngine.Random.Range(left,right); 
            
            Vector2 boxPos = new Vector2(x,y);
            box.transform.position = boxPos;
        }
    }
}
