using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class books : MonoBehaviour
{
    public static books instace;

    private void Awake()
    {
        instace = this;
        
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIController.instance.UpdateBooks();
            playerMovement.instance.changeAvatar();
            Destroy(gameObject);
        }
    }
}
