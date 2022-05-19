using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public int macarons, books;
    public Text macaronsText, booksText, HealthText;
    public int bestMacarons, bestBooks;

    [Header("Best")]
    public Text bestMacaronsText, bestBooksText;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        macarons = 0;
        books = 0;
        macaronsText.text = ": " + macarons.ToString();
        booksText.text = ": " + books.ToString();
        UpdateBest();
    }

    public void UpdateBooks()
    {
        books++;
        PlayerPrefs.SetInt("booksCurrent", UIController.instance.books);
        booksText.text = ": " + books.ToString();
        if (PlayerPrefs.HasKey("books"))
        {
            if (books > PlayerPrefs.GetInt("books"))
            { PlayerPrefs.SetInt("books", books);
                UpdateBest();
            }
        }
        else
        {
            PlayerPrefs.SetInt("books", books);
        }
    }
    public void UpdateMacarons()
    {
        macarons++;
        PlayerPrefs.SetInt("macaronsCurrent", UIController.instance.macarons);
        macaronsText.text = ": " + macarons.ToString();
        if (PlayerPrefs.HasKey("macarons"))
        {
            if (macarons > PlayerPrefs.GetInt("macarons"))
            {
                PlayerPrefs.SetInt("macarons", macarons);
                UpdateBest();
            }
        }
        else
        {
            PlayerPrefs.SetInt("macarons", macarons);
        }
    }
    public void UpdateHealth(int health)
    {
        HealthText.text = health.ToString();
    }
    public void UpdateBest()
    {
        if (PlayerPrefs.HasKey("macarons"))
        {
            bestMacaronsText.text = PlayerPrefs.GetInt("macarons").ToString();
        }
        if (PlayerPrefs.HasKey("books"))
        {
            bestBooksText.text = PlayerPrefs.GetInt("books").ToString();
        }
    }
}
