using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private Bullet bulletPrefab;

    private Vector2 moveInput = new Vector2();
    private float baseGravity;
    private float animationSpeed;
    private bool isAlive = true;


    private void Awake()
    {
        baseGravity = rb.gravityScale;
        animationSpeed = animator.speed;
    }


    private void Update()
    {
        Run();
        Climbing();
    }


    private void Run()
    {
        if (!isAlive)
            return;
        rb.linearVelocity = new Vector2(moveInput.x * horizontalSpeed, rb.linearVelocity.y);

        if (Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon)
        {
            animator.SetBool("IsRunning", true);
            FlipPlayer();
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }


    private void FlipPlayer()
    {
        transform.localScale = new Vector3(Mathf.Sign(moveInput.x), transform.localScale.y,
            transform.localScale.z);
    }

    private void OnMove(InputValue value)
    {
        if (!isAlive)
            return;
        moveInput = value.Get<Vector2>();
        Debug.Log(Mathf.Sign(moveInput.x));
    }

    private void OnJump(InputValue value)
    {
        if (!isAlive)
            return;
        var groundLayer = LayerMask.GetMask("Ground");
        var isGrounded = boxCollider2D.IsTouchingLayers(groundLayer);

        if (!isGrounded)
            return;


        if (value.isPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        }
    }


    private void Die()
    {
        isAlive = false;
        animator.SetBool("Die", true);
        rb.linearVelocity = new Vector2(5, 10);
    }


    private void Climbing()
    {
        if (!isAlive)
            return;

        var laderLayer = LayerMask.GetMask("Lader");
        var isTouchingLayer = capsuleCollider.IsTouchingLayers(laderLayer);

        if (!isTouchingLayer)
        {
            animator.SetBool("IsClimbing", false);

            rb.gravityScale = baseGravity;
            animator.StopPlayback();
            return;
        }

        // animator.StopPlayback();
        //
        // animator.Play("Climbing");
        // animator.SetBool("IsClimbing", true);

        animator.SetBool("IsClimbing", true);

        if (Mathf.Abs(rb.linearVelocity.y) > Mathf.Epsilon)
        {
            animator.speed = animationSpeed;
        }
        else
        {
            animator.speed = 0;
            // animator.SetBool("IsClimbing", false);
        }

        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, moveInput.y * verticalSpeed);
    }

    private void OnAttack(InputValue value)
    {
        var bullet = Instantiate(bulletPrefab, transform.localPosition, Quaternion.identity);
        // var bullet = bulletGameObject.GetComponent<Bullet>();
        bullet.Setup(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            Die();
        }
    }
}