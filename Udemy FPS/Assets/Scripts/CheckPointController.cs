using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointController : MonoBehaviour
{
    [SerializeField]
    string _cpname;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_cp"))
        {
            if (PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "_cp") == _cpname)
            {
                PlayerController.instance.transform.position = transform.position;
            }
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioManager.instance.PlaySFX(1);
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", _cpname);
        }
    }
}
