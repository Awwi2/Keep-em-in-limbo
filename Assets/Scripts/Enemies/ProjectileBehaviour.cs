using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "player")
        {
            //deal dmg on hit if player aint dashing
            if (collision.collider.GetComponent<PlayerMovement>().dashCounter <= 0f)
            {
                MainManager.Instance.health -= 1;
                
            }
            Destroy(gameObject);
        }
        else if(collision.collider.tag != "enemy")
        {
            Destroy(gameObject);
        }
    }
}
