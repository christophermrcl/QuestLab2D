using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Movement speed of the enemy
    public float raycastLength = 1f; // Length of the raycast
    public LayerMask groundLayer; // Layer mask for ground detection

    private bool movingLeft; // Determines if the enemy is moving left
    private SpriteRenderer sr;
    private GameState gameState;
    private Rigidbody2D rb2d;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        gameState = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameState>();
        // Randomize initial direction
        movingLeft = Random.Range(0, 2) == 0;
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
    }

    void Move()
    {
        // Set the movement direction
        float direction = movingLeft ? -1f : 1f;

        if (movingLeft)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }

        // Move the enemy
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        // Perform raycasts to check for ground
        Vector2 origin = transform.position;
        RaycastHit2D raycastHitLeft = Physics2D.Raycast(origin, Vector2.down + Vector2.left, raycastLength, groundLayer);
        RaycastHit2D raycastHitRight = Physics2D.Raycast(origin, Vector2.down + Vector2.right, raycastLength, groundLayer);

        Debug.DrawRay(origin, (Vector2.down + Vector2.left).normalized * raycastLength, Color.red);
        Debug.DrawRay(origin, (Vector2.down + Vector2.right).normalized * raycastLength, Color.blue);

        // Check if the enemy should change direction
        if (movingLeft && raycastHitLeft.collider == null)
        {
            movingLeft = false; // Change direction to right
        }
        else if (!movingLeft && raycastHitRight.collider == null)
        {
            movingLeft = true; // Change direction to left
        }
    }
}
