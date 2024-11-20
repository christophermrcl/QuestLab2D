using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damageRanged;

    private float duration = 3f;
    public float speed = 1f;
    public bool isLeft = false;

    private SpriteRenderer sr;

    public GameObject prefabParticle;
    public GameObject shotSoundPrefab;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (duration > 0f)
        {
            duration -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (isLeft)
        {
            sr.flipX = true;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
        {
            sr.flipX = false;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Instantiate(shotSoundPrefab);
            collision.gameObject.GetComponent<EnemyHealth>().health -= damageRanged;
            Instantiate(prefabParticle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
