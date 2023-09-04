using UnityEngine;
using System.Collections.Generic;

namespace twinkocat
{
    /// <summary>
    /// Object pool class.
    /// </summary>
    public abstract class ObjectPool<T> where T : Component
    {
        protected bool                  _isExpandable;
        protected bool                  _isActiveByDefault;
        protected int                   _poolObjectCount;
        protected Transform             _parent;

        protected T                     _object;
        protected Stack<T>              _objectPool;

        /// <summary>
        /// Creating object pool instance.
        /// </summary>
        public ObjectPool(  T prefab,
                            bool isExpandable, 
                            bool isActiveByDefault, 
                            int poolObjectCount, 
                            Transform parent = null)
        {
            _object = prefab;
            _isExpandable = isExpandable;
            _isActiveByDefault = isActiveByDefault;
            _poolObjectCount = poolObjectCount;
            _parent = parent;

            FillPool();
        }

        /// <summary>
        /// Fill object pool.
        /// </summary>
        protected void FillPool()
        {
            _objectPool = new Stack<T>(_poolObjectCount);

            for (int i = 0; i < _poolObjectCount; i++)
            {
                _objectPool.Push(CreateObject());
            }
        }

        /// <summary>
        /// Create object
        /// </summary>
        protected virtual T CreateObject()
        {
            var obj = GameObject.Instantiate(_object, _parent);
            obj.gameObject.SetActive(_isActiveByDefault);

            OnCreate(obj);

            return obj;
        }

        /// <summary>
        /// Get object from pool.
        /// </summary>
        public T PopObject()
        {
            if (_objectPool.TryPop(out T obj))
            {
                OnGet(obj);

                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                if (_isExpandable)
                {
                    var newObj = CreateObject();
                    newObj.gameObject.SetActive(true);

                    OnGet(newObj);

                    return newObj;
                }
                else
                {
                    Debug.LogError("Error: pool is not expandable");
                    return null;
                }
            }
        }

        /// <summary>
        /// Return object to pool.
        /// </summary>
        public void ReturnObject(T obj)
        {
            OnReturn(obj);

            obj.gameObject.SetActive(false);
            _objectPool.Push(obj);
        }

        /// <summary>
        /// Destroy pool.
        /// </summary>
        public void DestroyPool()
        {
            for (int i = 0; i < _objectPool.Count; i++)
            {
                GameObject.Destroy(_objectPool.Pop());
            }
        }

        protected virtual void OnGet(T obj) => (obj as IPoolObject)?.OnPop();

        protected virtual void OnCreate(T obj) => (obj as IPoolObject)?.OnCreate();

        protected virtual void OnReturn(T obj) => (obj as IPoolObject)?.OnReturn();
    }

}