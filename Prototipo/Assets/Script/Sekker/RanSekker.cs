using UnityEngine;

public class RanSekker : MonoBehaviour
{
    public GameObject Stalker;
    public Animator seekani;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seekani = Stalker.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            //HitObstacle(obstacle);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Stalker.SetActive(true);
            seekani.SetTrigger("Capture");
        
        }
    }
}
