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
    bool onScreen = false;
    float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
    }
    void Update()
    {
        if (player.isGrind)
        {
            if (!onScreen)
            {
                timer += Time.deltaTime;
                if (timer >= 1.5f)
                {
                    direction = Trick();
                    onScreen = true;
                    timer = 0;
                }
            }
        }
    }

    // Update is called once per frame
    public void TrickCerto(int index)
    {
        if (arrows[direction].activeSelf == true)
        {
            if (direction == index)
            {
                player.score += 20;
                arrows[direction].SetActive(false);
                onScreen = false;
            }



            else
            {
                arrows[direction].SetActive(false);
                onScreen = false;
                return;
            }
        }
    }
    public int Trick()
    {

        int direction = UnityEngine.Random.Range(0, 3);
        arrows[direction].SetActive(true);
        Debug.Log("chamou");
        return direction;
    }

}
