using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int health = 3;
    public Rigidbody2D rb;
    private Renderer rend;

    Vector2 movement;

    //Dash stuff
    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = .5f, dashCooldown = 1f;

    public float dashCounter; //for access in other scripts this is public (only read)
    private float dashCoolCounter;


    private void Start()
    {
        activeMoveSpeed = moveSpeed;
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (dashCounter <= 0f) { 
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement.Normalize();
        }
        

        rb.velocity = movement * activeMoveSpeed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space was pressed!");
            if (dashCounter <=0 && dashCounter <= 0)
            {
                Debug.Log("Now dashing!");
                rend.material.color = Color.red;
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                //Dash vorbei
                rend.material.color = Color.green;
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }
        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
        if( health <= 0)
        {
            Debug.Log("U ded!");
        }
    }
    /*
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    */
}
