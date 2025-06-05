using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public BoxCollider boxCollider;
    public float screenLeft;
    public float cameraHalfSize;
    public float obstacleRight;

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
