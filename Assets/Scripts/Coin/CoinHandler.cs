using System;
using UnityEngine;

namespace DefaultNamespace.Coin
{
    public class CoinHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;


        private void OnTriggerEnter2D(Collider2D other)
        {
            audioSource.Play();
        }
    }
}