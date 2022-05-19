using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    float _timeToReloadLevel;

    public bool _isLoading = false;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReloadLevelPlayerDied()
    {
        StartCoroutine(ReloadLevel());
    }
    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(_timeToReloadLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActivateOrDesactivatePauseMenu();
        }
    }

    public void ActivateOrDesactivatePauseMenu()
    {
        if (UiController.instance.pauseMenu.activeInHierarchy)
        {
            UiController.instance.pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            foreach (AudioSource i in PlayerController.instance._steps)
            {
                i.Stop();
            }
            UiController.instance.pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    // Update is called once per frame

}
