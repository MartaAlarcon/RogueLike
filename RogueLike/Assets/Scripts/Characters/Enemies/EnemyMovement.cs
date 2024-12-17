using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private Rigidbody2D rb;
    private Animator animator;
    private KnockBack knockBack;

    private Vector2 moveDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        SetRandomDirection();
    }

    private void FixedUpdate()
    {
        if (knockBack.gettingKnockedBack) { return; }

        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        UpdateAnimation();
    }

    private void SetRandomDirection()
    {
        // Establece una dirección aleatoria normalizada.
        moveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = (targetPosition - rb.position).normalized;
    }

    public Vector2 GetMoveDirection()
    {
        return moveDir;
    }

    private void UpdateAnimation()
    {
        float moveX = moveDir.x;
        float moveY = moveDir.y;

        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);

        if (moveDir.magnitude > 0.1f)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveDir = -moveDir;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (moveDir != Vector2.zero)
        {
            moveDir = moveDir.normalized;  // Normaliza la dirección si es necesario
        }
    }
}
