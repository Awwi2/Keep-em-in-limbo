using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies;
    [SerializeField] GameObject doors;
    int enemyNumber;

    private void Start()
    {
        enemyNumber = enemies.Count;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            doors.SetActive(true);
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemyNumber -= 1;
            if (enemyNumber == 0)
            {
                doors.SetActive(false);
            }
        }
    }
}
