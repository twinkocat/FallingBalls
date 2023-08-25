using System;
using UnityEngine;

namespace twinkocat
{
    public class BallDeathTrigger : MonoBehaviour
    {
        public event Action<Ball>       OnDeathTriggerBallEnter;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Ball ball))
            {
                OnDeathTriggerBallEnter?.Invoke(ball);
            }
        }
    }
}