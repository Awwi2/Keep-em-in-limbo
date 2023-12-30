using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] GameObject item;
    List<StatItems> items;
    List<StatItems> usableItems = new List<StatItems>();

    void Start()
    {
        items = new List<StatItems>(Resources.LoadAll<StatItems>("items"));
        int r = Random.Range(1,101);
        foreach (StatItems i in items)
        {
            Debug.Log(r);
            Debug.Log(i.rarity);
            if (r <= i.rarity)
            {
                usableItems.Add(i);
            }
        }
        GameObject itm = Instantiate(item, gameObject.transform);
        itm.GetComponent<CollectItem>().Item = usableItems[Random.Range(0,usableItems.Count)];
        itm.GetComponent<SpriteRenderer>().sprite = itm.GetComponent<CollectItem>().Item.image;
    }
}
