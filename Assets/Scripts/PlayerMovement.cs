using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] BoxCollider2D dashcollider;
    private Renderer rend;

    [SerializeField] float moveSpeed = 5f;
    public int health = 3;
    Vector2 movement;

    //Dash stuff
    private float activeMoveSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashLength = .5f, dashCooldown = 1f;
    public float dashCounter; //for access in other scripts this is public (only read)
    private float dashCoolCounter;


    private void Start()
    {
        activeMoveSpeed = moveSpeed;
        rend = GetComponent<Renderer>();
        rend.material.color = Color.green;
    }

    void Update()
    {
        if (dashCounter <= 0f) { 
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement.Normalize();
        }
        
        if(movement.x > 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        } else if (movement.x < 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        if (movement != new Vector2(0,0))
        {
            gameObject.GetComponent<Animator>().SetBool("Walking",true);
        } else
        {
            gameObject.GetComponent<Animator>().SetBool("Walking", false);
        }

        rb.velocity = movement * activeMoveSpeed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCounter <=0 && dashCoolCounter <= 0)
            {   
                //Player is dashing
                dashcollider.enabled = true;
                rend.material.color = new Color(1f, 0.5f, 0);
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
                dashcollider.enabled = false;
                activeMoveSpeed = moveSpeed * 0.75f;
                dashCoolCounter = dashCooldown;
            }
        }
        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        } 
        else if (dashCoolCounter <= 0 && dashCounter <= 0)
        {
            activeMoveSpeed = moveSpeed;
            rend.material.color = Color.green;
        }
        if( health <= 0)
        {
            Debug.Log("U ded!");
        }
    }
}
