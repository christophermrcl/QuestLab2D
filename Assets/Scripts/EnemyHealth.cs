using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 10;
    public bool isDead = false;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0f && !isDead)
        {
            isDead = true;
        }

        if (isDead && sr.color.a >= 0.01f)
        {
            sr.color = new Color(255, 255, 255, sr.color.a - Time.deltaTime * 2f);
        }else if (isDead)
        {
            Destroy(this.gameObject);
        }
    }

    
}
