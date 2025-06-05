using UnityEngine;

public class Batery : MonoBehaviour
{
  
    public float cameraHalfSize;
    public float screenLeft;
    public float obstacleRight;
    BoxCollider boxCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraHalfSize = Camera.main.orthographicSize * Camera.main.aspect;


        screenLeft = Camera.main.transform.position.x - cameraHalfSize;


        obstacleRight = transform.position.x + (boxCollider.size.x / 2);

        if (screenLeft >= obstacleRight)
        {

            Destroy(gameObject);

            return;
        }

    }
}

