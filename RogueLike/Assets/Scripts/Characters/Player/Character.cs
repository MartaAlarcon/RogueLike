using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    [SerializeField] private Material redFlashMat;
    [SerializeField] private float health;
    [SerializeField] private float restoreDefaultMatTime = .2f;
    public float damage;

    [SerializeField] private GameObject spawnPoint;
    private Animator animator;
    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;


    public void Start()
    {
        health = 10;
        animator = GetComponent<Animator>();
        spawnPoint = GameObject.FindWithTag("Respawn");
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
            Hurt();
        }
    }
    public void Hurt()
    {
        health--;
        StartCoroutine(FlashRoutine());

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
        health = 10;
        animator.SetFloat("Health", health);
    }
    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = redFlashMat;
        yield return new WaitForSeconds(restoreDefaultMatTime);
        spriteRenderer.material = defaultMaterial;
    }
}
