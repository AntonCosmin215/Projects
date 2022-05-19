using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class killzone : MonoBehaviour
{
    public Transform objToFollow;
    public GameObject explosion;
    public float offset;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Max(transform.position.y, objToFollow.position.y+offset), transform.position.z);
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth.instance.health = 1;
            PlayerHealth.instance.DealDamage();
        }
        else
        Destroy(collision.gameObject);
    }
}
