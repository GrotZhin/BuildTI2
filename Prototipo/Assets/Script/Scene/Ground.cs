using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Player player;
    public float groundHeight;
    public float groundRight;
    public float screenRight;
    public float screenLeft;
    BoxCollider collider;
     GameObject box;
    public float cameraHalfSize;
    


    bool didGenerateGround = false;

    public  GameObject[] boxPrefab;
    public  GameObject[] groundPrefab;
    public GameObject scoreCollider;
    public GameObject powerUp;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        collider = GetComponent<BoxCollider>();
        groundHeight = transform.position.y + (collider.size.y / 2);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    private void FixedUpdate()
    { 
        cameraHalfSize = Camera.main.orthographicSize * Camera.main.aspect;

        screenRight = Camera.main.transform.position.x * 2;
        screenLeft =  Camera.main.transform.position.x - cameraHalfSize;

      
        Vector3 pos = transform.position;

        groundRight = transform.position.x + (collider.size.x / 2);

        if (screenLeft >= groundRight)
        {
            // Destroy(box.gameObject);
            Destroy(gameObject);
            return;
        }

       

        if (!didGenerateGround)
        {
            if (groundRight < screenRight)
            {
                didGenerateGround = true;
                Debug.Log("Gerar");
                GenerateGround();
            }
        }
        transform.position = pos;
    }


    void GenerateGround()
    {
        int rdGround = UnityEngine.Random.Range(0, groundPrefab.Length);

        GameObject go = Instantiate(groundPrefab[rdGround]);
        BoxCollider goCollider = GetComponent<BoxCollider>();
        Vector3 pos;

        float h1 = player.jumpSpeed * player.maxHoldJumpTime;
        float t = player.jumpSpeed / -player.gravity;
        float h2 = player.jumpSpeed * t + (0.5f * (player.gravity * (t * t)));
        float maxJumpHeight = h1 + h2;
        float maxY = player.transform.position.y + maxJumpHeight * 0.7f;
        maxY += groundHeight;

        float minY = 7;
        float actualY = UnityEngine.Random.Range(minY, maxY);


        pos.y = actualY - goCollider.size.y / 2;
        if (pos.y > 11)
        {
            pos.y = 11;
        }
        if (pos.y < 7)
        {
            pos.y =7 ;
        }



        float t1 = t + player.maxHoldJumpTime;
        float t2 = MathF.Sqrt(2.0f * (maxY - actualY) / -player.gravity);
        float totalTime = t1 + t2;
        float maxX = totalTime * player.speed.x;
        maxX *= 0.7f;
        maxX += groundRight;
        float minX = screenRight +5;
        float actualX = UnityEngine.Random.Range(minX, maxX);


        pos.x = actualX + goCollider.size.x / 2;
        pos.z = -0.56f;
        go.transform.position = pos;

        Ground goGround = go.GetComponent<Ground>();
        goGround.groundHeight = go.transform.position.y + (goCollider.size.y / 2);


        int obstacleNum = UnityEngine.Random.Range(0, 3);
        
        
        for (int i = 0; i < obstacleNum; i++)
        {
            var random = UnityEngine.Random.Range(0, boxPrefab.Length);
             box = Instantiate(boxPrefab[random].gameObject);
            //GameObject scoreBox = Instantiate(scoreCollider.gameObject);
            //GameObject powerUps = Instantiate(powerUp.gameObject);

            float y = goGround.groundHeight;
            float halfWidth = goCollider.size.x /2 - 1;
            float left = go.transform.position.x - halfWidth;
            float right = go.transform.position.x + halfWidth;
            float x = UnityEngine.Random.Range(left,right); 
            
            Vector2 boxPos = new Vector2(x,y);
            Vector2 boxPos2 = new Vector2(x-5,y - 0.5f);
            box.transform.position = boxPos;
            //scoreBox.transform.position = boxPos;
            //powerUps.transform.position = boxPos2;
            
        }
       
    }
}
