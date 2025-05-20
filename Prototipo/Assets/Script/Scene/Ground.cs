using System;
using Unity.Mathematics;
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
    int count =0;
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
           
            Destroy(gameObject);
            count--;
            return;
        }




        if (groundRight < screenRight && count <=3)
        {

            Debug.Log("Gerar" + didGenerateGround);
            GenerateGround();
            count++;
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

        float maxX =0; 
        maxX += screenRight + 10;
        maxX *= 0.7f;

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
          

            //GameObject scoreBox = Instantiate(scoreCollider.gameObject);
            //GameObject powerUps = Instantiate(powerUp.gameObject);

            float y = goGround.groundHeight;
            float halfWidth = goCollider.size.x /2 - 1;
            float left = go.transform.position.x - halfWidth;
            float right = go.transform.position.x + halfWidth;
            float x = UnityEngine.Random.Range(left,right); 
            
            Vector2 boxPos = new Vector2(x,y);
            Vector2 boxPos2 = new Vector2(x-5,y - 0.5f);
            
            box = Instantiate(boxPrefab[random].gameObject, boxPos, quaternion.identity,this.gameObject.transform);
            

            box.transform.position = boxPos;
            //scoreBox.transform.position = boxPos;
            //powerUps.transform.position = boxPos2;
            
        }
       
    }
}
