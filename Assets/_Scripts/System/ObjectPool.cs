using System.Collections.Generic;
using UnityEngine;

namespace System
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private Queue<T> _objectQueue;
        private GameObject _objectPrefab;

        //单例模式
        private static ObjectPool<T> _instance;

        public static ObjectPool<T> instance => _instance ??= new ObjectPool<T>();
        private int queueCount => _objectQueue.Count;

        public void InitPool(GameObject prefab)
        {
            _objectPrefab = prefab;
            _objectQueue = new Queue<T>();
        }

        public T Spawn(Vector3 position, Quaternion quaternion)
        {
            if (_objectPrefab == null)
            {
                Debug.LogError(typeof(T).ToString() + "prefab not set");
                return default(T);
            }

            if (queueCount <= 0)
            {
                var g = UnityEngine.Object.Instantiate(_objectPrefab, position, quaternion);
                var t = g.GetComponent<T>();

                if (t == null)
                {
                    Debug.LogError(typeof(T).ToString() + "not found in prefab");
                    return default(T);
                }

                _objectQueue.Enqueue(t);
            }

            T obj = _objectQueue.Dequeue();
            obj.gameObject.SetActive(true);
            
            var transform = obj.transform;
            transform.position = position;
            transform.rotation = quaternion;

            return obj;
        }

        public void Recycle(T obj)
        {
            _objectQueue.Enqueue(obj);
            obj.gameObject.SetActive(false);
        }
    }
}