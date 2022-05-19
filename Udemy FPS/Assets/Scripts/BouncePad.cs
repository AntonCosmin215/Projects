using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField]
    float bounceForce;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        PlayerController.instance.Bounce(bounceForce);
    }
}
