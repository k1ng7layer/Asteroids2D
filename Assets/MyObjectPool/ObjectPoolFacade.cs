using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyObjectPool
{
    public static class ObjectPoolFacade
    {
        public static void CreatePoolingSlot<T>(T objectPrefab, int count) where T : PooledObject
        {
            ObjectPool.Instance.CreatePoolingSlot<T>(objectPrefab, count);
        }

        public static T GetObjectFromPool<T>(T objectPrefab, Vector3 position, Quaternion rotation) where T : PooledObject
        {
            return ObjectPool.Instance.GetObjectFromPool<T>(objectPrefab, position, rotation);
        }
        public static T GetObjectFromPool<T>(T objectPrefab, Vector3 position, Quaternion rotation, Vector3 scale) where T : PooledObject
        {
            return ObjectPool.Instance.GetObjectFromPool<T>(objectPrefab, position, rotation, scale);
        }

        public static void ReturnObjectToPool(PooledObject pooledObject)
        {
            ObjectPool.Instance.ReturnObjectToPool(pooledObject);
        }
    }
}
