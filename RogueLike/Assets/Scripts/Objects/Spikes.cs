using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int thisSpikeGoal;      
    private int enemiesCount = 0;

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Character player = collision.gameObject.GetComponent<Character>();
            if (player != null)
            {
                player.Die();
            }
        }
    }

    public void EnemyKilled()
    {
        enemiesCount++;
        CheckEnemies();
    }

    private void CheckEnemies()
    {
        if (enemiesCount >= thisSpikeGoal)
        {
            Destroy(gameObject); 
        }
    }
}
