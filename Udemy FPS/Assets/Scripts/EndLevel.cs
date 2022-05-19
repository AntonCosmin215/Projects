using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndLevel : MonoBehaviour
{
    [SerializeField]
    string nextLevel;
    public float timeToWait;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ToNextLev());
            AudioManager.instance.Vistory();
        }
    }

    IEnumerator ToNextLev()
    {
        GameManager.instance._isLoading= true;
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene(nextLevel);
    }
}
