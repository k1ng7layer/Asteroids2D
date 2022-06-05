using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyObjectPool
{
    public class GameObjectPool:MonoSingleton<GameObjectPool>
    {
        private Dictionary<string, Queue<GameObject>> _pooledObjects = new Dictionary<string, Queue<GameObject>>();
        private Dictionary<string, Transform> _parentObjects = new Dictionary<string, Transform>();
        [SerializeField] private Transform _objectsParent;

        public void CreatePoolingSlot(string poolName,GameObject objectPrefab, int count)
        {
            if (!_pooledObjects.TryGetValue(poolName, out Queue<GameObject> pooledObjects))
            {
                Queue<GameObject> objects = new Queue<GameObject>();
                for (int i = 0; i < count; i++)
                {
                    var name = objectPrefab.name + i;
                    Transform parentObj;
                    parentObj = GetParentObjectForPool(poolName);
                    if (parentObj == null)
                        parentObj = CreateParentObjectForPool(poolName, objectPrefab.name).transform;

                    var instance = GameObject.Instantiate(objectPrefab, parentObj.transform.position, parentObj.transform.rotation, parentObj.transform);
                    instance.name = name;
              
                    objects.Enqueue(instance);

                }
                _pooledObjects.Add(poolName, objects);
            }
        }
      

        public void CreatePoolingSlot(string key,GameObject objectPrefab)
        {
            if (!_pooledObjects.TryGetValue(key, out Queue<GameObject> pooledObjects))
            {
                Queue<GameObject> objects = new Queue<GameObject>();
                var name = objectPrefab.name + 1;
                Transform parentObj;
                parentObj = GetParentObjectForPool(key);
                if (parentObj == null)
                    parentObj = CreateParentObjectForPool(key, objectPrefab.name).transform;

                var instance = GameObject.Instantiate(objectPrefab, this.transform.position, this.transform.rotation, parentObj.transform);
                instance.name = name;
                objects.Enqueue(instance);
                _pooledObjects.Add(key, objects);
       
            }
        }

        public GameObject GetObjectFromPool(string key,GameObject objectPrefab, Vector3 position, Quaternion rotation)
        {
            GameObject instance = null;
            if (_pooledObjects.TryGetValue(key, out Queue<GameObject> queue))
            {
                if (queue.Count > 0)
                {
                    instance = queue.Dequeue();
                    instance.transform.position = position;
                    instance.transform.rotation = rotation;
                    //instance.OnCreated();
                }
                else
                {
                    
                    var parentObj = GetParentObjectForPool(key);
                    instance = Instantiate(objectPrefab, position, rotation, parentObj.transform);


                }
            }
            else
            {
                CreatePoolingSlot(key, objectPrefab);
                instance = GetObjectFromPool(key,objectPrefab, position, rotation);

            }
            return instance;  
        }
        public GameObject GetObjectFromPool(string key, GameObject objectPrefab, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            GameObject instance = null;
            if (_pooledObjects.TryGetValue(key, out Queue<GameObject> queue))
            {
                if (queue.Count > 0)
                {
                    instance = queue.Dequeue();
                    instance.transform.position = position;
                    instance.transform.rotation = rotation;
                    instance.transform.localScale = scale;
                }
                else
                {

                    var parentObj = GetParentObjectForPool(key);
                    instance = Instantiate(objectPrefab, position, rotation, parentObj.transform);
                    instance.transform.localScale = scale;
                }
            }
            else
            {
                CreatePoolingSlot(key, objectPrefab);
                instance = GetObjectFromPool(key, objectPrefab, position, rotation);

            }
            return instance;
        }


        private GameObject CreateParentObjectForPool(string id, string name)
        {
            if (_parentObjects.TryGetValue(id, out Transform parent))
            {
                return parent.gameObject;
            }
            else
            {
                var poolName = name + "objectPool";
                GameObject obj = new GameObject(poolName);

                obj.transform.position = new Vector3(-300f, -300f, 0f);
                _parentObjects.Add(id, obj.transform);
                return obj;
            }

        }
        private Transform GetParentObjectForPool(string id)
        {
            if (_parentObjects.TryGetValue(id, out Transform parent))
            {
                return parent;
            }
            return default;
        }
        public void ReturnObjectToPool(string poolName,GameObject pooledObject)
        {

            if (_pooledObjects.TryGetValue(poolName, out Queue<GameObject> queue))
            {
                queue.Enqueue(pooledObject);

                var parent = GetParentObjectForPool(poolName);


                pooledObject.transform.localPosition = parent.transform.position;
                pooledObject.transform.transform.position = parent.transform.position;

            }
            else
            {
                CreatePoolingSlot(poolName,pooledObject);

            }
        }
    }
}
