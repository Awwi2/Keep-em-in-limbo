using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Does this even work?");
    }
    // Start is called before the first frame update

    // This function is called when another collider enters this object's collider
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit me");
        if (collision.collider.tag == "player") // Check if the player collides with the trigger
        {
            // Handle collision here
            Debug.Log("Collision detected with the player, now letting the ground crumble");
        }
    }
}
