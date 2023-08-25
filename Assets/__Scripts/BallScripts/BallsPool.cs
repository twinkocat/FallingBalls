using UnityEngine;

namespace twinkocat
{
    /// <summary>
    /// Extension of ObjectPool for Ball objects.
    /// </summary>
    public sealed class BallsPool : ObjectPool<Ball>
    {
        public BallsPool(Ball prefab, bool isExpandable, bool isActiveByDefault, int capacity, Transform parent = null) 
            : base(prefab, isExpandable, isActiveByDefault, capacity, parent) { }
    }
}
