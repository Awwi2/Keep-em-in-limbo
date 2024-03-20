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
            MainManager.Instance.maxHealth += Item.maxHealthModfier;
            MainManager.Instance.dashCooldown += Item.dashCooldownSpeedModifier;
            MainManager.Instance.moveSpeed += Item.speedModifier;
            MainManager.Instance.dashSpeed += Item.dashSpeedModifier;
            MainManager.Instance.dashCooldownSpeed += Item.dashCooldownSpeedModifier;
            MainManager.Instance.corruption += Item.corruptionModifier;
            switch (Item.specialEffect)
            {
                case 0:
                    break;
                case 1:
                    MainManager.Instance.isSlip = true;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
