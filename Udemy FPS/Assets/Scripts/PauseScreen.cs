using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseScreen : MonoBehaviour
{
    [SerializeField]
    string _MainMenu;
    // Start is called before the first frame update
    public void Resume()
    {
        GameManager.instance.ActivateOrDesactivatePauseMenu();
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(_MainMenu);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
