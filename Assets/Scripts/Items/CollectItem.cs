using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectItem : MonoBehaviour
{
    public StatItems Item;
    public GameObject CollectionGUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            GameObject GUI = Instantiate(CollectionGUI);
            GUI.transform.parent = GameObject.Find("Canvas").transform;
            GUI.GetComponent<RectTransform>().anchoredPosition =  new Vector3(0,0,0);
            GUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Item.name;
            GUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Item.description;
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
