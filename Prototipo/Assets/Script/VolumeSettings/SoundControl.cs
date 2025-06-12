using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

namespace RWM
{
    public class SongControl : MonoBehaviour
    {
        public AudioSource[] songsLoop;
        public AudioSource song;

        float time;
        bool toggle;

        void Start()
        {
            toggle = true;
            for (int i = 0; i < songsLoop.Length; i++)
            {
                songsLoop[i].Play();
            }
            for (int i = 0; i < songsLoop.Length; i++)
            {
                songsLoop[i].Pause();
            }

        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            if (song.isPlaying == false && toggle == true)
            {
                Loop();
                toggle = false;
            }

        }
        public void Loop()
        {
            for (int i = 0; i < songsLoop.Length; i++)
            {
                songsLoop[i].UnPause();
            }
        }

        public void Stop()
        {

            song.Pause();

        }
        
         public static IEnumerator FadeAudio(AudioSource audioSource, float targetVolume, float duration)
    {
        float startVolume = audioSource.volume;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / duration;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t);
            yield return null; 
        }

        audioSource.volume = targetVolume;
    }
    }
}
