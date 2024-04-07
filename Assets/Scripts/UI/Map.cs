using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;


public class Map : MonoBehaviour
{
    [SerializeField] Camera MainCamera;
    [SerializeField] Camera MapCamera;
    private bool mapActive = false;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        MapCamera.enabled = false;
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneChanged;
    }

    private void SceneChanged(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
    {
        MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        MapCamera.transform.position = new Vector3(-52, 27.8f, -10);
        MapCamera.orthographicSize = 45;
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
