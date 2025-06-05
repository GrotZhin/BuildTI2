using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DOtween : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public RectTransform Tester;
    public RawImage Fadein;


    void Start()
    {
        
        Tester.DOScale(0.9f,1).SetEase(Ease.OutCubic).SetLoops(-1,LoopType.Yoyo);
        Fadein.DOFade(0,3);
        
     }

    // Update is called once per frame
    void Update()
    {
        
    }
}
