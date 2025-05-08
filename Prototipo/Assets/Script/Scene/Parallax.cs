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

        cameraHalfSize = Camera.main.orthographicSize * Camera.main.aspect;
        screenLeft =  Camera.main.transform.position.x - cameraHalfSize;
        screenRight =  Camera.main.transform.position.x + cameraHalfSize;

        float realSpeed = player.speed.x / depth;
       
        Vector2 pos = transform.position; 
        pos.x -= realSpeed * Time.fixedDeltaTime;
     
        transform.position = pos;
    }
}
