using System;
using Unity.Mathematics;
using UnityEngine;

public class Tricks : MonoBehaviour
{
    public int trickPoint;
    public GameObject[] arrows;
    GameObject arrow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    void Update()
    {
        if (arrow)
        {
            
        }
    }

    // Update is called once per frame

    void Trick()
    {
        Vector3 pos = transform.position;
        Vector3 arrowPos = new Vector3(pos.x + 5, pos.y, pos.z);
        int direction = UnityEngine.Random.Range(0, 3);
        if (direction == 0)
        {
            arrow = Instantiate(arrows[0], arrowPos, quaternion.identity);
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                trickPoint += 20;
            }
        }
        else if (direction == 1)
        {
            arrow = Instantiate(arrows[1], arrowPos, quaternion.identity);
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                trickPoint += 20;
            }
        }
        else if (direction == 2)
        {
            arrow = Instantiate(arrows[2], arrowPos, quaternion.identity);
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                trickPoint += 20;
            }
        }
        else if (direction == 3)
        {
            arrow = Instantiate(arrows[3], arrowPos, quaternion.identity);
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                trickPoint += 20;
            }
        }

    }


}
