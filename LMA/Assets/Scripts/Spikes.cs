using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float fallSpeed;
    private void Update()
    {
        transform.position -= new Vector3(0f, fallSpeed * Time.deltaTime, 0f);
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth.instance.DealDamage();
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
