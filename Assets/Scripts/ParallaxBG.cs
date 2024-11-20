using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    public Parallax parallaxScript;
    // Start is called before the first frame update
    void Start()
    {
        parallaxScript = this.gameObject.transform.parent.GetComponent<Parallax>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (this.gameObject == parallaxScript.next)
            {
                parallaxScript.PlayerStepped();
            }
        }
    }
}
