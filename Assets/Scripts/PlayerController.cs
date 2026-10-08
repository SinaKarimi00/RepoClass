using System;
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
    [SerializeField] private AnimationClip animationClip;


    private Vector2 moveInput = new Vector2();
    private float baseGravity;
    private float animationSpeed;
    


    private void Awake()
    {
        baseGravity = rb.gravityScale;
        animationSpeed = animator.speed ;

    }


    private void Update()
    {
        Run();
        Climbing();
    }


    private void Run()
    {
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
        moveInput = value.Get<Vector2>();
        Debug.Log(Mathf.Sign(moveInput.x));
    }

    private void OnJump(InputValue value)
    {
        var groundLayer = LayerMask.GetMask("Ground");
        var isGrounded = boxCollider2D.IsTouchingLayers(groundLayer);

        if (!isGrounded)
            return;


        if (value.isPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        }
    }


    private void Climbing()
    {
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
}