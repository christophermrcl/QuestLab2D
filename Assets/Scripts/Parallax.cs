using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject prefab;

    public GameObject parent;
    public GameObject prev2;
    public GameObject prev;
    public GameObject current;
    
    
    public GameObject next;
    public GameObject next2;

    public float platformSpeedChange = 1f;
    public float currentPlatformSpeed = 1f;

    public GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        GameObject init1 = Instantiate(prefab, parent.transform);
        init1.transform.localPosition = new Vector2(current.transform.localPosition.x + 5.52f, current.transform.localPosition.y);
        next = init1;

        GameObject init2 = Instantiate(prefab, parent.transform);
        init2.transform.localPosition = new Vector2(current.transform.localPosition.x + 11.04f, current.transform.localPosition.y);
        next2 = init2;

        gameState = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.isPaused)
        {
            return;
        }

        currentPlatformSpeed += platformSpeedChange * Time.deltaTime;
        parent.transform.position = new Vector2(parent.transform.position.x - (Time.deltaTime * currentPlatformSpeed), parent.transform.position.y);
    }

    public void PlayerStepped()
    {
        Destroy(prev2.gameObject);
        prev2 = prev;
        prev = current;
        current = next;
        next = next2;

        GameObject tilemapGenerated = Instantiate(prefab, parent.transform);
        tilemapGenerated.transform.localPosition = new Vector2(current.transform.localPosition.x + 11.04f, current.transform.localPosition.y);
        next2 = tilemapGenerated;

    }
}
