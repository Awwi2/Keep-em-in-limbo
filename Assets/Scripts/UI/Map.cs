using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Map : MonoBehaviour
{
    [SerializeField] Camera MainCamera;
    [SerializeField] Camera MapCamera;
    private bool mapActive = false;
    // Start is called before the first frame update
    void Start()
    {
        MapCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
            {
            mapActive = !mapActive;
            MainCamera.enabled = !mapActive;
            MapCamera.enabled = mapActive;
            }
        if (mapActive) { Time.timeScale = 0; }
        else { Time.timeScale = 1; }
    }
}
