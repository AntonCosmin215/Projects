using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource bgm,victory;
    public AudioSource[] sfx;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame updat

    public void StopBGM()
    {
        bgm.Stop();
    }
    public void PlaySFX(int nrlista)
    {
        sfx[nrlista].Stop();
        sfx[nrlista].Play();
    }

    public void Vistory()
    {
        StopBGM();
        victory.Play();
    }
    public void StopSFX(int nrlista)
    {
        sfx[nrlista].Stop();
    }
}
