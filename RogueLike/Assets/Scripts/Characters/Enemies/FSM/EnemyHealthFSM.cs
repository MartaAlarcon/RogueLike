using System.Collections;
using UnityEngine;

public class EnemyHealthFSM : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject coinPrefab;
    private float currentHealth;
    private Flash flash;

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        flash = GetComponent<Flash>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
        healthBar.UpdateHealthBar(startingHealth, currentHealth);
    }

    public void Hurt(float damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(startingHealth, currentHealth);

        if (currentHealth > 0)
        {
            StartCoroutine(flash.FlashRoutine()); 
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        if (currentHealth <= 0)
        {
            DropCoin();
            Destroy(gameObject);
            NotifySpikes();
            FinalBoss.instance.diedEnemies++;

        }
    }

    private void DropCoin()
    {
        Vector2 randomOffset = new Vector2(
           Random.Range(-0.5f, 0.5f), 
           Random.Range(-0.5f, 0.5f));

        Instantiate(coinPrefab, transform.position, Quaternion.identity);
        Instantiate(coinPrefab, (Vector2)transform.position + randomOffset, Quaternion.identity);
    }
    private void NotifySpikes()
    {
        Spikes[] allSpikes = FindObjectsOfType<Spikes>();
        foreach (Spikes spike in allSpikes)
        {
            spike.EnemyKilled();
        }
    }
}
