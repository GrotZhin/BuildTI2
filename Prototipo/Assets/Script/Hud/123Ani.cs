using UnityEngine;
using DG.Tweening;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class onetwotreeani : MonoBehaviour
{
    public RectTransform stani;
    public CanvasGroup staimg;
    public float fadet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stani.DOScale(1.4f, 0.5f).SetLoops(-1, LoopType.Restart).SetEase(Ease.InBounce);
         stani.DORotate(new Vector2(90,0),0.3f).SetLoops(3, LoopType.Restart).SetEase(Ease.InBounce);    
    }

    // Update is called once per frame
    void Update()
    {
        fadet -= Time.deltaTime;
        if (fadet <= 0)
        {
            staimg.DOFade(0, 0.6f);
        }
        
    }
}
