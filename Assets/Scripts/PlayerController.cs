using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameState gameState;

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private Animator anim;

    private bool isGrounded;

    private float jumpPower = 8f;
    private float speed = 2f;

    private float jumpBuffer;
    private float jumpBufferTime = 0.05f;

    private PlayerHealth playerHealthScript;

    public float meleeAttackCooldown = 0.4f;
    private float meleeAttackBuffer;
    public GameObject meleeVisualPrefab;
    public GameObject meleeColliderPrefab;

    public float rangedAttackCooldown = 0.2f;
    private float rangedAttackBuffer;
    public GameObject rangedAttackPrefab;

    private RangedAmmo rangedAmmoScript;

    public Image meleeReloadFill;

    public TilemapGenerate tilemapGenerateScript;

    public GameObject jumpSoundPrefab;
    public GameObject meleeSoundPrefab;
    public GameObject rangedSoundPrefab;
    private void Start()
    {
        rangedAmmoScript = GetComponent<RangedAmmo>();
        playerHealthScript = GetComponent<PlayerHealth>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gameState = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameState>();
    }

    void Update()
    {
        if (gameState.isPaused)
        {
            rb2d.bodyType = RigidbodyType2D.Static;
            return;
        }
        rb2d.bodyType = RigidbodyType2D.Dynamic;

        Move();
        Attack();
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        if(moveHorizontal != 0f)
        {
            anim.SetFloat("Walk", 1);
        }
        else
        {
            anim.SetFloat("Walk", 0);
        }

        if (playerHealthScript.isKnockback)
        {
            rb2d.velocity = new Vector2(playerHealthScript.xPosEnemy < transform.position.x? playerHealthScript.knockbackAmount : -playerHealthScript.knockbackAmount, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
        }
        

        if (GroundedCheck())
        {
            jumpBuffer = jumpBufferTime;
        }
        else
        {
            jumpBuffer -= Time.deltaTime;
        }

        if (jumpBuffer > 0f && Input.GetKeyDown(KeyCode.W))
        {
            Instantiate(jumpSoundPrefab);
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
        }

        if (Input.GetKeyDown(KeyCode.W) && rb2d.velocity.y > 0f)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);

            jumpBuffer = 0f;
        }

        if (GroundedCheck())
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isFall", false);
        }
        else if(rb2d.velocity.y > 0f)
        {
            anim.SetBool("isJump", true);
            anim.SetBool("isFall", false);
        }
        else if(rb2d.velocity.y < 0f)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isFall", true);
        }
        else
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isFall", false);
        }

        Flip(moveHorizontal);

        if(moveHorizontal == 0)
        {
            this.transform.position = new Vector2(this.transform.position.x - (Time.deltaTime * tilemapGenerateScript.currentPlatformSpeed), this.transform.position.y);
        }
        
    }

    private void Flip(float direction)
    {
        if (direction < 0f)
        {
            sr.flipX = true;
        }
        else if(direction > 0f)
        {
            sr.flipX = false;
        }
    }

    private bool GroundedCheck() 
    {
        return isGrounded;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isGrounded = false;
        }
    }

    private void Attack()
    {
        if (meleeAttackBuffer > 0f)
        {
            meleeAttackBuffer -= Time.deltaTime;
        } else if (Input.GetMouseButtonDown(0) && meleeAttackBuffer <= 0f)
        {
            Instantiate(meleeSoundPrefab);
            GameObject slashCollider = Instantiate(meleeColliderPrefab, transform.position, Quaternion.identity);
            GameObject slashVisual = Instantiate(meleeVisualPrefab, transform.position, Quaternion.identity);

            if (sr.flipX)
            {
                slashCollider.transform.localScale = new Vector2(-1, 1);
                slashVisual.transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                slashCollider.transform.localScale = new Vector2(1, 1);
                slashVisual.transform.localScale = new Vector2(1, 1);
            }

            slashVisual.transform.SetParent(transform);
            meleeAttackBuffer = meleeAttackCooldown;
        }

        meleeReloadFill.fillAmount = 1 - (meleeAttackBuffer / meleeAttackCooldown);

        if (rangedAttackBuffer > 0f)
        {
            rangedAttackBuffer -= Time.deltaTime;
        }
        else if (Input.GetMouseButtonDown(1) && rangedAttackBuffer <= 0f && rangedAmmoScript.currentAmmoCount > 0)
        {
            Instantiate(rangedSoundPrefab);
            rangedAmmoScript.Shot();
            GameObject projectile = Instantiate(rangedAttackPrefab, transform.position, Quaternion.identity);

            if (sr.flipX)
            {
                projectile.GetComponent<Projectile>().isLeft = true;
            }
            else
            {
                projectile.GetComponent<Projectile>().isLeft = false;
            }
        }
    }
}
