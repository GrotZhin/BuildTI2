using Unity.Collections;
using UnityEngine;

public class GenerateObstacle : MonoBehaviour
{

    Ground go;
    Player player;

    public float cameraHalfSize;
    public float screenLeft;
    public float obstacleRight;
    public GameObject box;
    public GameObject[] boxPrefab;
    public GameObject ref1;
    public GameObject ref2;
    BoxCollider collider;
    float groundHeight;
    bool generateObstacle = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
      
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        go = GameObject.FindGameObjectWithTag("Ground").GetComponent<Ground>();

    }

    void Update()
    {
        groundHeight = transform.position.y + (collider.size.y / 2);
        cameraHalfSize = Camera.main.orthographicSize * Camera.main.aspect;
        screenLeft = Camera.main.transform.position.x - cameraHalfSize;
        var y = groundHeight;
        var x = Random.Range(ref1.transform.position.x, ref2.transform.position.x);

        Vector3 boxPos = new Vector3(x, y, -0.56f);
        var random = Random.Range(0, boxPrefab.Length);
        if (!generateObstacle)
        {
            generateObstacle = true;
        box = Instantiate(boxPrefab[random].gameObject, boxPos, Quaternion.identity);
        }


        

    }



}
