using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] private Material redFlashMat;
    [SerializeField] private float health;
    private float startingHealth = 5f;
    [SerializeField] private float restoreDefaultMatTime = .2f;
    public float damage;

    [SerializeField] private GameObject spawnPoint;
    private Animator animator;
    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;


    public void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        health = startingHealth;
        animator = GetComponent<Animator>();
        spawnPoint = GameObject.FindWithTag("Respawn");
        healthBar.UpdateHealthBar(startingHealth, health);

    }
    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Hurt(1);
        }
    }
    
    public void Hurt(float damage)
    {
        health-=damage;
        healthBar.UpdateHealthBar(startingHealth, health);
        StartCoroutine(FlashRoutine());
        if (health <= 0) Die();
    }
    public void Die()
    {     
        health = 0;
        animator.SetFloat("Health", health);
        StartCoroutine(WaitForDeathAnimation());

    }
    private IEnumerator WaitForDeathAnimation()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float deathAnimLength = stateInfo.length;
        yield return new WaitForSeconds(deathAnimLength);
        transform.position = spawnPoint.transform.position;
        health = startingHealth;
        animator.SetFloat("Health", health);
        healthBar.UpdateHealthBar(startingHealth, health);
    }
    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = redFlashMat;
        yield return new WaitForSeconds(restoreDefaultMatTime);
        spriteRenderer.material = defaultMaterial;
    }
}
