using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameover;
    [SerializeField]
    GameObject _mainMenu;
    [SerializeField]
    GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        gameover = true;
        Time.timeScale = 0f;
        Instantiate(_player, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1f;
            _mainMenu.SetActive(false);
            UiController.Instance.updateScore();
        }
        if (!FindObjectOfType<Player>())
        {
            if(PlayerPrefs.HasKey("score"))
            {
                if (UiController.Instance.GetScore() > PlayerPrefs.GetInt("score"))
                    PlayerPrefs.SetInt("score", UiController.Instance.GetScore());
            }else
                PlayerPrefs.SetInt("score", UiController.Instance.GetScore());
            StartCoroutine(restart());
        }
    }
    IEnumerator restart()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
