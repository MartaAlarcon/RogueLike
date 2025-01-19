using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");
    private Animator animator;
    private InputController playerControls;

    [SerializeField] private float attackCooldown = 0.75f; 
    private bool canAttack = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerControls = new InputController();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Combat.Attack.started += _ => AttemptAttack();
    }


    private void AttemptAttack()
    {
        if (canAttack)
        {
            Attack();
        }
    }

    public void Attack()
    {
        canAttack = false; 
        animator.SetTrigger(FIRE_HASH);

        GameObject arrow = Spawner.Instance.GetFromPool();
        if (arrow != null)
        {
            arrow.transform.position = arrowSpawnPoint.position;
            arrow.transform.rotation = arrowSpawnPoint.rotation;

            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = Vector2.zero;
        }

        StartCoroutine(ResetAttackCooldown());
    }

    private IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true; 
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    private void OnDisable()
    {
        playerControls.Combat.Attack.started -= _ => AttemptAttack();
        playerControls.Disable();
        animator = null;
    }

    public void CleanUp()
    {
        playerControls.Disable();
        animator = null;
    }
}
