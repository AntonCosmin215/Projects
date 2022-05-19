using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gogoasaRoz : MonoBehaviour
{
    public float dropRate;
    public float fallSpeed;
    public GameObject[] objectsToSpawn;
    public Transform[] spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform i in spawnPoints)
        {
            float j = Random.Range(0f, 1f);
            if (j < dropRate/100f)
            {
                int k = (int) Random.Range(0f, objectsToSpawn.Length);
                GameObject drop=Instantiate(objectsToSpawn[k], i.position, i.rotation);
                drop.transform.parent = gameObject.transform;
            }
        }
    }
    private void Update()
    {
        transform.position -= new Vector3(0f, fallSpeed * Time.deltaTime, 0f);
    }
}
