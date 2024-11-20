using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    public GameObject settingCanvas;
    public GameObject clickPrefabSound;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Instantiate(clickPrefabSound);
        PlayerPrefs.SetFloat("bgmduration", audioSource.time);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Instantiate(clickPrefabSound);
        Application.Quit();
    }

    public void Setting()
    {
        Instantiate(clickPrefabSound);
        settingCanvas.SetActive(true);
    }
}
