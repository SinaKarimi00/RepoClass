using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class TestDotween : MonoBehaviour
    {
        private void Start()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();

            transform.DOLocalMoveY(1, 5f);
            transform.DOLocalRotate(new Vector3(0, 0, 90), 20f);
            transform.DOMoveX(2, 5);
            spriteRenderer.DOFade(0, 5);
            
            

            // var sequence = DOTween.Sequence();
            //
            // sequence.Append(transform.DOLocalMoveY(1, 5f));
            // sequence.Append(transform.DOLocalRotate(new Vector3(0, 0, 90), 20f));
            // sequence.Append(transform.DOMoveX(2, 5));
            //
            // sequence.Play();
        }
    }
}