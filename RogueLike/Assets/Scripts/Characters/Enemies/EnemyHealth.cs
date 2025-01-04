using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
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
    public void Start()
    {
        currentHealth = startingHealth;
        healthBar.UpdateHealthBar(startingHealth, currentHealth);

    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(startingHealth, currentHealth);
        DetectDeath();
       
    }
    public void GetKnockedBack()
    {
        knockBack.GetKnockedBack(PlayerMovement.Instance.transform, 10f);
        StartCoroutine(flash.FlashRoutine());
    }
    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            DropCoin(); 
            Destroy(gameObject);
        }
    }
    private void DropCoin()
    {       
        Instantiate(coinPrefab, transform.position, Quaternion.identity);       
    }
}
