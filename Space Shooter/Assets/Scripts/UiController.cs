using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public static UiController Instance;
    [SerializeField]
    Sprite[] _healthSprites;
    [SerializeField]
    Image _lives;
    [SerializeField]
    Text _scoreText;
    int _score;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("score"))
        {
            _scoreText.text = "Best Score: " + PlayerPrefs.GetInt("score");
        }
        Instance = this;
        _lives.sprite = _healthSprites[3];
    }

    // Update is called once per frame
    public void UpdateLives(int i)
    {
        if (i >= 0)
            _lives.sprite = _healthSprites[i];
        else
            _lives.sprite = _healthSprites[3];
    }
    public void AddScore()
    {
        _score += 10;
        updateScore();
    }
    public void updateScore()
    {
        _scoreText.text = "Score: "+_score.ToString();
    }
    public int GetScore()
    {
        return _score;
    }
}
