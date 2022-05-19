using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public GameObject explosion;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    public int health;
    private void Start()
    {
        UIController.instance.UpdateHealth(health);
    }

    public void DealDamage()
    {
        health--;
        audioManager.instance.playSFX(0);
        UIController.instance.UpdateHealth(health);
        playerMovement.instance.takeDamage();
        if (health <= 0)
        {
            //gameObject.SetActive(false);
            playerMovement.instance.Transparent();
            playerMovement.instance.stopInput = true;
            GameObject explozie=Instantiate(explosion, transform.position, transform.rotation);
            audioManager.instance.playSFX(1);
            Destroy(explozie.gameObject,0.35f);
            StartCoroutine(Waitfor1sec());
        }
    }
    IEnumerator Waitfor1sec()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOver");
    }
}
