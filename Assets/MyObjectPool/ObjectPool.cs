
using System.Collections.Generic;
using UnityEngine;

namespace MyObjectPool
{
    public sealed class ObjectPool: MonoSingleton<ObjectPool>
    {


        private Dictionary<int, Queue<PooledObject>> _pooledObjects = new Dictionary<int, Queue<PooledObject>>();
        private Dictionary<int, Transform> _parentObjects = new Dictionary<int, Transform>();
        [SerializeField] private Transform _objectsParent;

        public void CreatePoolingSlot<T>(T objectPrefab, int count) where T : PooledObject
        {
            if (!_pooledObjects.TryGetValue(objectPrefab.GetInstanceID(), out Queue<PooledObject> pooledObjects))
            {
                Queue<PooledObject> objects = new Queue<PooledObject>();
                for (int i = 0; i < count; i++)
                {
                    var name = objectPrefab.name + i;
                    Transform parentObj;
                    parentObj = GetParentObjectForPool(objectPrefab.GetInstanceID());
                    if (parentObj == null)
                        parentObj = CreateParentObjectForPool(objectPrefab.GetInstanceID(), objectPrefab.name).transform;

                    var instance = Instantiate(objectPrefab, parentObj.transform.position, parentObj.transform.rotation, parentObj.transform);
                    instance.name = name;
                    instance.id = objectPrefab.GetInstanceID();
                    objects.Enqueue(instance);
                    
                }
                _pooledObjects.Add(objectPrefab.GetInstanceID(), objects);
            }
        }
        //public void CreatePoolingSlot<T>(GameObject objectPrefab, int count) where T : PooledObject
        //{
        //    var obj = Instantiate(objectPrefab);
        //    var tInstance = obj.transform.GetOrCreateComponent<T>();
        //    var parentObj1 = GetParentObjectForPool(objectPrefab.GetInstanceID());
        //    tInstance.transform.SetParent(parentObj1.transform);

        //    if (!_pooledObjects.TryGetValue(tInstance.GetInstanceID(), out Queue<PooledObject> pooledObjects))
        //    {
        //        Queue<PooledObject> objects = new Queue<PooledObject>();
        //        for (int i = 0; i < count; i++)
        //        {
        //            var name = tInstance.name + i;
        //            var parentObj = GetParentObjectForPool(tInstance);
        //            var instance = Instantiate(tInstance, parentObj.transform.position, parentObj.transform.rotation, parentObj.transform);
        //            instance.name = name;
        //            instance.id = tInstance.GetInstanceID();
        //            objects.Enqueue(instance);
        //            Debug.Log("BULLET CREATED INIT");
        //        }
        //        _pooledObjects.Add(tInstance.GetInstanceID(), objects);
        //    }
        //}

        public void CreatePoolingSlot<T>(T objectPrefab) where T : PooledObject
        {
            if (!_pooledObjects.TryGetValue(objectPrefab.GetInstanceID(), out Queue<PooledObject> pooledObjects))
            {
                Queue<PooledObject> objects = new Queue<PooledObject>();
                var name = objectPrefab.name + 1;
                Transform parentObj;
                parentObj = GetParentObjectForPool(objectPrefab.GetInstanceID());
                if (parentObj == null)
                    parentObj = CreateParentObjectForPool(objectPrefab.GetInstanceID(), objectPrefab.name).transform;

                var instance = GameObject.Instantiate(objectPrefab, this.transform.position, this.transform.rotation, parentObj.transform);
                instance.name = name;
                objects.Enqueue(instance);
                _pooledObjects.Add(objectPrefab.id, objects);
                Debug.Log("BULLET CREATED INIT");
            }
        }

        public T GetObjectFromPool<T>(T objectPrefab, Vector3 position, Quaternion rotation) where T : PooledObject
        {
            T instance = null;
            if (_pooledObjects.TryGetValue(objectPrefab.GetInstanceID(), out Queue<PooledObject> queue))
            {
                if (queue.Count > 0)
                {
                    instance = queue.Dequeue() as T;
                    instance.transform.position = position;
                    instance.transform.rotation = rotation;
                    instance.OnCreated();
                }
                else
                {
    
                    var parentObj = GetParentObjectForPool(objectPrefab.GetInstanceID());
                    instance = Instantiate<T>(objectPrefab, position, rotation, parentObj.transform);
                    instance.id = objectPrefab.GetInstanceID();
                    instance.OnCreated();
                    
                }
            }
            else
            {
                CreatePoolingSlot(objectPrefab);
                instance = GetObjectFromPool(objectPrefab, position, rotation);
                instance.OnCreated();
               
            }
            return instance;
        }
        public T GetObjectFromPool<T>(T objectPrefab, Vector3 position, Quaternion rotation, Vector3 scale) where T : PooledObject
        {
            T instance = null;
            if (_pooledObjects.TryGetValue(objectPrefab.GetInstanceID(), out Queue<PooledObject> queue))
            {
                if (queue.Count > 0)
                {
                    instance = queue.Dequeue() as T;
                    instance.transform.position = position;
                    instance.transform.rotation = rotation;
                    instance.gameObject.transform.localScale = scale;
                    instance.OnCreated();
                }
                else
                {
                   
                    var parentObj = GetParentObjectForPool(objectPrefab.GetInstanceID());
                    instance = Instantiate<T>(objectPrefab, position, rotation, parentObj.transform);
                    instance.id = objectPrefab.GetInstanceID();
                    instance.transform.localScale = scale;
                    instance.OnCreated();

                }
            }
            else
            {
                CreatePoolingSlot(objectPrefab);
                instance = GetObjectFromPool(objectPrefab, position, rotation);
                instance.transform.localScale = scale;
                instance.OnCreated();
                
            }
            return instance;
        }

        private GameObject CreateParentObjectForPool(int id, string name)
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
        private Transform GetParentObjectForPool(int id)
        {
            if (_parentObjects.TryGetValue(id,out Transform parent))
            {
                return parent;
            }
            return default;
        }
        public void ReturnObjectToPool(PooledObject pooledObject)
        {
            if (_pooledObjects.TryGetValue(pooledObject.id, out Queue<PooledObject> queue))
            {
                queue.Enqueue(pooledObject);
                
                var parent = GetParentObjectForPool(pooledObject.id);
                

                pooledObject.transform.localPosition = parent.transform.position;
                
            }
            else
            {
                CreatePoolingSlot(pooledObject);
               
            }
        }
    }
}
            

           

           
           
           
           

