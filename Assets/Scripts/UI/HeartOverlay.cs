using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class HeartOverlay : MonoBehaviour
{
    private Canvas Canvas;
    private int maxHeartsbefore;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    [SerializeField] MainManager MM;

    public Image[] hearts; // Array to hold heart images

    public static HeartOverlay Instance;
    private int NumOfHearts;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Instance.Start();
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        maxHeartsbefore = MM.maxHealth;
        NumOfHearts = hearts.Length;
        InitializeHearts();
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneChanged;
        Canvas = this.GetComponent<Canvas>();
    }

    private void SceneChanged(Scene arg0, Scene arg1)
    {
        Canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if ((MM.maxHealth != maxHeartsbefore) && (MM.maxHealth <= NumOfHearts))
        {
            maxHeartsbefore = MM.maxHealth;
            for (int i = 0; i < NumOfHearts; i++)
            {
                hearts[i].enabled = false;
            }
            for (int i = 0; i < MM.maxHealth; i++)
            {
                hearts[i].enabled = true;
            }
        }
        else { MM.maxHealth = maxHeartsbefore; } //This might cause chaos
        SetHealth(MM.health);
    }

    // Initialize the heart images
    void InitializeHearts()
    {
        for (int i = 0; i < NumOfHearts; i++)
        {
            hearts[i].enabled = false;
        }
        for (int i = 0; i < MM.maxHealth; i++)
        {
            hearts[i].enabled = true;
        }
    }


    // Method to set health amount
    public void SetHealth(int amount)
    {
        for (int i = 0; i < MM.maxHealth; i++)
        {
            if (i < amount)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
    
}
