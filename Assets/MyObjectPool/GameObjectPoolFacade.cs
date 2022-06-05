using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyObjectPool
{
    public class GameObjectPoolFacade
    {
        public static void CreatePoolingSlot(string poolName, GameObject objectPrefab, int count)
        {
            GameObjectPool.Instance.CreatePoolingSlot(poolName,objectPrefab, count);
        }

        public static GameObject GetObjectFromPool(string poolName, GameObject objectPrefab, Vector3 position, Quaternion rotation)
        {
            return GameObjectPool.Instance.GetObjectFromPool(poolName,objectPrefab, position, rotation);
        }

        public static GameObject GetObjectFromPool(string poolName, GameObject objectPrefab, Vector3 position, Quaternion rotation,Vector3 scale)
        {
            return GameObjectPool.Instance.GetObjectFromPool(poolName, objectPrefab, position, rotation, scale);
        }

        public static void ReturnObjectToPool(string poolName,GameObject pooledObject)
        {
            GameObjectPool.Instance.ReturnObjectToPool(poolName,pooledObject);
        }
    }
}
