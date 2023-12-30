using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public StatItems Item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            Debug.Log("collect");
            MainManager.Instance.health += Item.healthModifier;
            Destroy(gameObject);
        }
    }
}
