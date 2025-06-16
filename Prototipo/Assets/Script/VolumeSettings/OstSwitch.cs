using RWM;
using UnityEngine;

public class OstSwitch : MonoBehaviour
{
    public AudioSource Ost1;
    public AudioSource Ost2;



    public float timer1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SongControl.FadeAudio(Ost2, 0f, 0f));
        StartCoroutine(SongControl.FadeAudio(Ost1, 1f, 0f));

    }

    // Update is called once per frame
    void Update()
    {

        timer1 += Time.deltaTime;


        if (timer1 >= 118)
        {
            if (Ost2.volume <= 0)
            {
                loop1();
            }
            else
            if (Ost1.volume <= 0)
            {
                loop2();
            }
            timer1 = 0;
        }

        



    }

    public void loop1()
    {
        StartCoroutine(SongControl.FadeAudio(Ost2, 1f, 10f));
        StartCoroutine(SongControl.FadeAudio(Ost1, 0f, 6f));
    }
    public void loop2()
    {
        StartCoroutine(SongControl.FadeAudio(Ost1, 1f, 6f));
        StartCoroutine(SongControl.FadeAudio(Ost2, 0f, 10f));

    }

}
