using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().text = "HIGHSCORE: " + Mathf.Round(PlayerPrefs.GetFloat("highscore", 0)).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
