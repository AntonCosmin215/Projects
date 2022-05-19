using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField]
    int _timeToDestroy;
    [SerializeField]
    GameObject[] jets;
    // Start is called before the first frame update
    void Start()
    {
        if (jets.Length>0)
            foreach (var jet in jets)
            {
                Destroy(jet,0.5f);
            }
        Destroy(gameObject,_timeToDestroy);
    }
}
