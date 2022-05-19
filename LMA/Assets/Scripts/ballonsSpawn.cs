using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballonsSpawn : MonoBehaviour
{
    public Transform[] spawns;
    public GameObject[] baloane;
    // Start is called before the first frame update
    public float timespawn;
    private float timeCount;

    [Header("Baloane")]
    public Transform[] targets;
    int k;
    public float viteza;

    private void Update()
    {
        if (timeCount >= 0)
        {
            timeCount -= Time.deltaTime;
        }
        else
        {
            int k = (int)Random.Range(0f, spawns.Length - 0.5f);
            int j = (int)Random.Range(0f, baloane.Length - 0.5f);
            GameObject balon = Instantiate(baloane[j], spawns[k].position, spawns[k].rotation);
            k = (int)Random.Range(0f, targets.Length - 0.5f);
            balon.GetComponent<target>().setTarget(targets[k].position, viteza);
            timeCount = timespawn;
        }

    }
}
