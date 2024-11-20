using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapGenerate : MonoBehaviour
{
    public List<GameObject> tilemaps = new List<GameObject>();
    private int tilemapsCount;

    public GameObject gridObject;

    public GameObject current;
    public GameObject prev;
    public GameObject prev2;
    public GameObject next;
    public GameObject next2;

    public float currentPlatformSpeed = 1f;
    public float platformSpeedChange = 1f;

    public GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        tilemapsCount = tilemaps.Count;
        GameObject init1 = Instantiate(tilemaps[ (int) Mathf.Floor(Random.Range(0, tilemapsCount)) ], gridObject.transform);
        init1.transform.localPosition = new Vector2(current.transform.localPosition.x + 22f, current.transform.localPosition.y);
        next = init1;

        GameObject init2 = Instantiate(tilemaps[(int)Mathf.Floor(Random.Range(0, tilemapsCount))], gridObject.transform);
        init2.transform.localPosition = new Vector2(current.transform.localPosition.x + 44f, current.transform.localPosition.y);
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
        gridObject.transform.position = new Vector2(gridObject.transform.position.x - (Time.deltaTime * currentPlatformSpeed), gridObject.transform.position.y);
    }

    public void PlayerStepped()
    {
        if (prev2)
        {
            Destroy(prev2.gameObject);
        }
        
        prev2 = prev;
        prev = current;
        current = next;
        next = next2;

        GameObject tilemapGenerated = Instantiate(tilemaps[(int)Mathf.Floor(Random.Range(0, tilemapsCount))], gridObject.transform);
        tilemapGenerated.transform.localPosition = new Vector2(current.transform.localPosition.x + 44f, current.transform.localPosition.y);
        next2 = tilemapGenerated;

    }
}
