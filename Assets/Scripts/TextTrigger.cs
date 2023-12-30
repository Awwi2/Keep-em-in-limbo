using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] GameObject text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            MainManager.Instance.paused = true;
            text.SetActive(true);
            Debug.Log("Starting the Dialogue - Trigger");
            text.GetComponent<TextBoxes>().StartDialogue();
            gameObject.SetActive(false);
        }
    }
}
