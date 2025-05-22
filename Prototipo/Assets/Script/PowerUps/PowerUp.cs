using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class PowerUp : MonoBehaviour
{
    Player player;
    public BoxCollider boxCollider;
    public Image batery;

    public float cameraHalfSize;
    public float screenLeft;
    public float obstacleRight;
    
    private void Awake()
    {

        batery.fillAmount = 0;
        boxCollider = GetComponent<BoxCollider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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


    void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            batery.fillAmount = 1;
        }
    }
    public void Boost()
    {
        player.speed.x *= 0.5f;
        batery.fillAmount = 0;

    }
}
