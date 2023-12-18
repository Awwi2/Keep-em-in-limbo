using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float moveSpeed = 3f; // Adjust the speed of the enemy
    public float detectionRange = 5f; // Range within which the enemy detects the player

    private Rigidbody2D rb;
    private Transform player;
    private bool isPlayerInRange;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("player").transform; // Assumes the player has the "Player" tag
        isPlayerInRange = false;
        InvokeRepeating("CheckPlayerInRange", 0f, 0.5f); // Check for player in range every 0.5 seconds
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void CheckPlayerInRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
        }
    }
}
