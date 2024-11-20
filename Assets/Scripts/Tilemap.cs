using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap : MonoBehaviour
{
    public TilemapGenerate tilemapGenerateScript;
    // Start is called before the first frame update
    void Start()
    {
        tilemapGenerateScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TilemapGenerate>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if(this.gameObject == tilemapGenerateScript.next)
            {
                tilemapGenerateScript.PlayerStepped();
            }
        }
    }
}
