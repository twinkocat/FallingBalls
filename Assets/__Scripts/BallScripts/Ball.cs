using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace twinkocat
{
    [RequireComponent(  typeof(CircleCollider2D),
                        typeof(SpriteRenderer))]
    public class Ball : MonoBehaviour, IPoolObject
    {
        private const float             SCALE_SCORE_MIDIFER = 10f;

        [SerializeField] private float  _minRadius;
        [SerializeField] private float  _maxRadius;
        [Space]
        [SerializeField] private float  _minSpeed;
        [SerializeField] private float  _maxSpeed;

        private float                   _speed;
        private SpriteRenderer          _spriteRenderer;

        public float                    MinSpeed => _minSpeed;
        public float                    MaxSpeed => _maxSpeed;
        public float                    CurrentSpeed => _speed;

        public event Action<Ball>       OnBallClicked;


        void IPoolObject.OnCreate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            var kek = "lol";
        }

        void IPoolObject.OnPop()
        {
            SetBallSpeed(Random.Range(_minSpeed, _maxSpeed));
            SetBallRadius(Random.Range(_minRadius, _maxRadius));
            SetBallColor(Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f)); // Pick a random, saturated and not-too-dark color
        }

        private void Update()
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }

        void IPoolObject.OnReturn() { }

        public int GetBallScore() => (int)(CurrentSpeed + ((transform.localScale.x * SCALE_SCORE_MIDIFER) - transform.localScale.x));

        public void SetMinSpeed(float value) => _minSpeed = value;

        public void SetMaxSpeed(float value) => _maxSpeed = value;

        private void SetBallColor(Color color) => _spriteRenderer.color = color;

        private void SetBallSpeed(float value) => _speed = value;

        private void SetBallRadius(float value) => transform.localScale = Vector3.one * value;

        private void OnMouseDown() => OnBallClicked?.Invoke(this);
    }
}
