using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private float moveX;
    private float moveY;

    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    private void Update()
    {
        Vector2 direction = enemyPathfinding.GetMoveDirection();
        moveX = direction.x;
        moveY = direction.y;

        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);

        if (direction.magnitude > 0.1f)
        {
            animator.SetBool("isMoving", true); 
        }
        else
        {
            animator.SetBool("isMoving", false); 
        }
    }
}
