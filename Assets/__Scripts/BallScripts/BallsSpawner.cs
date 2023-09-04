using System;
using UnityEngine;

namespace twinkocat
{
    public class BallsSpawner : MonoBehaviour
    {
        private const float                         Y_OFFSET_VALUE = 0.5f;
        private const float                         START_SPAWN_OFFSET_VALUE = 1f;

        private Vector2                             _minScreenWorldValue;
        private float                               _lastTimeSpawned;
        private float                               _delay;

        private BallsPool                           _ballsPool;

        public event Action<Ball>                   OnBallSpawn;
        

        public void Init(BallsPool ballsPool, float delay)
        {
            _ballsPool = ballsPool;
            _delay = delay;

            _minScreenWorldValue = BoundsCalculator.GetMinScreenPoint();

        }

        private void Update()
        {
            if (Time.time > _lastTimeSpawned)
            {
                var ball = _ballsPool.PopObject();
                ball.transform.position = new Vector2(
                                            UnityEngine.Random.Range(_minScreenWorldValue.x + Y_OFFSET_VALUE, - _minScreenWorldValue.x - Y_OFFSET_VALUE),
                                            - _minScreenWorldValue.y + START_SPAWN_OFFSET_VALUE);
                OnBallSpawn?.Invoke(ball);
                _lastTimeSpawned = Time.time + _delay;
            }
        }

        public void SetSpawnDelay(float value) => _delay = value;
    }
}