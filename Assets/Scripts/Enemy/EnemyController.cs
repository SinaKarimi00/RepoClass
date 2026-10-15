using System;
using UnityEngine;

namespace DefaultNamespace.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] Rigidbody2D rb;
        [SerializeField] float speed;
        [SerializeField] BoxCollider2D boxCollider2D;

        private void Update()
        {
            rb.linearVelocity = Vector2.right * speed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("*****");
            if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                speed *= -1;
                FlipEnemy();
            }
        }

        // private void OnCollisionExit2D(Collision2D other)
        // {
        //     if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        //     {
        //         speed *= -1;
        //         FlipEnemy();
        //     }
        // }


        private void FlipEnemy()
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,
                transform.localScale.z);
        }
    }
}