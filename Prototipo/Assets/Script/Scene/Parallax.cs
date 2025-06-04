using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float depth = 1;
    

    public float cameraHalfSize;
    public float screenLeft;
    public float screenRight;
    public Sprite[] sprite;
    int i = 0;

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
        if (screenLeft >= sprite[i].border.z)
        {
            pos.x = sprite[i+1].border.x;
            i++;
            if (i == 2)
            {
                i = 0;
            }   
        }
       
        pos.x -= realSpeed * Time.fixedDeltaTime;
     
        transform.position = pos;
    }
}
