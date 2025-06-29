using UnityEngine;

public class GameCheat : MonoBehaviour
{
    Player player;
    GameObject[] walls;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {


        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {
        walls = GameObject.FindGameObjectsWithTag("wall");

        for (int i = 0; i < walls.Length; i++)
        {
            if (player.cheat == true)
            {
                walls[i].SetActive(false);
            }
            else if (player.cheat == false && walls[i].activeSelf == false)
            {
                walls[i].SetActive(true);
            }

        }
    }

    // Update is called once per frame
    public void Cheat()
    {
        player.cheat = !player.cheat;


    }

}
