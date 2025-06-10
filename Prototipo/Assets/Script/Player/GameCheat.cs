using UnityEngine;

public class GameCheat : MonoBehaviour
{
    Player player;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {


        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
   public void Cheat()
    {
        player.cheat = !player.cheat;

    }

}
