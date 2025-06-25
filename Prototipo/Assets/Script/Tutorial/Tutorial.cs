using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class Tutorial : MonoBehaviour
{
    Player player;
    Sekker sekker;

    public GameObject TutoMng;
    public GameObject T1;
    public GameObject T2;
    public GameObject T3;
    public GameObject T4;
    public GameObject T5;

    public CanvasGroup tu;

    public bool Tupause = false;

    public float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TutoMng.SetActive(false);
    }
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        T1.SetActive(true);
        T2.SetActive(false);
        T3.SetActive(false);
        T4.SetActive(false);
        T5.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {






    }

    public async void tsugi()
    {
        await tu.DOFade(0, 0.5f).SetUpdate(true).AsyncWaitForCompletion();
        tu.DOFade(0, 1f).SetUpdate(true);
        TutoMng.SetActive(false);
        Time.timeScale = 1;
        Tupause = false;
        T1.SetActive(false);
        T2.SetActive(true);


    }

    public async void tsugi2()
    {
        await tu.DOFade(0, 0.5f).SetUpdate(true).AsyncWaitForCompletion();
        tu.DOFade(0, 1f).SetUpdate(true);
        TutoMng.SetActive(false);
        Time.timeScale = 1;
        Tupause = false;
        T2.SetActive(false);
        T3.SetActive(true);


    }

    public async void tsugi3()
    {
        await tu.DOFade(0, 0.5f).SetUpdate(true).AsyncWaitForCompletion();
        tu.DOFade(0, 1f).SetUpdate(true);
        TutoMng.SetActive(false);
        Time.timeScale = 1;
        Tupause = false;
        T3.SetActive(false);
        T4.SetActive(true);


    }

    public async void tsugi4()
    {
        await tu.DOFade(0, 0.5f).SetUpdate(true).AsyncWaitForCompletion();
        tu.DOFade(0, 1f).SetUpdate(true);
        TutoMng.SetActive(false);
        Time.timeScale = 1;
        Tupause = false;
        T4.SetActive(false);
        T5.SetActive(true);


    }
    
    public void tsugi5()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
