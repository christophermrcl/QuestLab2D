using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject PauseCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused && Input.GetKeyDown(KeyCode.Escape)) 
        {
            PauseBtn();
        }
    }

    public void PauseBtn()
    {
        isPaused = true;
        PauseCanvas.SetActive(true);
    }
}
