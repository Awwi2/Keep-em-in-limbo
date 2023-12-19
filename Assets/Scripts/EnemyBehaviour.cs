using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float moveSpeed = 3f; // Adjust the speed of the enemy
    public float detectionRange = 5f; // Range within which the enemy detects the player

    private Rigidbody2D rb;
    private GameObject player;
    private Transform playerTrans;
    private bool isPlayerInRange;
    public PlayerMovement playerMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("player"); // Assumes the player has the "Player" tag
        playerTrans = player.transform;
        isPlayerInRange = false;
        InvokeRepeating("CheckPlayerInRange", 0f, 0.5f); // Check for player in range every 0.5 seconds
    }

    void Update()
    {

        if (isPlayerInRange)
        {
            Vector2 direction = (playerTrans.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void CheckPlayerInRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTrans.position);
        if (distanceToPlayer <= detectionRange)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("This aint it right?!");
        if (collision.collider.tag == "player") // Check if the collider has a specific tag
        {
            Debug.Log("Collision between Player and this Obj detected!");
            if (playerMovement.dashCounter <= 0f) //Player only takes dmg when he is not dashing
            {
                playerMovement.health -= 1;
            }
            else
            {
                Destroy(gameObject); // Enemy is killed by player
            }
            
        }
    }

}
