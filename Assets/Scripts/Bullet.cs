using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed;


        public void Setup(PlayerController playerController)
        {
            rb.AddForce(Vector2.right * speed * playerController.transform.localScale.x);
        }
    }
}