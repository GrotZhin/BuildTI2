using Unity.VisualScripting;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    Player player;
    public bool existe;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        Vector2 playerPos = player.transform.position;


        if (pos.x < playerPos.x)
        {
            Destroy(gameObject);
        }

    }
    public void DestroyArrow()
    { 
        Destroy(gameObject);
    }
}
