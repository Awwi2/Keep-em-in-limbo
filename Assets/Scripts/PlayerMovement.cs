using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] BoxCollider2D dashCollider;
    [SerializeField] BoxCollider2D normalCollider;

    Vector2 movement;

    //Dash stuff
    float activeMoveSpeed;
    [SerializeField] float dashLength = 0.5f;
    public float dashCounter; //for access in other scripts this is public (only read)
    private float dashCoolCounter;
    [SerializeField] SpriteRenderer eyeSprite;
    [SerializeField] float slipperySpeedModifier = 0.03f;
    [SerializeField] int slipSpeedCap = 20;
    [SerializeField] Sprite[] eyes;


    private void Start()
    {
        activeMoveSpeed = MainManager.Instance.moveSpeed;
        eyeSprite.sprite = eyes[0];
        eyeSprite.size = new Vector2(100, 2);
    }

    void Update()
    {
        if (!MainManager.Instance.paused)
        {
            if (dashCounter <= 0f)
            {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
                movement.Normalize();
            }

            if (movement.x > 0)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (movement.x < 0)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }

            if (movement != new Vector2(0, 0))
            {
                gameObject.GetComponent<Animator>().SetBool("Walking", true);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("Walking", false);
            }

            if (MainManager.Instance.isSlip)
            {
                rb.velocity += movement * activeMoveSpeed * slipperySpeedModifier * Time.deltaTime * 600;

                if (rb.velocity.x > slipSpeedCap && dashCounter <= 0)
                {
                    rb.velocity = new Vector2(slipSpeedCap, rb.velocity.y);
                } else if (rb.velocity.x < -slipSpeedCap && dashCounter <= 0)
                {
                    rb.velocity = new Vector2(-slipSpeedCap, rb.velocity.y);
                }
                if (rb.velocity.y > slipSpeedCap && dashCounter <= 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, slipSpeedCap);
                }
                else if (rb.velocity.y < -slipSpeedCap && dashCounter <= 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -slipSpeedCap);
                }
            }
            else
            {
                rb.velocity = movement * activeMoveSpeed * Time.deltaTime * 600;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dashCounter == 0 && dashCoolCounter <= 0)
                {
                    //Player is dashing
                    eyeSprite.sprite = eyes[1];
                    normalCollider.enabled = false;
                    dashCollider.enabled = true;
                    gameObject.GetComponent<Animator>().SetBool("Dashing", true);
                    activeMoveSpeed = MainManager.Instance.dashSpeed;
                    dashCounter = dashLength;
                }
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;

                if (dashCounter <= 0)
                {
                    //Dash vorbei
                    eyeSprite.sprite = eyes[2];
                    dashCounter = 0;
                    gameObject.GetComponent<Animator>().SetBool("Dashing", false);
                    normalCollider.enabled = true;
                    dashCollider.enabled = false;
                    activeMoveSpeed = MainManager.Instance.moveSpeed * MainManager.Instance.dashCooldownSpeed;
                    dashCoolCounter = MainManager.Instance.dashCooldown;
                }
            }
            if (dashCoolCounter > 0)
            {
                //Dash on cooldown
                dashCoolCounter -= Time.deltaTime;
            }
            else if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                //Dash no longer on cooldown
                eyeSprite.sprite = eyes[0];
                activeMoveSpeed = MainManager.Instance.moveSpeed;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            gameObject.GetComponent<Animator>().SetBool("Walking", false);

        }
    }
}
