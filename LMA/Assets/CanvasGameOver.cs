using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasGameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public Text bestMac, bestbok;
    public Text curentMac, curentbok;

    void Start()
    {
        
        if (PlayerPrefs.HasKey("macarons"))
        {
            bestMac.text = PlayerPrefs.GetInt("macarons").ToString();
        }
        if (PlayerPrefs.HasKey("books"))
        {
            bestbok.text = PlayerPrefs.GetInt("books").ToString();
        }
        if (PlayerPrefs.HasKey("macaronsCurrent"))
        {
            curentMac.text = PlayerPrefs.GetInt("macaronsCurrent").ToString();
        }
        if (PlayerPrefs.HasKey("booksCurrent"))
        {
            curentbok.text = PlayerPrefs.GetInt("booksCurrent").ToString();
        }
    }
    public void playagain()
    {
        SceneManager.LoadScene("Play");
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
