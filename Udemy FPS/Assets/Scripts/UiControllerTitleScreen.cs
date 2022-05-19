using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UiControllerTitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    string _levelName;
    [Header("LoadingScreen")]
    public Image loadingScreen;
    public float fadespeed;

    bool toBlack=false;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        loadingScreen.color = new Color(loadingScreen.color.r, loadingScreen.color.g, loadingScreen.color.b,1);
        toBlack = false;
    }
    public void GoToLevel()
    {
        toBlack = true;
        StartCoroutine(Gotolevel());
    }
    public void Quit()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (!toBlack)
        loadingScreen.color = new Color(loadingScreen.color.r, loadingScreen.color.g, loadingScreen.color.b, Mathf.MoveTowards(loadingScreen.color.a, 0, fadespeed * Time.deltaTime));
        else
        loadingScreen.color = new Color(loadingScreen.color.r, loadingScreen.color.g, loadingScreen.color.b, Mathf.MoveTowards(loadingScreen.color.a, 1, fadespeed * Time.deltaTime));
    }
    IEnumerator Gotolevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(_levelName);
    }
}
