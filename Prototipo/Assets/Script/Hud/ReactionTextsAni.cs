using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;


public class ReactionTextsAni : MonoBehaviour
{
    public RectTransform Text;
    public Tween Path;
    public CanvasGroup fade;

    public float move;
    public float Back;
    public float Enter;

    public float Timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Path = Text.DOAnchorPosX(Enter, 1);
        Path = Text.DOAnchorPosX(move, 1);
        fade.DOFade(1, 1f);

    }

    public async void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= 4.5)
        {
            Destroy(this.gameObject);
        }
        if (Timer >= 4)
        {
            await buack();
            Destroy(this.gameObject);
            Timer = 0;
        }
    }

    async Task buack()
    {
        await Text.DOAnchorPosX(Back, 1).AsyncWaitForCompletion();
    }
    



    


}
