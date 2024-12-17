using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    public float damage;

    [SerializeField] private GameObject spawnPoint;
    private Animator animator;


    public void Start()
    {
        health = 10;
        animator = GetComponent<Animator>();
        spawnPoint = GameObject.FindWithTag("Respawn");
    }

    public void Hurt()
    {

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
}
