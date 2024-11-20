using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    private GameObject PauseCanvas;
    private GameState gameState;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        PauseCanvas = this.gameObject;
        gameState = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameState>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject && Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }

    public void Resume()
    {
        gameState.isPaused = false;
        PauseCanvas.SetActive(false);
    }

    public void MainMenu()
    {
        PlayerPrefs.SetFloat("bgmduration", audioSource.time);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
