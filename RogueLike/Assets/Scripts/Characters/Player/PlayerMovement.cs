using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    private bool facingLeft = false;
    public static PlayerMovement Instance;
    [SerializeField] private Transform weaponCollider;

    PlayerInputManager inputManager;
    private Rigidbody2D rb;
    private Animator animator;
    private float moveX;
    private float moveY;

    private void Awake()
    {
        Instance = this;
        inputManager = GetComponent<PlayerInputManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 direction = inputManager.direction;

        moveX = direction.x;
        moveY = direction.y;

        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
        float speed = direction.sqrMagnitude;  
        animator.SetFloat("Speed", speed);

        Move(direction);
    }
    public Transform GetWeaponCollider() { return weaponCollider; }

    private void Move(Vector2 direction)
    {
        rb.velocity = direction * inputManager.speed;
    }

}
