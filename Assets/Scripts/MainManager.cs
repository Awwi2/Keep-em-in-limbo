using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    [SerializeField] int Layer = -1;
    public float health = 3;
    private float maxHealth = 3;
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
        Debug.Log("LETS GO!");
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
        health = maxHealth;
        SceneManager.LoadScene("Main Hub");
    }
}
