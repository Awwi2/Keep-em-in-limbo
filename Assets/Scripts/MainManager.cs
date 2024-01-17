using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    [SerializeField] int originalMaxHealth = 3;
    [SerializeField] float originalMoveSpeed = 7f;
    [SerializeField] float originalDashCooldown = 1f;
    [SerializeField] float originalDashCooldownSpeed = 0.75f;
    [SerializeField] float originalDashSpeed = 20f;

    public int health = 3;
    public int maxHealth = 3;
    public float moveSpeed = 7f;
    public float dashCooldown = 1f;
    public float dashCooldownSpeed = 0.75f;
    public float dashSpeed = 20f;
    public int corruption = 0;
    public bool isSlip = false;

    [SerializeField] int Layer = -1;
    [SerializeField] List<Vector2Int> layerSizes; // -1 ; -1 if is a Story Layer
    public bool paused = false;
    public int deathCount = 0;

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
        Layer++;

        if (layerSizes[Layer].x != -1)
        {
            Instance.GetComponent<NonVisualMazeGenerator>().Awake();
            Instance.GetComponent<NonVisualMazeGenerator>().GenerateMaze(layerSizes[Layer]);
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }

    public void Downstairs()
    {       
        if (layerSizes[Layer + 1].x == -1)
        {
            SceneManager.LoadScene("Ending");
        } 
        else
        {
            SceneManager.LoadScene("Dungeon generator");
        }
    }
    public void Death()
    {
        deathCount += 1;
        maxHealth = originalMaxHealth;
        health = originalMaxHealth;
        moveSpeed = originalMoveSpeed;
        dashCooldown = originalDashCooldown;
        dashCooldownSpeed = originalDashCooldownSpeed;
        dashSpeed = originalDashSpeed;
        SceneManager.LoadScene("Main Hub");
    }
    public void Damage(int amount)
    {       
        if (amount >= health) 
        {
            health = 0;
            Death();
        }
        else { health -= amount; }
        

    }
}
