using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIController2 : MonoBehaviour
{
    public string sceneToPlay;
    // Start is called before the first frame update
    public void StartButton()
    {
        SceneManager.LoadScene(sceneToPlay);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
