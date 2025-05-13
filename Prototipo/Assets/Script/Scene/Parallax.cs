using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float depth = 1;
    public float limiteDireita;
    public float limiteEsquerda;

    public float cameraHalfSize;
    public float screenLeft;
    public float screenRight;

    Player player;
    // Start is called before the first frame update

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 pos = transform.position; 
        Vector2 playerPos = player.transform.position; 
        cameraHalfSize = Camera.main.orthographicSize * Camera.main.aspect;
        screenLeft =  Camera.main.transform.position.x - cameraHalfSize;
        screenRight =  Camera.main.transform.position.x + cameraHalfSize;

        float realSpeed = player.speed.x / depth;
        if (screenLeft >= pos.x + 4)
        {
            pos.x = playerPos.x + 19.2f;
            
        }
       
        pos.x -= realSpeed * Time.fixedDeltaTime;
     
        transform.position = pos;
    }
}
