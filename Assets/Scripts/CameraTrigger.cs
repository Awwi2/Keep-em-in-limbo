using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    GameObject Cam;

    void Start()
    {
        Cam = GameObject.Find("Main Camera");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "player")
        {
            Cam.gameObject.GetComponent<Transform>().position = transform.position - new Vector3(0,0,10);
        }
    }
}
