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
    [SerializeField] Transform BatteryAni;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Start()
    {
        BatteryAni.DOScale(1.5f, 0);
        BatteryAni.DORotate(new Vector3(0, 360, 360), 2f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        BatteryAni.DOMove(new Vector3(0, 0.5f, 0), 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
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
