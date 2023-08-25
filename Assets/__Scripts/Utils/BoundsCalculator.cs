using UnityEngine;

namespace twinkocat
{
    public class BoundsCalculator
    {
        private readonly Camera         _camera;
        private static Vector2          _minScreenWorldPoint;


        public BoundsCalculator(Camera camera)
        {
            _camera = camera;
            _minScreenWorldPoint = CalculateMinScreenWorldPoint();
        }

        public static void Init(Camera camera) => new BoundsCalculator(camera);
        private Vector2 CalculateMinScreenWorldPoint() => _camera.ScreenToWorldPoint(Vector2.zero);
        public static Vector2 GetMinScreenPoint() => _minScreenWorldPoint;
    }
}
