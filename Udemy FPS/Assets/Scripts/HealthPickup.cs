using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    bool collected;
    [SerializeField]
    int _healthAdd=1;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !collected)
        {
            collected = true;
            AudioManager.instance.PlaySFX(5);
            PlayerHealthController.instance.AddHealth(_healthAdd);
            Destroy(gameObject);
        }
    }
}
