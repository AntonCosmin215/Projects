using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatforms : MonoBehaviour
{
    public static SpawnPlatforms instance;
    public GameObject[] platforms;
    public Transform[] spawnPoints;
    public float platformSpawnChance;
    public float timeToSpawn;
    float timeCount;
    float random;
    int randomTypesPlatform,i;
    public float distanceBetweenPlatforms;
    int platformsSpawned;
    public bool isok;
    [Header("spikes")]
    public GameObject spikes;
    [Range(0f,100)]public float spikesChance;
    [Range(0f, 0.1f)] public float spikesBoost;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        float spawnChanceStart = platformSpawnChance;
        platformSpawnChance = 100f;
        UrcaCu4();
        platformSpawnChance = spawnChanceStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeCount>0)
        {
            timeCount -= Time.deltaTime;
        }
        else
        {
            if (Vector3.Distance(transform.position, playerMovement.instance.transform.position) > 25f)
            {
                SpawnPlatform();
                timeCount = timeToSpawn;
            }
            else
            {
                UrcaCu4();
            }
        }    
    }

    private void SpawnPlatform()
    {
        platformsSpawned = 0;
        foreach (Transform spawnPoint in spawnPoints)
        {
            random = Random.Range(0f,1f);
            if(random<= platformSpawnChance/100)
            {
                randomTypesPlatform = (int)Random.Range(0f, platforms.Length-0.5f);
                Instantiate(platforms[randomTypesPlatform], spawnPoint.position, spawnPoint.rotation);
                platformsSpawned++;
            }
            else
            {
                if(platformSpawnChance + spikesChance > 99)
                {
                    spikesChance = 99 - platformSpawnChance;
                }
                if(random<= (platformSpawnChance + spikesChance) / 100)
                {
                    Instantiate(spikes, spawnPoint.position, spawnPoint.rotation);
                }
            }
        }
        if (platformsSpawned<1)
        {
            platformsSpawned = (int)Random.Range(0f, spawnPoints.Length - 0.5f);
            randomTypesPlatform = (int)Random.Range(0f, platforms.Length - 0.5f);
            Instantiate(platforms[randomTypesPlatform], spawnPoints[platformsSpawned].position, spawnPoints[platformsSpawned].rotation);
            /*randomTypesPlatform = (int)Random.Range(0f, spawnPoints.Length - 0.5f);
            if (randomTypesPlatform == platformsSpawned)
            {
                platformsSpawned= (int)Random.Range(0f, spawnPoints.Length - 1.5f);
                randomTypesPlatform = (randomTypesPlatform + platformsSpawned) % 3;
                Instantiate(spikes, spawnPoints[randomTypesPlatform].position, spawnPoints[randomTypesPlatform].rotation);
            }
            else
            {
                Instantiate(spikes, spawnPoints[randomTypesPlatform].position, spawnPoints[randomTypesPlatform].rotation);
            }*/
        }
        spikesChance += spikesBoost;
    }
    public void UrcaCu4()
    {
        i = 0;
        while (i < 7)
        {
            SpawnPlatform();
            transform.position += new Vector3(0f, distanceBetweenPlatforms, 0f);
            i++;
        }
    }
}
