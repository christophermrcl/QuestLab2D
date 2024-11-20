using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI scoreText;
    private GameState gameState;
    private ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreManager>();
        gameState = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameState>();
        gameState.isPaused = true;
        scoreText.text = "SCORE: "+ Mathf.Round(scoreManager.currentScore).ToString();
        highscoreText.text = "HIGHSCORE: "+ Mathf.Round(scoreManager.highScore).ToString();
        PlayerPrefs.SetFloat("highscore", scoreManager.highScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        PlayerPrefs.SetFloat("bgmduration", audioSource.time);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Game");
    }
    public void MainMenu()
    {
        PlayerPrefs.SetFloat("bgmduration", audioSource.time);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
