using UnityEngine;
using DG.Tweening;
using RWM;

public class ReactionTexts : MonoBehaviour
{
    public GameObject com;
    public Transform Daddy;
    int rand;
    public float Timer;
    public float counter;

    public void Update()
    {
        rand = Random.Range(2,5);

        Timer += Time.deltaTime;

        if (Timer >= 0.9f)
        {    
            Instantiate(com, transform.position, Quaternion.identity, Daddy);
            soundManager.PlaySound(SoundType.Plamn);
            Timer = 0;
            counter++;
        }
        if (counter >= rand)
        {
            Timer = 0;
        }



    }
    
    
        
    
}
