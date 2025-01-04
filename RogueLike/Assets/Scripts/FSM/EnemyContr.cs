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
    public Collider2D chaseCollider; // Collider para perseguir
    public Collider2D damageCollider; // Collider para recibir daño
    public float attackRange = 1.0f; // Distancia para iniciar ataque

    public int HP;
    public GameObject target;
    private ChaseBehaviour _chaseB;
    private EnemyHealthFSM _enemyHealthFSM;
    public States Currentstate;

    private void Start()
    {
        _chaseB = GetComponent<ChaseBehaviour>();
        _enemyHealthFSM = GetComponent<EnemyHealthFSM>();
        Currentstate = States.Idle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == chaseCollider && collision.CompareTag("Player") && CheckIfAlife())
        {
            target = collision.gameObject;
            Currentstate = States.Chase;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == chaseCollider && collision.CompareTag("Player") && CheckIfAlife())
        {
            Currentstate = States.Idle;
        }
    }

    public void ReceiveDamage(float damage)
    {
        HP -= (int)damage;

        // Si el enemigo sigue vivo, regresa a su estado previo o Idle
        if (CheckIfAlife())
        {
            if (Currentstate == States.Chase)
            {
                // Mantener Chase si ya estaba persiguiendo
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
                Attack();
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

    public void Attack()
    {
        Debug.Log("Attack");
        _chaseB.StopChasing();

        // Verificar si está cerca del jugador
        if (Vector2.Distance(transform.position, target.transform.position) <= attackRange)
        {
            // Lógica para ataque, por ejemplo, infligir daño al jugador
            // Suponiendo que el jugador tiene un método TakeDamage:
            // PlayerHealth.Instance.TakeDamage(damage);
            Debug.Log("Attack executed on player");
            _chaseB.StopChasing();
        }
        else
        {
            Currentstate = States.Chase; // Volver a perseguir si está fuera de alcance
        }
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
            _chaseB.Chase(target.transform, transform);
        }
    }
}
