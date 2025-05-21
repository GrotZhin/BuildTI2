using UnityEngine;

public class Obstacle : MonoBehaviour
{

    Ground go;
    Player player;
    public BoxCollider boxCollider;
    public float cameraHalfSize;
    public float screenLeft;
    public float obstacleRight;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        go = GameObject.FindGameObjectWithTag("Ground").GetComponent<Ground>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        cameraHalfSize = Camera.main.orthographicSize * Camera.main.aspect;

       
        screenLeft =  Camera.main.transform.position.x - cameraHalfSize;

      
        obstacleRight = transform.position.x + (boxCollider.size.x / 2);

        if (screenLeft >= obstacleRight)
        {
           
            Destroy(gameObject);
            
            return;
        }

    }



}
