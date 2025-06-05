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
    public float cameraHalfSize;


    bool didGenerateGround = false;

    public GameObject[] boxPrefab;
    public GameObject[] groundPrefab;
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
        screenLeft = Camera.main.transform.position.x - cameraHalfSize;


        Vector3 pos = transform.position;

        groundRight = transform.position.x + (collider.size.x / 2);

        if (screenLeft >= groundRight)
        {
            
            Destroy(gameObject);
            return;
        }



        if (!didGenerateGround)
        {
            Debug.Log("Status" + didGenerateGround);
            if (groundRight <= screenRight)
            {
                didGenerateGround = true;

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


        float minY = 7;
        float actualY = UnityEngine.Random.Range(minY, maxY);


        pos.y = actualY - goCollider.size.y / 2;
        if (pos.y > 11)
        {
            pos.y = 11;
        }
        if (pos.y < 7)
        {
            pos.y = 7;
        }

        float maxX = screenRight + 1;


        float minX = screenRight + 0.5f;

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
            float halfWidth = goCollider.size.x / 2 - 1;
            float left = go.transform.position.x - halfWidth;
            float right = go.transform.position.x + halfWidth;
            float x = UnityEngine.Random.Range(left, right);

            Vector3 boxPos = new Vector3(x, y, -0.56f);
            float x2 = UnityEngine.Random.Range(left, right);
            Vector3 boxPos2 = new Vector3(x2, y, -0.56f);
            //box = Instantiate(boxPrefab[random].gameObject, boxPos, quaternion.identity);
           // box.transform.position = boxPos;
            //scoreBox.transform.position = boxPos;
            //powerUps.transform.position = boxPos2;

        }

    }
}
