using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    public PlayerHealth healthScript;

    private bool onEdgeBorder = false;
    private bool onEdgeGround = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(onEdgeBorder && onEdgeGround && healthScript.health > 0)
        {
            healthScript.health = 0;
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            onEdgeBorder = true;
        }

        if(collision.gameObject.layer == 8)
        {
            onEdgeGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            onEdgeBorder= false;
        }

        if (collision.gameObject.layer == 8)
        {
            onEdgeGround = false;
        }
    }
}
