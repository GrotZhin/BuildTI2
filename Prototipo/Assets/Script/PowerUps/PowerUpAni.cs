using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class PowerUpAni : MonoBehaviour
{
    public Image batery;

    [SerializeField] Transform BatteryAni;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Start()
    {
        BatteryAni.DOScale(0.3f, 0);
        BatteryAni.DORotate(new Vector3(0, 360, 360), 2f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        BatteryAni.DOMove(BatteryAni.position, 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}