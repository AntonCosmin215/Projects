using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager instance;
    // Start is called before the first frame update
    public AudioSource[] melodii;
    public AudioSource[] sunete;
    int melodie=0;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        melodie = (int) Random.Range(0f, melodii.Length - 0.5f);
        if (!melodii[melodie].isPlaying)
            melodii[melodie].Play();
    }
    private void Update()
    {
        if (!melodii[melodie].isPlaying)
        {
            melodii[melodie].Stop();
            melodie++;
            if (melodie >=melodii.Length)
                melodie = 0;
            melodii[melodie].Play();
        }
    }
    public void playSFX(int i) {
        sunete[i].Stop();
        sunete[i].Play();
        
    }
}
