using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    bool collected;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !collected)
        {   
            collected = true;
            AudioManager.instance.PlaySFX(3);
            other.gameObject.GetComponent<PlayerController>().AmmoPickUpCollected();
            Destroy(gameObject);
        }
    }
}
