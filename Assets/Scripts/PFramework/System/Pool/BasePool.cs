using System.Collections.Generic;
using UnityEngine;

namespace PFramework
{
    public class BasePool<T> where T : Component
    {
        protected List<T> _pool = new List<T>();

        protected T _prefab;
        protected Transform _root;

        public BasePool(GameObject prefab, Transform root, int initAtStart)
        {
            _root = root;

            if (prefab != null)
                _prefab = prefab.GetComponent<T>();

            // Init pool
            for (int i = 0; i < initAtStart; i++)
            {
                T item = SpawnItem();
                SetActive(item, false);
                _pool.Add(item);
            }
        }

        public T GetItem()
        {
            // Find if there is any available item, return it
            for (int i = 0; i < _pool.Count; i++)
            {
                if (_pool[i] != null)
                {
                    if (IsActive(_pool[i]))
                    {
                        continue;
                    }
                    else
                    {
                        T item = _pool[i];

                        // New item active from pool will move to last
                        _pool.RemoveAt(i);
                        _pool.Add(item);

                        SetActive(item, true);
                        return item;
                    }
                }
                else
                {
                    _pool.RemoveAt(i);
                    i = Mathf.Clamp(i, 0, _pool.Count - 1);
                }
            }

            // Check if there is no more item in pool, create new
            _pool.Add(SpawnItem());
            return _pool.Last();
        }

        protected virtual T SpawnItem()
        {
            // Create new item and set parent to root
            T newItem = null;

            if (_prefab == null)
            {
                newItem = new GameObject().AddComponent<T>();
                newItem.transform.SetParent(_root);

#if UNITY_EDITOR
                newItem.name = typeof(T).ToString();
#endif
            }
            else
            {
                newItem = Object.Instantiate(_prefab, _root).GetComponent<T>();
            }

            return newItem;
        }

        protected virtual bool IsActive(T item)
        {
            return item.gameObject.activeSelf;
        }

        protected virtual void SetActive(T item, bool enabled)
        {
            item.gameObject.SetActive(enabled);
        }
    }
}
