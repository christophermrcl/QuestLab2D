using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float currentScore = 0f;
    public float highScore = 0f;
    public float scoreBeatEnemy = 50f;

    public float passiveScoreIncreaseAmount = 1f;

    public GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GetComponent<GameState>();
        highScore = PlayerPrefs.GetFloat("highscore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.isPaused)
        {
            return;
        }

        currentScore += Time.deltaTime * passiveScoreIncreaseAmount;

        if(currentScore > highScore)
        {
            highScore = currentScore;
        }
    }

    public void BeatEnemy()
    {
        currentScore += scoreBeatEnemy;
    }
}
