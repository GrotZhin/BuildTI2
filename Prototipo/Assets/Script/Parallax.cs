using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float depth = 1;
    public float limiteDireita;
    public float limiteEsquerda;
    public new GameObject camera;

    Player player;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float realSpeed = player.speed.x / depth;
        Vector2 pos = transform.position;

        pos.x -= realSpeed * Time.fixedDeltaTime;

        if (pos.x <= limiteEsquerda )
        pos.x = limiteDireita;
            
        
        transform.position = pos;
    }
}
