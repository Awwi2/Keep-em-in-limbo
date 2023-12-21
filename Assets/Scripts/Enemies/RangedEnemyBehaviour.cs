using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBehaviour : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f; // Adjust the speed of the enemy
    [SerializeField] float detectionRange = 5f; // Range within which the enemy detects the player
    [SerializeField] float shotCooldown = 1f;
    [SerializeField] float projectileSpeed = 4f;
    [SerializeField] float offsetDistance = 1f;

    public GameObject projectilePrefab;


    private Rigidbody2D rb;
    private GameObject player;
    private Transform playerTrans;
    private bool isPlayerInRange;
    private PlayerMovement playerMovement;
    private float shotCooldownCounter;


    void Start()
    {
        shotCooldownCounter = shotCooldown;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("player"); // Assumes the player has the "Player" tag
        playerTrans = player.transform;
        playerMovement = player.GetComponent<PlayerMovement>();
        isPlayerInRange = false;
        InvokeRepeating("CheckPlayerInRange", 0f, 0.5f); // Check for player in range every 0.5 seconds
    }

    void Update()
    {
        if (shotCooldownCounter != shotCooldown)
        {
            shotCooldownCounter += Time.deltaTime;
            if (shotCooldownCounter >= shotCooldown)
            {
                shotCooldownCounter = shotCooldown;
            }
        }
      
        if (isPlayerInRange)
        {
            Vector2 direction = (playerTrans.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * -moveSpeed * Time.fixedDeltaTime);
            if(shotCooldownCounter >= shotCooldown)
            {
                Shoot();
                shotCooldownCounter = 0f;
            }
            
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

        if (collision.collider.tag == "player") // Check if the collider has a specific tag
        {

            if (playerMovement.dashCounter <= 0f) //Player only takes dmg when he is not dashing
            {
                MainManager.Instance.health -= 1;
            }
            else
            {
                Destroy(gameObject); // Enemy is killed by player
            }

        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 direction = (playerTrans.position - transform.position).normalized;
        // Offset the projectile's spawn position in the direction the enemy is facing
        Vector2 spawnPosition = (Vector2)transform.position + direction * offsetDistance;
        projectile.transform.position = spawnPosition;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }

}
