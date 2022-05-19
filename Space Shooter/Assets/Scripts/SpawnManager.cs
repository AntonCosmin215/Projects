using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject _enemy;
    [SerializeField]
    GameObject[] _powerUps;
    [SerializeField]
    float _bordersX;
    [SerializeField]
    float _timeToSpawnPowerUp, _timeToSpawnEnemy,_minusTimeToSpawnEnemy;
    int _powerUpToSpawn;
    void Start()
    {
        StartCoroutine(SpawnPowerUp());
        StartCoroutine(SpawnEnemy());
        StartCoroutine(MinusTimeSpawnEnemy());
    }
    IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            _powerUpToSpawn = Random.Range(0, 3);
            Instantiate(_powerUps[_powerUpToSpawn], new Vector3(Random.Range(-_bordersX, _bordersX), 6.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(_timeToSpawnPowerUp);
        }
    }
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(_enemy, new Vector3(Random.Range(-_bordersX, _bordersX), 6.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(_timeToSpawnEnemy-0.1f,_timeToSpawnEnemy+0.1f));
        }
    }
    IEnumerator MinusTimeSpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(_minusTimeToSpawnEnemy);
            _timeToSpawnEnemy = Mathf.Clamp(_timeToSpawnEnemy - 0.1f, 3, 10);
        }
    }
}
