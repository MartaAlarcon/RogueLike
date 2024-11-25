using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    public float health;
    public float damage;
    public void Move(){}
    public void Attack(){}
    public void Hurt(){}
    public void Die(){}
}
