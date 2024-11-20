using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;

    private bool isInvisible;

    public float invisibleTime;
    private float invisibleBuffer = 0f;
    public float blinkInterval = 0.1f;
    private bool isBlinking;

    public float knockbackAmount = 3f;
    public float knockbackDuration = 0.1f;
    private float knockbackBuffer = 0f;
    public bool isKnockback = false;
    public float xPosEnemy;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;

    public GameObject gameOverCanvas;
    private GameState gameState;

    public GameObject particlePrefab;

    public List<GameObject> hpIcon;

    public GameObject hurtSoundPrefab;
    public GameObject deadSoundPrefab;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gameState = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameState>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.isPaused)
        {
            return;
        }

        if(invisibleBuffer > 0f)
        {
            invisibleBuffer -= Time.deltaTime;
            isInvisible = true;
        }
        else
        {
            isInvisible = false;
        }

        if (knockbackBuffer > 0f)
        {
            knockbackBuffer -= Time.deltaTime;
            isKnockback = true;
        }
        else
        {
            isKnockback = false;
        }

        if(health <= 0 && !gameOverCanvas.activeSelf)
        {
            Instantiate(deadSoundPrefab);
            anim.SetBool("isDead", true);
            for (int i = 0; i < 5 - health; i++)
            {
                hpIcon[4 - i].SetActive(false);
            }
            Instantiate(particlePrefab, transform.position, Quaternion.identity);
            gameOverCanvas.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7 && !collision.GetComponent<EnemyHealth>().isDead)
        {
            if (!isInvisible)
            {
                xPosEnemy = collision.transform.position.x;
                invisibleBuffer = invisibleTime;
                knockbackBuffer = knockbackDuration;
                Hurt();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            health = 0;
        }
    }

    private void Hurt()
    {
        Instantiate(hurtSoundPrefab);
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
        if (sr.flipX == true)
        {
            
            rb.AddForce(new Vector2(knockbackAmount, 0));
        }
        else
        {
            rb.AddForce(new Vector2(-knockbackAmount, 0));
        }
        StartBlinking();
        health -= 1;

        for(int i = 0; i < 5 - health; i++)
        {
            hpIcon[4 - i].SetActive(false);
        }
    }

    public void StartBlinking()
    {
        if (!isBlinking)
        {
            StartCoroutine(BlinkCoroutine());
        }
    }

    private IEnumerator BlinkCoroutine()
    {
        isBlinking = true;
        float timer = 0f;

        while (timer < invisibleTime)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        sr.enabled = true;
        isBlinking = false;
    }
}
