using System;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class Tricks : MonoBehaviour
{
    public int trickPoint;
    Player player;
    public GameObject[] arrows;
    GameObject arrow;
    Arrows arrowsRef;
    int direction;
    float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       player = GetComponent<Player>();
    }
    void Update()
    {
        if (player.grind)
        {
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                direction = Trick();
                timer = 0;
            }
        }
        arrowsRef = GameObject.FindGameObjectWithTag("arrow").GetComponent<Arrows>();

        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && arrowsRef.transform.position.x > transform.position.x)
            {
                trickPoint += 20;
                arrowsRef.DestroyArrow();
            }
        }
        if (direction == 1)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && arrowsRef.transform.position.x > transform.position.x)
            {
                trickPoint += 20;
                arrowsRef.DestroyArrow();
            }
        }
        if (direction == 2)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && arrowsRef.transform.position.x > transform.position.x)
            {
                trickPoint += 20;
                arrowsRef.DestroyArrow();
            }
        }
        if (direction == 3)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && arrowsRef.transform.position.x > transform.position.x)
            {
                trickPoint += 20;
                
            }
        }

    }

    // Update is called once per frame

    int Trick()
    {
        Vector3 pos = transform.position;
        Vector3 arrowPos = new Vector3(pos.x + 5, pos.y, pos.z);
        int direction = UnityEngine.Random.Range(0, 3);

        arrow = Instantiate(arrows[direction], arrowPos, quaternion.identity);
        return direction;
    }
}
