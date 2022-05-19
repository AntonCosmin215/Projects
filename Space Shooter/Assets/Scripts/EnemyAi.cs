using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float _enemySpeed;
    [SerializeField]
    int _enemyLifes;
    public GameObject enemyExplode;
    [SerializeField]
    AudioClip _explosion;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _enemySpeed* Time.deltaTime);
        if (transform.position.y < -6.5f)
        {
            transform.position = new Vector3(Random.Range(-8.75f, 8.75f), 6.5f, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser"))
        {
            _enemyLifes--;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            _enemyLifes = 0;
        }
        TestDestroy();
    }

    private void TestDestroy()
    {
        if (_enemyLifes<=0)
        {
            UiController.Instance.AddScore();
            Instantiate(enemyExplode,transform.position,transform.rotation);
            AudioSource.PlayClipAtPoint(_explosion, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
