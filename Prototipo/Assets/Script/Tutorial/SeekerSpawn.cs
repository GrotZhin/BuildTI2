using UnityEngine;

public class SeekerSpawn : MonoBehaviour
{
    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            player.speed.x= 0f;
            player.maxXSpeed= 8f;
        }

    }
}
