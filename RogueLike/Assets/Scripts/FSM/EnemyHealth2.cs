using System.Collections;
using UnityEngine;

public class EnemyHealthFSM : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject coinPrefab;
    private float currentHealth;
    //private KnockBack knockBack;
    private Flash flash;

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        flash = GetComponent<Flash>();
        //knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
        healthBar.UpdateHealthBar(startingHealth, currentHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(startingHealth, currentHealth);

        if (currentHealth > 0)
        {
            StartCoroutine(flash.FlashRoutine()); // Efecto visual
            //knockBack.GetKnockedBack(PlayerMovement.Instance.transform, 10f); // Retroceso
        }
        else
        {
            DetectDeath();
        }
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            DropCoin();
            Destroy(gameObject);
        }
    }

    private void DropCoin()
    {
        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }
}
