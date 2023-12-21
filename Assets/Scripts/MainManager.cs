using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    [SerializeField] int Layer = -1;
    public float health = 3;
    [SerializeField] List<Vector2Int> layerSizes; // -1 ; -1 if is a Story Layer

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
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
            Instance.GetComponent<NonVisualMazeGenerator>().GenerateMaze(layerSizes[Layer]);
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("U ded!");
        }
    }
}
