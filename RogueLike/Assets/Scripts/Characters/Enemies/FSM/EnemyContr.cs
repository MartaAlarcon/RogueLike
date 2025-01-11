using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    Attack,
    Idle,
    Chase,
    Die,
    Run,
}

public class EnemyContr : MonoBehaviour
{
    public float attackRange = 1.0f; 
    public float attackCooldown = 2.0f; 
    public int HP;
    public GameObject target;
    private ChaseBehaviour _chaseB;
    private EnemyHealthFSM _enemyHealthFSM;
    public States Currentstate;

    private Animator animator;
    private bool attack = false;

    public GameObject explosion;

    private void Start()
    {

        _chaseB = GetComponent<ChaseBehaviour>();
        _enemyHealthFSM = GetComponent<EnemyHealthFSM>();
        Currentstate = States.Idle;
        animator = GetComponent<Animator>();
        explosion.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 && CheckIfAlife())
        {
            target = collision.gameObject;
            Currentstate = States.Chase;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 && CheckIfAlife())
        {
            Currentstate = States.Idle;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7 && CheckIfAlife())
        {
            target = collision.gameObject;
            Currentstate = States.Attack;
            StartCoroutine(PerformAttack()); 
        }
    }

    public void ReceiveDamage(float damage)
    {
        HP -= (int)damage;

        if (CheckIfAlife())
        {
            if (Currentstate == States.Chase)
            {
                _chaseB.Chase(target.transform, transform);
            }
            else if (Currentstate != States.Die)
            {
                Currentstate = States.Idle; // Establecer Idle solo si no está persiguiendo
            }
        }
        else
        {
            Currentstate = States.Die; // Cambiar a muerto
            _chaseB.StopChasing(); // Detener movimiento al morir
        }
    }

    public bool CheckIfAlife()
    {
        return HP > 0;
    }

    void Update()
    {
        switch (Currentstate)
        {
            case States.Attack:
                break;
            case States.Idle:
                Idle();
                break;
            case States.Chase:
                Chase();
                break;
            case States.Die:
                Die();
                break;
        }
    }

    private IEnumerator PerformAttack()
    {
        _chaseB.StopChasing();

        attack = true;
        animator.SetBool("Attack", attack);
        Debug.Log("Attack executed on player");

        yield return new WaitForSeconds(attackCooldown); // Esperar tras atacar
        Currentstate = States.Chase; // Volver a perseguir
    }

    public void Idle()
    {
        Debug.Log("Idle");
        _chaseB.StopChasing();
    }

    public void Die()
    {
        Debug.Log("Die");
        _chaseB.StopChasing();
    }

    public void Chase()
    {

        Debug.Log("Chase");
        if (target != null)
        {
            attack = false;
            animator.SetBool("Attack", attack);
            _chaseB.Chase(target.transform, transform);
        }
    }
    public void Explosion()
    {
        explosion.SetActive(true);
    }
    public void StopExplosion()
    {
        explosion.SetActive(false);
    }
}
