using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField]
    string _gunName;
    bool collected;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !collected)
        {
            collected = true;
            AudioManager.instance.PlaySFX(4);
            other.gameObject.GetComponent<PlayerController>().AddGun(_gunName);
            Destroy(gameObject);
        }
    }
}
