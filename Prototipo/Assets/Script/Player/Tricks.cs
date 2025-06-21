using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using RWM;

public class Tricks : MonoBehaviour
{
    public int trickPoint;
    Player player;
    public GameObject[] arrows;
    GameObject arrow;

    public GameObject Ranna;
    public Animator Ranani;

    public RectTransform ArrowAni;
    public CanvasGroup ArrowG;
    public GameObject arwMan;
    public GameObject comJ;
    public GameObject comG;
    public Transform ReDad;
    [SerializeField] float ReTweenDur;
    [SerializeField] float TweenDur;
    Arrows arrowsRef;
    int direction;
    bool onScreen = false;
    float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
        Ranani = Ranna.GetComponent<Animator>();
    }
    void Update()
    {
        if (player.isGrind)
        {
            if (!onScreen)
            {
                timer += Time.deltaTime;
                if (timer >= 1.0f)
                {
                    ArrowAniintro();
                    direction = Trick();
                    onScreen = true;
                    timer = 0;
                }
            }
        }

        if (!onScreen)
        {
            ArrowG.DOFade(0, TweenDur).SetUpdate(true);

        }

        if (!player.isGrind)
        {
            arwMan.SetActive(false);
        }
        else
        {
            arwMan.SetActive(true);
         }
    }

    // Update is called once per frame
    public async Task TrickCerto(int index)
    {
        if (arrows[direction].activeSelf == true)
        {
            if (direction == index)
            {
                int Ran = Random.Range(0, 5);
                if (Ran <= 5)
                {
                    Instantiate(comJ, ReDad.position, Quaternion.identity, ReDad);
                }
                else
                {
                    Instantiate(comG, ReDad.position, Quaternion.identity, ReDad);
                }
                soundManager.PlaySound(SoundType.Plamn);
                player.score += 20;
                Ranani.SetInteger("GrindTrickIndex", Random.Range(0, 5));
                Ranani.SetTrigger("grindtrig");
                await ArrowAnioutro();
                arrows[direction].SetActive(false);
                onScreen = false;
                ArrowAni.DOScale(1, ReTweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
            }



            else
            {
                soundManager.PlaySound(SoundType.Hit);
                await ArrowAnioutroWng();
                arrows[direction].SetActive(false);
                onScreen = false;
                return;
            }
        }
    }
    public int Trick()
    {

        int direction = UnityEngine.Random.Range(0, 3);
        arrows[direction].SetActive(true);
        Debug.Log("chamou");
        return direction;
    }

    public void ArrowAniintro()
    {
        ArrowG.DOFade(1, TweenDur).SetUpdate(true);
        ArrowAni.DOScale(1.8f, ReTweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        ArrowAni.DOScale(0.7f, ReTweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        ArrowAni.DOScale(1, ReTweenDur).SetEase(Ease.OutFlash).SetUpdate(true);


    }

    public async Task ArrowAnioutro()
    {
        await ArrowG.DOFade(0f, 0.5f).SetUpdate(true).AsyncWaitForCompletion();

    }

    public async Task ArrowAnioutroWng()
    {
        await ArrowAni.DOShakePosition(1f, 10, 40, 10).AsyncWaitForCompletion();
        await ArrowG.DOFade(0f, 0.5f).SetUpdate(true).AsyncWaitForCompletion();
        
        
    }

}
