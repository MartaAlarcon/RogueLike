using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject coinPrefab;
    private float currentHealth;
    private KnockBack knockBack;
    private Flash flash;

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
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
        Die();
    }

    public void GetKnockedBack()
    {
        knockBack.GetKnockedBack(PlayerMovement.Instance.transform, 10f);
        StartCoroutine(flash.FlashRoutine());
    }

    public void Die()
    {
        if (currentHealth <= 0)
        {
            DropCoin();
            Destroy(gameObject);
            NotifySpikes();
        }
    }

    private void DropCoin()
    {
        Instantiate(coinPrefab, transform.position, Quaternion.identity);
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
