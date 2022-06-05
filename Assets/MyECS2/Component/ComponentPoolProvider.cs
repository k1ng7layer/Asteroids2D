using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyECS2
{
    internal class ComponentPoolProvider
    {
        public ComponentPoolProvider()
        {
            _pools = new Dictionary<int, ComponentPool>();
        }

        private Dictionary<int, ComponentPool> _pools;
        internal EntityComponentPool<T> CreatePool<T>(int hash) where T:struct
        {
            if (!_pools.ContainsKey(hash))
            {
                EntityComponentPool<T> componentPool = new EntityComponentPool<T>();
                _pools.Add(hash, componentPool);
                return componentPool;
            }
            else throw new InvalidOperationException();
        }

        internal EntityComponentPool<T> GetPool<T>(int hash) where T : struct
        {
            if (_pools.TryGetValue(hash, out ComponentPool pool))
            {
                return (EntityComponentPool<T>)pool;
            }
            else
            {
                return CreatePool<T>(hash);
            }
        }

        //internal ref T AddComponentToPool<T>(int hash, out int pooledIndex) where T:struct
        //{
        //    T instance = new T();
        //    if (!_pools.TryGetValue(hash, out ComponentPool result))
        //    {
        //        CreatePool<T>(hash);
        //    }
        //    var pool = GetPool<T>(hash);
        //    pool.AddComponentToPool(ref instance, out int index);
        //    ref var pooledComponent1 = ref pool.GetComponentFromPool<T>(index);
        //    pooledIndex = index;
        //    return ref pooledComponent1;
        //}
        internal ref T AddComponentToPool<T>(int entityId,int hash, out int pooledIndex) where T : struct
        {
            T instance = new T();
            if (!_pools.TryGetValue(hash, out ComponentPool result))
            {
                CreatePool<T>(hash);
            }
            var pool = GetPool<T>(hash);
            pool.AddComponentToPool(entityId,ref instance, out int index);
            ref var pooledComponent1 = ref pool.GetComponentFromPool<T>(index);
            pooledIndex = index;
            return ref pooledComponent1;
        }


        //internal int GetEntityPooledIndex(ref EntityObject entity, int hash)
        //{
        //    int index = 0;
        //    bool founded = false;
        //    for (int i = 0; i < entity.componentsHashCount; i++)    
        //    {
        //        if(entity.registeredComponentsHash[i]== hash)
        //        {
        //            index = i;
        //            founded = true;
        //            break;
        //        }
        //    }
        //    if (founded)
        //    {
        //        return entity.registeredComonentsIndexInPool[index];
        //    }
        //    throw new ArgumentNullException();
        //}
        internal int GetEntityPooledIndex(ref EntityObject entity, int hash)
        {
            if (_pools.TryGetValue(hash, out ComponentPool pool))
            {
                return pool.GetEntityPooledIndex(entity.globalIndex);
            }
            else throw new InvalidOperationException($"there is no pool of that type (type hash = {hash})");
        }
        internal void RemoveComponentFromPoolByEntityId(int hash,int entityGlobalId)
        {
            _pools[hash].RemoveEntityFromPool(entityGlobalId);
        }


    }
               
}
