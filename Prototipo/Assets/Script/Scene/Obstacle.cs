using UnityEngine;

public class Obstacle : MonoBehaviour
{

    Ground go;
    Player player;
    public BoxCollider boxCollider;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        boxCollider = GetComponent<BoxCollider>();
    }

   


}
