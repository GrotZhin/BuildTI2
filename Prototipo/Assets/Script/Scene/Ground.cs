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
    public Transform laele;
    GameObject box;
    public float cameraHalfSize;


    bool didGenerateGround = false;

    public GameObject[] boxPrefab;
    public GameObject[] groundPrefab;
    public GameObject scoreCollider;
    public GameObject powerUp;
    float timer = 0;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        collider = GetComponent<BoxCollider>();
        groundHeight = laele.transform.position.y;
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
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                Destroy(gameObject);
                timer = 0;
                return;

            }

        }



        if (!didGenerateGround)
        {
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
        Vector3 posPredioAtual = transform.position;


        float maxX = screenRight + 1;


        float minX = screenRight + 0.5f;

        float actualX = UnityEngine.Random.Range(minX, maxX);


        pos.x = actualX + goCollider.size.x / 2;


        float h1 = player.jumpSpeed * player.maxHoldJumpTime;
        float t = player.jumpSpeed / -player.gravity;
        float h2 = player.jumpSpeed * t + (0.5f * (player.gravity * (t * t)));
        float maxJumpHeight = h1 + h2;
        float maxY = player.transform.position.y + maxJumpHeight * 8f;


        float minY = 7;
        float actualY = UnityEngine.Random.Range(minY, maxY);
        if (rdGround == 15)
        {
            pos.y = posPredioAtual.y + 5;
            pos.z = posPredioAtual.z;
            pos.x = actualX + goCollider.size.x / 2;
               go.transform.position = pos;
            return;

        }

        pos.y = actualY - goCollider.size.y / 2;
        if (pos.y > 8)
        {
            pos.y = 8;
        }
        if (pos.y < 6)
        {
            pos.y = 6;
        }
        pos.z = -0.56f;

        go.transform.position = pos;

        Ground goGround = go.GetComponent<Ground>();
        goGround.groundHeight = go.transform.position.y + (goCollider.size.y / 2);





    }
}
